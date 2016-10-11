using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITMobileClientPrototype
{
    class ServerHandler
    {

        //попытка авторизации на сервере игры с данными email:password
        //возвращает true, если авторизация успешна
        public static bool tryAuth(string email, string password)
        {
            return checkCredentials(email, password);
            
        }

        //авторизует пару пароль:email на сервере
        private static bool checkCredentials(string email, string password)
        {
            //отправляет credentials на сервер и авторизуется или нет
            //...
            if (email == "drizhiloda@gmail.com") return true;
            else
                return false;
        }

        //получает с сервера сообщения для пользователя email
        public static string[] getUpdates(string email)
        {
            string[] s = checkUpdates(email);
            return s;

        }

        //возвращает с сервера список игроков рядом с игроком p
        public static string[] getNearbyPlayers(Player p)
        {
            int x = p.getX();
            int y = p.getY();
            return getPlayersAt(x, y);
            

        }


        //запрашивает обновления с сервера для игрока email
        //...
        private static string[] checkUpdates(string email)
        {
            return new string[2] {"update1", "update2"};
        }


        //запрос с сервера списка игроков рядом
        //...
        private static string[] getPlayersAt(int x, int y)
        {
            return new string[2] { "abc@mail.xyz", "hohohohahaha@gmail.com" };
        }


    }
}
