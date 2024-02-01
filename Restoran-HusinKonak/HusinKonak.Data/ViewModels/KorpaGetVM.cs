using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.ViewModels
{
    public class KorpaGetVM
    {
        public int Id { get; set; }
        public int? korisnik_id { get; set; }
        public int Kolicina { get; set; }
        public int? meni_id { get; set; }
    }
}
