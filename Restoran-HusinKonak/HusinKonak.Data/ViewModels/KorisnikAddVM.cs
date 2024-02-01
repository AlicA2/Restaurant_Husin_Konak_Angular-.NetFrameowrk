using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.ViewModels
{
    public class KorisnikAddVM
    {
        public int id { get; set; }
        public string ime { get; set; }
        public string prezime { get; set; }
        public string telefon { get; set; }
        public string email { get; set; }
        public string spol { get; set; }
        public int grad_ID { get; set; }


        public string korisnickoIme { get; set; }
        public string lozinka { get; set; }
    }
}
