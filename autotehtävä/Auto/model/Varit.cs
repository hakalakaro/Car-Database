using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autokauppa.model
{
   public class Varit
    {
        public int ID { get; set; }
        public string Varin_nimi { get; set; }

        public override string ToString()
        {
            return string.Format("ID: {0}, Varin_nimi: {1}", this.ID, this.Varin_nimi);
        }
    }
}
