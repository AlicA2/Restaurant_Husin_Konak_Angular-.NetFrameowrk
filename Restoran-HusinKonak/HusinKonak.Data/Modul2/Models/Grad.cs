using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace HusinKonak.Data.Modul2.Models
{
    public class Grad
    {
        [Key]
        public int ID { get; set; }
        public string Naziv { get; set; }
        public int PostanskiBroj { get; set; }
        [ForeignKey(nameof(drzava))]
        public int drzavaID { get; set; }
        public Drzava drzava { get; set; }
    }
}
