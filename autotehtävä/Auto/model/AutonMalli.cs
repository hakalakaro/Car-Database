using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autokauppa.model
{
   public class AutonMalli
    {
        public int ID { get; set; }
        public string Auton_mallin_nimi { get; set; }
        public int AutonMerkkiID { get; set; }
        public override string ToString()
        {
            return string.Format("ID: {0}, Auton_mallin_nimi: {1}, AutonMerkkiID: {2}", this.ID, this.Auton_mallin_nimi, this.AutonMerkkiID);
        }
    }
}
