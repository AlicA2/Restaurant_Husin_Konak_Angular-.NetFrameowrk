using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using HusinKonak.Data;
using HusinKonak.Data.Modul2.Models;
using HusinKonak.Data.ViewModels;

namespace HusinKonak.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class DostavaController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public DostavaController(RestaurantDBContext context)
        {
            _dbContext = context;
        }
        [HttpGet]
        public ActionResult<IEnumerable<DostavaGetVM>> GetAll()
        {
            try
            {
                var dostave = _dbContext.Dostava
                    .Select(d => new DostavaGetVM
                    {
                        Id = d.Id,
                        DatumKreiranja = d.DatumKreiranja,
                        TelefonDostave = d.TelefonDostave,
                        AdresaDostave = d.AdresaDostave,
                        korisnik_id = d.korisnik_id,
                        meni_id = d.meni_id,
                        Kolicina=d.Kolicina,
                        kartica_id=d.kartica_id,
                        Cijena=d.Cijena
                    })
                    .ToList();
                return Ok(dostave);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dohvatanja dostava: {ex.Message}");
            }
        }

        [HttpPost]
        public ActionResult DodajDostavu([FromBody] DostavaAddVM dostava)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Neispravni podaci za unos dostave.");
            }

            try
            {
                var newKartica = new Kartica
                {
                    TipKartice = dostava.TipKartice,
                    Ime = dostava.Ime,
                    Prezime = dostava.Prezime,
                    Drzava = dostava.Drzava,
                    Grad = dostava.Grad,
                    PostanskiBroj = dostava.PostanskiBroj,
                    BrojKartice = dostava.BrojKartice,
                    AdresaRacuna = dostava.AdresaRacuna,
                    Telefon = dostava.Telefon,
                    SigurnosniKod = dostava.SigurnosniKod,
                    DatumIsteka = dostava.DatumIsteka
                };

                var newDostava = new Dostava
                {
                    DatumKreiranja = DateTime.Now,
                    TelefonDostave = dostava.TelefonDostave,
                    AdresaDostave = dostava.AdresaDostave,
                    Kolicina = dostava.Kolicina,
                    kartica = newKartica,
                    meni_id=dostava.meni_id,
                    korisnik_id=dostava.korisnik_id,
                    Cijena=dostava.Cijena
                };

                _dbContext.Dostava.Add(newDostava);
                _dbContext.SaveChanges();

                return Ok(new { message = "Dostava uspješno dodana." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dodavanja dostave: {ex.Message}");
            }
        }

    }
}

