using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Modul2.Models
{
    public class Dostava
    {
        public int Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public decimal Cijena { get; set; }
        public string TelefonDostave { get; set; }
        public string AdresaDostave { get; set; }

        [ForeignKey(nameof(korisnik))]
        public int? korisnik_id { get; set; }
        public Korisnik korisnik { get; set; }
        public int Kolicina { get; set; }
        public int? meni_id { get; set; }
        [ForeignKey(nameof(kartica))]
        public int? kartica_id { get; set; }
        public Kartica? kartica { get; set; }

    }
}
