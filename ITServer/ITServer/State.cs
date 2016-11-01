using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

    }
}
