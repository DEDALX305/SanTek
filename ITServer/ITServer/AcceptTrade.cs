using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.IO;

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


        private void toJSON(string text)
        {
            text = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

           
        }

        private static AcceptTrade fromJSON(string text)
        {
            
            var ds = JsonConvert.DeserializeObject<AcceptTrade>(text, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            return ds;
        }


    }
}
