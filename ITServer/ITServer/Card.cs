using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;

namespace ITServer
{
    public class Card
    {
        private Image image = null;

        private string name;

        private string description;

        private int type;

        public Card(string name, string description, int type)
        {
            this.name = name;
            this.description = description;
            this.type = type;
        }

        public int getType()
        {
            return type;
        }

        public string getName()
        {
            return name;
        }

        public string getDescription()
        {
            return description;
        }


    }
}
