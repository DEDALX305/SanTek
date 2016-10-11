using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

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


        //получает обновления с сервера
        //обновляет лог
        private void getUpdates(string email)
        {
            string[] lines = ServerHandler.getUpdates(email);
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
            string[] lines = ServerHandler.getNearbyPlayers(p);
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
            bool result = ServerHandler.tryAuth(mail, pass);

            if (result)
            {
                isOnline = true;
                p = new Player(mail);
                p.setCoordinates(234, 567);
                
                richTextBox1.Visible = true;
                panel1.Visible = true;
                label3.Visible = true;
                label4.Visible = true;
                button2.Visible = true;
                button3.Visible = true;
                button4.Visible = true;
                listBox1.Visible = true;
                messageLog("Вы успешно вошли как " + mail + "\n");
                messageLog("Игра началась\n");
                
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
            richTextBox1.AppendText(s);
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            getUpdates(p.getEmail());
        }



        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            string s = (string)listBox1.Items[index];
            messageLog("Вы отправили сообщение " + s + "\n");
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            getNearbyPlayers();
        }

        private void timer3_Tick(object sender, EventArgs e)
        {
            Geo.updateCoordinates(p);
            updateCoordinates();
        }

        private void updateCoordinates()
        {
            label4.Text = "Координаты: (" + p.getX() + ", " + p.getY();
        }
    }
}
