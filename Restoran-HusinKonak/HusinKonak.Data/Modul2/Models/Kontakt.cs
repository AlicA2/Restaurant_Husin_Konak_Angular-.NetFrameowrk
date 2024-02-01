using HusinKonak.Data.Modul2.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

public class Kontakt
{
    [Key]
    public int ID { get; set; }
    public string Ime { get; set; }
    public string Prezime { get; set; }
    public string Email { get; set; }
    public string Telefon { get; set; }
    public string Poruka { get; set; }

    [ForeignKey(nameof(korisnik))]
    public int korisnikID { get; set; }
    public Korisnik korisnik { get; set; }
}
