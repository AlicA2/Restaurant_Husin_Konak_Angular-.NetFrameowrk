using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Modul2.Models
{
    public class Korpa
    {
        public int Id { get; set; }
        public int Kolicina { get; set; }
        [ForeignKey(nameof(Meni))]
        public int? meni_id { get; set; }
        public Meni Meni { get; set; }
        [ForeignKey(nameof(korisnik))]
        public int? korisnik_id { get; set; }
        public Korisnik korisnik { get; set; }
    }
}
