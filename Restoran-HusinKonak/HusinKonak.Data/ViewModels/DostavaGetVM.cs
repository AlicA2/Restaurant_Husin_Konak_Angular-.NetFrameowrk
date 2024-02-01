using HusinKonak.Data.Modul2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.ViewModels
{
    public class DostavaGetVM
    {
        public int Id { get; set; }
        public DateTime DatumKreiranja { get; set; }
        public string TelefonDostave { get; set; }
        public string AdresaDostave { get; set; }
        public int  Kolicina { get; set; }
        public decimal Cijena { get; set; }
        public int?  meni_id { get; set; }
        public int? korisnik_id { get; set; }
        public int? kartica_id { get; set; }
    }
}
