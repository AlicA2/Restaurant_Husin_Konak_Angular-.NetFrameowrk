using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.ViewModels
{
    public class RezervacijaAddVM
    {
        public string Ime { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public DateTime Vrijeme { get; set; }
        public string Prezime { get; set; }
        public string BrojOsoba { get; set; }
        public string Rezervisano { get; set; }
        public int? korisnik_id { get; set; }

    }
}
