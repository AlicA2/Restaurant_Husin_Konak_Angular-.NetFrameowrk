using HusinKonak.Data.Modul0_Autentifikacija.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Modul2.Models
{
    public class ForumTema
    {
        public int ID { get; set; }
        public string Tema { get; set; }
        public string Pitanje { get; set; }
        public DateTime DatumKreiranja { get; set; }

        [ForeignKey(nameof(korisnickiNalog))]
        public int korisnickiNalogID { get; set; }
        public KorisnickiNalog korisnickiNalog { get; set; }
    }
}
