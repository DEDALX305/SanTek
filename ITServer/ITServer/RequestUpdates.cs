using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization.Formatters.Binary;
using Newtonsoft.Json;
using System.IO;

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

        private void toJSON(string text)
        {
            text = JsonConvert.SerializeObject(this, Newtonsoft.Json.Formatting.Indented, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });


        }

        private static RequestUpdates fromJSON(string text)
        {

            var ds = JsonConvert.DeserializeObject<RequestUpdates>(text, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.All
            });
            return ds;
        }


    }
}
