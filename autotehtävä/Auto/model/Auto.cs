using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Autokauppa.model
{

    public class Auto
    {
        public int ID { get; set; } // "Auto" tables column
        public decimal Hinta { get; set; } // "Auto" tables column
        public DateTime Rekisteri_paivamaara { get; set; } // "Auto" tables column
        public decimal Moottorin_tilavuus { get; set; } // "Auto" tables column
        public int Mittarilukema { get; set; } // "Auto" tables column
        public int AutonMerkkiID { get; set; } // "Auto" tables column
        public int AutonMalliID { get; set; } // "Auto" tables column
        public int VaritID { get; set; } // "Auto" tables column
        public int PolttoaineID { get; set; } // "Auto" tables column
        public string Auton_mallin_nimi { get; set; }
        public string Merkki { get; set; }
        public override string ToString()
        {
            return string.Format("ID: {0}, Hinta: {1}, Rekisteri_paivamaara: {2}, Moottorin_tilavuus: {3}, Mittarilukema: {4}, Merkki: {5}, Auton_mallin_nimi: {6}, VaritID: {7}, PolttoaineID: {8}",
            this.ID, this.Hinta, this.Rekisteri_paivamaara, this.Moottorin_tilavuus, this.Mittarilukema, this.Merkki, this.Auton_mallin_nimi, this.VaritID, this.PolttoaineID);
        }

    }

}
