using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autokauppa.model
{
   public class Polttoaine
    {
        public int ID { get; set; }
        public string Polttoaineen_nimi { get; set; }

        public override string ToString()
        {
            return string.Format("ID: {0}, Polttoaineen_nimi: {1}", this.ID, this.Polttoaineen_nimi);
        }
    }
}
