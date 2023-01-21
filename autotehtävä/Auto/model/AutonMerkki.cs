using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autokauppa.model
{
   public class AutonMerkki
    {
        public int ID { get; set; }
        public string Merkki { get; set; }
        public override string ToString()
        {
            return string.Format("ID: {0}, Merkki: {1}", this.ID, this.Merkki);
        }
    }
}
