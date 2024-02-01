using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.ViewModels
{
    public class AdminAddVM
    {
        public int id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string datum_rodjenja { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
        public string adresa { get; set; }
        public string jmbg { get; set; }
        public string spol { get; set; }
        public int grad_ID { get; set; }
        public string datum_zaposlenja { get; set; }
        public string strucna_sprema { get; set; }

        public string korisnickoIme { get; set; }
        public string lozinka { get; set; }
    }
}
