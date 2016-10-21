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
        //почта
        string email;
        //очки
        int points = 0;
        //текущие координаты
        int x = 0;
        int y = 0;

        //возвращает email
        public string getEmail()
        {

            return email;
        }



        public Player(string s)
        {
            email = s;

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

        public int getY()
        {
            return y;
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
