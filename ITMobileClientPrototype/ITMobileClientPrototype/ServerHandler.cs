using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ITServer;
using System.Collections;
using System.IO;
using System.Net;
using System.Net.Sockets;
using System.Threading;

namespace ITMobileClientPrototype
{
    class ServerHandler
    {

        

        //попытка авторизации на сервере игры с данными email:password
        //возвращает true, если авторизация успешна
        public static string tryAuth(ITServer.ServerHandler sh, string email, string password)
        {


            

            return sh.checkCredentials(email, password);
            
        }

    }
}
