using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITMobileClientPrototype
{
    //Класс хранит модель игрока
    [Serializable]
    public class Player : MarshalByRefObject
    {
        //список карт
        List<int> cardsList = new List<int>();
        //почта
        string email;
        //очки
        int points = 0;
        //текущие координаты
        int x = 0;
        int y = 0;

        private string token;

        //возвращает email
        public string getEmail()
        {

            return email;
        }

        public string getToken(){
            return token;
        }

        public Player(string s, string token)
        {
            email = s;
            this.token = token;

        }

        //устанавливает новые координаты (x, y)
        public void setCoordinates(int x, int y){
            this.x = x;
            this.y = y;
        }

        public int getX()
        {
            return x;
        }

        public int[] getCards()
        {
            return cardsList.ToArray();
        }

        public void addCard(int type){
            cardsList.Add(type);
        }

        public void removeCard(int type)
        {
           
            cardsList.Remove(type);
          
        }

        public int getY()
        {
            return y;
        }

        public void clearCards(){
            cardsList = new List<int>();
        }

        public int getPoints()
        {
            return points;
        }

        public void setPoints(int pp)
        {
            points = pp;
        }

       
    }
}
