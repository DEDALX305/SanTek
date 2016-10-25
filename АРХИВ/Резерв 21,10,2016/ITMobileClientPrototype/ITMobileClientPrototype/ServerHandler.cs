using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITServer;

namespace ITMobileClientPrototype
{
    class ServerHandler
    {

        

        //попытка авторизации на сервере игры с данными email:password
        //возвращает true, если авторизация успешна
        public static bool tryAuth(ITServer.ServerHandler sh, string email, string password)
        {
            return sh.checkCredentials(email, password);
            
        }

       

       

        


       


       


    }
}
