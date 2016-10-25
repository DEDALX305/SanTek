using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ITServer
{
    public class TradeRequest
    {
        private bool completed = false;
        private string from_email;
        private string to_email;
        private int from_card;
        private int to_card;
        public TradeRequest(string from_email, string to_email, int from_card, int to_card)
        {
            this.from_card = from_card;
            this.from_email = from_email;
            this.to_card = to_card;
            this.to_email = to_email;
        }

        public void complete()
        {
            completed = true;
        }


        public bool isCompleted()
        {
            return completed;
        }

        public int getTo_card()
        {
            return to_card;
        }

        public int getFrom_card()
        {
            return from_card;
        }

        public string getTo_email()
        {
            return to_email;
        }

        public string getFrom_email()
        {
            return from_email;
        }

    }
}
