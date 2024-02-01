using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Modul2.Models
{
    public class Rezervacija
    {
        public int ID { get; set; }
        public string Ime { get; set; }
        public DateTime DatumRezervacije { get; set; }
        public DateTime Vrijeme { get; set; }
        public string Prezime { get; set; }
        public string BrojOsoba { get; set; }
        public string Rezervisano { get; set; }
        [ForeignKey(nameof(korisnik))]
        public int? korisnik_id { get; set; }
        public Korisnik korisnik { get; set; }
    }
}
