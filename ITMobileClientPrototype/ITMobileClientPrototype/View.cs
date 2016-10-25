using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.Remoting;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Remoting.Channels;
using System.Runtime.Remoting.Channels.Tcp;
using ITServer;

namespace ITMobileClientPrototype
{
    public partial class View : Form
    {
        //текущий игрок
        private Player p;

        

        //онлайн или нет?
        private bool isOnline = false;

        public View()
        {
            InitializeComponent();
        }

        public int lmin;
        public int lmax;
        public int tmin;
        public int tmax;

        
        private ITServer.ServerHandler sh;
       

        //получает обновления с сервера
        //обновляет лог
        private void getUpdates(string email)
        {
            updatePoints(email);
            updateCards(email);
            updateMessages(email);
        }

        public void updateMessages(string email)
        {
            richTextBox1.Clear();
            string[] lines = sh.getMessages(email);
            foreach (string s in lines)
            {
                if (s.StartsWith(sh.trade_hash))
                {
                    
                    string[] l = s.Split('|');
                    string from = l[1];
                    string to = l[2];
                    int from_ = Convert.ToInt32(l[3]);
                    int to_ = Convert.ToInt32(l[4]);
                    sh.completeTrade(from, to, from_, to_);

                    MessageBox.Show("Вам пришел обмен от " + from + " на вашу карту " + to_.ToString() + " на карту " + from_.ToString());

                    MessageBox.Show("Удаляем у [" + p.getEmail() + "] карту + <" + from_ + ">");

                    sh.removeCardFrom(p.getEmail(), from_);
                    MessageBox.Show("Удаляем у [" + to + "] карту + <" + to_ + ">");

                    sh.removeCardFrom(to,to_);

                    MessageBox.Show("Добавляем игроку [" + p.getEmail() + "] карту + <" + to_ + ">");
                    sh.addCardTo(p.getEmail(), to_);
                    MessageBox.Show("Добавляем игроку [" + to + "] карту + <" + from_ + ">");
                    sh.addCardTo(to, from_);


                    
                    
                }
                else
                richTextBox1.AppendText(s + "\n");
            }

        }
        //обновляет очки на экране
        private void updatePoints(string email)
        {
            int points = sh.getPointsFor(email);
            p.setPoints(points);
            label3.Text = "Очки: " + p.getPoints().ToString();
        }

        //получает список близлежащих игроков с сервера
        //обновляет список игроков на экране
        private void getNearbyPlayers()
        {
            string[] lines = sh.getNearbyPlayers(p.getEmail());
            updatePlayers(lines);
        }


        //обновляет список игроков на экране
        private void updatePlayers(string[] lines)
        {
            listBox1.Items.Clear();
            foreach (string s in lines)
            {
                listBox1.Items.Add(s);
            }

        }

        public void updateCards(string email)
        {
            int[] ss = sh.getCardsFor(email);
            p.clearCards();

            foreach (int z in ss)
            {
                p.addCard(z);
            }
            listBox2.Items.Clear();
            foreach (int s in p.getCards())
            {
                listBox2.Items.Add(s.ToString());
            }


        }
        private void button1_Click(object sender, EventArgs e)
        {

            string mail = textBox1.Text;
            string pass = textBox2.Text;
            string result = ServerHandler.tryAuth(sh, mail, pass);

            if (result != null)
            {
                isOnline = true;
                p = new Player(mail, result);
                this.Text = p.getEmail();
                
                
                
                
                
                richTextBox1.Visible = true;
                
                label3.Visible = true;
                label4.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                button5.Visible = true;
                button6.Visible = true;
                button7.Visible = true;
                button8.Visible = true;
                listBox2.Visible = true;
                lmin = 0;
                lmax = sh.getMaxX();
                tmin = 0;
                tmax = sh.getMaxY();
                listBox1.Visible = true;
                textBox3.Visible = true;
                p.setCoordinates(0, 0);

                sh.addPlayer(p.getEmail(), p.getToken());

                messageLog(mail + " вошел в сеть.");
                
                
                timer1.Start();
                timer2.Start();
                timer3.Start();

                getUpdates(p.getEmail());
                getNearbyPlayers();
                
            }

            else
            {
                MessageBox.Show("Данные для входа неверны");
            }
        }


        //выводит сообщение в лог
        private void messageLog(string s)
        {
            sh.addMessage(s);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            getUpdates(p.getEmail());
        }



        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index != -1)
            {
                string s = (string)listBox1.Items[index];
                messageLog(sh.private_hash + s + "|" + p.getEmail() + ": " + textBox3.Text);
            }
            else
                messageLog(p.getEmail() + ":" + textBox3.Text);
            textBox3.Clear();
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            getNearbyPlayers();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {

            Geo.updateCoordinates(p, label5.Left, label5.Top);
            updateCoordinates();
        }

        private void updateCoordinates()
        {
            sh.changePlayerState(p.getEmail(), p.getX(), p.getY());
            label4.Text = "Координаты: (" + p.getX() + ", " + p.getY();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            label5.Top--;
            shrinkCoordinates();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            label5.Left--;
            shrinkCoordinates();
        }

        private void button7_Click(object sender, EventArgs e)
        {
            label5.Left++;
            shrinkCoordinates();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            label5.Top++;
            shrinkCoordinates();
        }

        private void shrinkCoordinates()
        {
            if (label5.Left < lmin) label5.Left = lmin;
            if (label5.Left > lmax) label5.Left = lmax;
            if (label5.Top < tmin) label5.Top = tmin;
            if (label5.Top > tmax) label5.Top = tmax;


        }

        private void View_Load(object sender, EventArgs e)
        {
            TcpClientChannel channel = new TcpClientChannel();
            ChannelServices.RegisterChannel(channel, true);
            sh = (ITServer.ServerHandler)Activator.GetObject(
                    typeof(ITServer.ServerHandler), "tcp://localhost:9090/xplore.rem");
        }

        private void View_FormClosed(object sender, FormClosedEventArgs e)
        {
            sh.deletePlayer(p.getEmail());
        }

        private void button4_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            if (index == -1) return;
            string otherplayerEmail = (string)listBox1.Items[index];
            int[] otherplayercardsList = sh.getCardsFor(otherplayerEmail);
            listBox3.Visible = true;
            listBox3.Items.Clear();
            foreach (int x in otherplayercardsList)
            {
                listBox3.Items.Add(x.ToString());
            }
            button9.Visible = true;
        }

        private void button9_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex == -1) return;

            if (listBox2.SelectedIndex == -1) return;
            if (listBox3.SelectedIndex == -1) return;

            int what = listBox2.SelectedIndex;
            int for_what = listBox3.SelectedIndex;
            string to = (string)listBox1.Items[listBox1.SelectedIndex];
            string from = p.getEmail();

            
            int from_ = Convert.ToInt32(listBox2.Items[what]);
            int to_ = Convert.ToInt32(listBox3.Items[for_what]);

            if (what == -1 || for_what == -1) return;
            sh.createTrade(from, to, from_, to_);
            listBox3.Visible = false;
            button9.Visible = false;
            MessageBox.Show("Предложение об обмене отправлено!");
        }



      

    }
}
