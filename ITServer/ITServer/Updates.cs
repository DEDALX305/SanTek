using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.IO;

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

        private void toJSON(string text)
        {
            text = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });


        }

        private static Updates fromJSON(string text)
        {

            var ds = JsonConvert.DeserializeObject<Updates>(text, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            return ds;
        }

    }
}
