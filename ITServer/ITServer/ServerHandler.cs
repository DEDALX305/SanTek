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
        public List<Card> cardsList = new List<Card>();
        public List<TradeRequest> trades = new List<TradeRequest>();

        public static int maxRegions = 4;
        public static int maxCardTypes = 9;

        public string private_hash = "337711732STOP|";
        public string trade_hash = "76345364564562STOP|";


        private bool isNearby(Player p1, Player p2)
        {
            if (Math.Sqrt((p2.getX() - p1.getX()) * (p2.getX() - p1.getX()) + (p2.getY() - p1.getY()) * (p2.getY() - p1.getY())) < 5)
                return true;
            else
                return false;

        }

        public string checkCredentials(string email, string password)
        {
            return email + password;
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

        public int getPointsFor(string email){
            foreach (Player p in playersList)
            {
                if (p.getEmail() == email) return p.getPoints();
            }
            return -1;
            
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

        public void addPlayer(string email, string token)
        {
            playersList.Add(new Player(email, token));
            
        }

        public void addCardTo(string email, int type)
        {
            foreach (Player p in playersList)
            {
                if (p.getEmail() == email)
                {
                    p.addCard(type);
                }

            }


        }

        public void removeCardFrom(string email, int type)
        {

            
            foreach (Player p in playersList)
            {
                if (p.getEmail() == email)
                {
                    p.removeCard(type);
                }

            }


        }

        public int[] getCardsFor(string email)
        {
            foreach (Player p in playersList)
            {
                if (p.getEmail() == email)
                {
                    return p.getCards();
                }

            }
            return null;
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

        public void createTrade(string from_email, string to_email, int from_card, int to_card){
            trades.Add(new TradeRequest(from_email, to_email, from_card, to_card));
            
        }

         public void completeTrade(string from_email, string to_email, int from_card, int to_card){
             
                     
                    string s = trade_hash + from_email + "|" + to_email + "|" + from_card.ToString() + "|" + to_card.ToString();
                    messagesList.Remove(s);
                    foreach (TradeRequest tr in trades)
                    {
                        if (tr.getTo_email() == to_email && tr.getFrom_email() == from_email
                            && tr.getFrom_card() == from_card && tr.getTo_card() == to_card)
                        {
                            tr.complete();
                        }
                    }
                     
                 
             
        }


        public int getRegion(int x, int y)
        {
            double s1 = (double)(getMaxX() / 2);
            double s2 = (double)(getMaxY() / 2);
            
            if (x < s1 && y < s2) return 1;
            if (x > s1 && y > s2) return 2;
            if (x < s1 && y > s2) return 3;
            if (x > s1 && y < s2) return 4;
            return 1;
        }
    }
}
