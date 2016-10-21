using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITMobileClientPrototype
{
    //Модуль работы с GPS
    class Geo
    {
        //обновляет текущие координаты игрока на карте
        public static void updateCoordinates(Player p)
        {
            Random r = new Random();
            int x1 = r.Next(1000);
            int x2 = r.Next(1000);
            p.setCoordinates(x1, x2);
        }

        public static void updateCoordinates(Player p, int x, int y)
        {
            
            p.setCoordinates(x, y);
        }

    }
}
