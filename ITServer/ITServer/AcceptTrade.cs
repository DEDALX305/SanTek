using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITServer
{
    class AcceptTrade
    {

        private int trade_state_number;
        private string email_who;
        private string token_who;
        private string email_to;
        private bool status;

        public AcceptTrade(int trade_state_number, string email_who, string token_who, string email_to, bool status)
        {
            this.trade_state_number = trade_state_number;
            this.email_to = email_to;
            this.email_who = email_who;
            this.status = status;
            this.token_who = token_who;
        }


    }
}
