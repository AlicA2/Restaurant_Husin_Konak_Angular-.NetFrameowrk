using HusinKonak.Data.Modul0_Autentifikacija.Models;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace HusinKonak.Data.Modul2.Models
{
    [Table("Admin")]
    public class Admin : KorisnickiNalog
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }
        public DateTime DatumRodjenja { get; set; }
        public string Telefon { get; set; }
        public string Email { get; set; }
        public string Adresa { get; set; }
        public string JMBG { get; set; }
        public string Spol { get; set; }
        public DateTime DatumZaposlenja { get; set; }
        public string StrucnaSprema { get; set; }

        [ForeignKey(nameof(grad))]
        public int gradid { get; set; }
        public Grad grad { get; set; }
    }
}
