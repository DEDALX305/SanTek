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
            richTextBox1.Clear();
            string[] lines = sh.getMessages(email);
            performUpdates(lines, p);
            foreach (string s in lines)
            {
                richTextBox1.AppendText(s + "\n");
            }

        }

        //обновляет модель игрока в соответствии с новыми сообщениями с сервера
        private void performUpdates(string[] lines, Player p)
        {
            p.setPoints(p.getPoints() + 10);
            updatePoints();
        }

        //обновляет очки на экране
        private void updatePoints()
        {
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


        private void button1_Click(object sender, EventArgs e)
        {
            string mail = textBox1.Text;
            string pass = textBox2.Text;
            bool result = ServerHandler.tryAuth(sh, mail, pass);

            if (result)
            {
                isOnline = true;
                p = new Player(mail);
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
                lmin = 0;
                lmax = sh.getMaxX();
                tmin = 0;
                tmax = sh.getMaxY();
                listBox1.Visible = true;
                textBox3.Visible = true;
                p.setCoordinates(0, 0);

                sh.addPlayer(p.getEmail());

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

        }

      

    }
}
