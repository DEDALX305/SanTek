using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITServer
{
    class Updates
    {

        private string email;
        private string[] messages;

        public Updates(string email, string[] messages)
        {
            this.email = email;
            this.messages = messages;
        }
    }
}
