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
using ITMobileClientPrototype;

namespace ITServer
{
    public partial class Form1 : Form
    {
        ServerHandler sh;
        private Graphics GFX;
        public Image mapImage = Image.FromFile("map.jpg");
        
        public Form1()
        {
            InitializeComponent();
            
            GFX = this.CreateGraphics();
            sh = new ServerHandler();

            TcpServerChannel channel = new TcpServerChannel(9090);
            ChannelServices.RegisterChannel(channel, true);
            RemotingServices.Marshal(sh, "xplore.rem");

            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (Player p in sh.playersList)
            {
                listBox1.Items.Add(p.getEmail() + " " + p.getX() + " " + p.getY());
                
            }
            richTextBox1.Clear();
            foreach (string s in sh.messagesList)
            {
                richTextBox1.AppendText(s + "\n");
            }

            this.Invalidate();
            
        }



        protected override void OnPaint(PaintEventArgs e)
        {
            // The forms graphics object
            Graphics g = e.Graphics;

            // Portion of original image to display
            
            Rectangle region = new Rectangle(0, 0, mapImage.Height, mapImage.Width);
            mapImage = Image.FromFile("map.jpg");
            Bitmap p = new Bitmap(mapImage);
            Image cross = Image.FromFile("cross.png");
            Bitmap c = new Bitmap(cross);
            
                    foreach (Player pl in sh.playersList)
                    {
                        int x = pl.getX();
                        int y = pl.getY();
                        for (int i = 0; i < c.Width; i++)
                        {
                            for (int j = 0; j < c.Height; j++)
                            {
                                p.SetPixel(x + i, y + j, c.GetPixel(i, j));
                            }

                        }
                    }

                    mapImage = (Image)p;
            g.DrawImage(mapImage, 0, 0, region, GraphicsUnit.Pixel);

            
           
            base.OnPaint(e);


            
        }
    }
}
