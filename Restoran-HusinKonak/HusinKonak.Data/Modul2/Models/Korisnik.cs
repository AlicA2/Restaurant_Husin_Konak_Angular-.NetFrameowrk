using HusinKonak.Data.Modul0_Autentifikacija.Models;
using System.ComponentModel.DataAnnotations.Schema;

namespace HusinKonak.Data.Modul2.Models
{
    [Table("Korisnik")]
    public class Korisnik : KorisnickiNalog
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Spol { get; set; }


        [ForeignKey(nameof(grad))]
        public int gradid { get; set; }
        public Grad grad { get; set; }
    }

}
