using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Modul2.Models
{
    [Table("Meni")]
    public class Meni
    {
        public int Id { get; set; }
        public string Naziv { get; set; }
        public string Opis { get; set; }
        public decimal Cijena { get; set; }
        public byte[] Slika { get; set; }
        [ForeignKey(nameof(kategorija))]
        public int? kategorija_id { get; set; }
        public Kategorija kategorija { get; set; }


    }
}
