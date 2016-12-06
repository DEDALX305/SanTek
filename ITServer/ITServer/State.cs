using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.IO;


namespace ITServer
{
    class State
    {
        private string email;
        private string token;
        private double x;
        private double y;

        public State(string email, string token, double x, double y)
        {
            this.email = email;
            this.token = token;
            this.x = x;
            this.y = y;
        }



        private void toJSON(string text)
        {
            text = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });

        }

        private static State fromJSON(string text)
        {

            var ds = JsonConvert.DeserializeObject<State>(text, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            return ds;
        }


    }
}
