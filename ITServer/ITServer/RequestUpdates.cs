using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITServer
{
    class RequestUpdates
    {
        private string email;
        private string token;

        public RequestUpdates(string email, string token)
        {
            this.email = email;
            this.token = token;

        }
    }
}
