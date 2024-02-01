using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Modul2.Models
{
    public class Drzava
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public string? Skracenica { get; set; }
    }
}
