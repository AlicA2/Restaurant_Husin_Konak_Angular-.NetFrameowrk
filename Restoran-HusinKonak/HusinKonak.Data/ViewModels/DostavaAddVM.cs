using HusinKonak.Data.Modul2.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.ViewModels
{
    public class DostavaAddVM
    {
        public string TelefonDostave { get; set; }
        public string AdresaDostave { get; set; }
        public int? meni_id { get; set; }
        public int Kolicina { get; set; }
        public int? korisnik_id { get; set; }
        public int? kartica_id { get; set; }
        public decimal Cijena { get; set; }


        public string? TipKartice { get; set; }
        public string? BrojKartice { get; set; }
        public string? Ime { get; set; }
        public string? Prezime { get; set; }
        public string? AdresaRacuna { get; set; }
        public string? Grad { get; set; }
        public string? Drzava { get; set; }
        public string? Telefon { get; set; }
        public string? PostanskiBroj { get; set; }
        public string? DatumIsteka { get; set; }
        public string? SigurnosniKod { get; set; }
    }
}
