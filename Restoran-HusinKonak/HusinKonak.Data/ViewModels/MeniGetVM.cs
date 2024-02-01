using HusinKonak.Data.Modul2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.ViewModels
{
    public class MeniGetVM
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public decimal Cijena { get; set; }
        public int? kategorija_id { get; set; }
        public string SlikaBase64 { get; set; }

    }
}
