using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITMobileClientPrototype;
using System.Drawing;

namespace ITServer
{
    [Serializable]
    public class ServerHandler : MarshalByRefObject
    {

        public List<Player> playersList = new List<Player>();
        public List<string> messagesList = new List<string>();
        public string private_hash = "337711732STOP|";

        private bool isNearby(Player p1, Player p2)
        {
            if (Math.Sqrt((p2.getX() - p1.getX()) * (p2.getX() - p1.getX()) + (p2.getY() - p1.getY()) * (p2.getY() - p1.getY())) < 5)
                return true;
            else
                return false;

        }

        public bool checkCredentials(string email, string password)
        {
            return true;
        }

        public int getMaxX()
        {
            Image m = Image.FromFile("map.jpg");
            return m.Width;
        }

        public int getMaxY()
        {
            Image m = Image.FromFile("map.jpg");
            return m.Height;
        }

        public void addMessage(string s){
            messagesList.Add(s);
        }

        public void deletePlayer(string email)
        {
            int x = -1;

            for (int i = 0; i < playersList.Count; i++)
            {
                if (playersList[i].getEmail() == email) x = i;
            }

            playersList.RemoveAt(x);
        }

        public string[] getNearbyPlayers(string email)
        {
            List<string> l = new List<string>();
            
            Player p1 = null;

            foreach (Player p in playersList)
            {
                if (p.getEmail() == email) p1 = p;
            }
            foreach (Player p in playersList)
            {
                if (p.getEmail() != email && isNearby(p, p1))
                l.Add(p.getEmail());
            }
            return l.ToArray();
        }

        public string[] getMessages(string email)
        {
            List<string> l = new List<string>();
            foreach (string s in messagesList)
            {
                if (s.StartsWith(private_hash)){
                    string[] lines = s.Split('|');
                    string whom = lines[1];
                    if (email == whom)
                    {
                        l.Add(lines[2]);
                    }
                }
                else

                l.Add(s);
            }
            return l.ToArray();
        }

        public void addPlayer(string email)
        {
            playersList.Add(new Player(email));
            
        }

        public void sendMessageTo(string who, string whom)
        {
            //messagesList.Add(p.getEmail() + ":" + s);

        }

        public void changePlayerState(string email, int x, int y)
        {
            foreach (Player p in playersList)
            {
                if (p.getEmail() == email)
                {
                    p.setCoordinates(x, y);
                }
            }
        }

         
    }
}
