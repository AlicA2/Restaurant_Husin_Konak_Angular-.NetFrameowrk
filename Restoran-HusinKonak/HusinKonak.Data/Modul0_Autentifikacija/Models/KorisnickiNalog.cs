using HusinKonak.Data.Modul2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HusinKonak.Data.Modul0_Autentifikacija.Models
{
    [Table("KorisnickiNalog")]
    public class KorisnickiNalog
    {
        [Key]
        public int ID { get; set; }
        public string KorisnickoIme { get; set; }
        [JsonIgnore]
        public string Lozinka { get; set; }

        [JsonIgnore]
        public Admin? admin => this as Admin;
        [JsonIgnore]
        public Korisnik? korisnik => this as Korisnik;


        public bool isAdmin => admin != null;
        public bool isKorisnik => korisnik != null;

        public bool isAktiviran { get; set; }
        public string? aktivacijaGUID { get; set; }
    }
}
