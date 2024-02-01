using HusinKonak.Data.Modul2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.ViewModels
{
    public class KorpaAddVM
    {
        public int Kolicina { get; set; }
        public int korisnik_id { get; set; }
        public int meni_id { get; set; }
    }
}
