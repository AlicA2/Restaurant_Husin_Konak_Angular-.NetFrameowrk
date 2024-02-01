using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Modul2.Models
{
    public class Kartica
    {
        public int Id { get; set; }
        public string TipKartice { get; set; }
        public string BrojKartice { get; set; }
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string AdresaRacuna { get; set; }
        public string Grad { get; set; }
        public string Drzava { get; set; }
        public string Telefon { get; set; }
        public string PostanskiBroj { get; set; }
        public string DatumIsteka { get; set; }
        public string SigurnosniKod { get; set; }
    }
}
