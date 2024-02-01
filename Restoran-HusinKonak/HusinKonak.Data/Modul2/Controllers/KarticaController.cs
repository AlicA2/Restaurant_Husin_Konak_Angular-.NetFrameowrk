using HusinKonak.Data.Modul2.Models;
using HusinKonak.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KarticaController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public KarticaController(RestaurantDBContext context)
        {
            _dbContext = context;
        }
     [HttpPost]
     public ActionResult DodajKarticu([FromBody] KarticaAddVM x)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Neispravni podaci za unos kartice.");
            }

            try
            {

                Kartica kartica = new Kartica
                {
                    TipKartice=x.TipKartice,
                    Ime=x.Ime,
                    Prezime=x.Prezime,
                    Drzava=x.Drzava,
                    Grad=x.Grad,
                    PostanskiBroj= x.PostanskiBroj,
                    BrojKartice=x.BrojKartice,
                    AdresaRacuna=x.AdresaRacuna,
                    Telefon = x.Telefon,
                    SigurnosniKod = x.SigurnosniKod,
                    DatumIsteka = x.DatumIsteka
                };
                _dbContext.Kartica.Add(kartica);
                _dbContext.SaveChanges();
                return Ok(new { message = "Kartica uspješno dodan." });
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dodavanja kartice: {ex.Message}");
            }
        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var kartice = _dbContext.Kartica
                    .Select(x => new KarticaGetVM
                    {
                        Id=x.Id,
                        TipKartice = x.TipKartice,
                        Ime = x.Ime,
                        Prezime = x.Prezime,
                        Drzava = x.Drzava,
                        Grad = x.Grad,
                        PostanskiBroj = x.PostanskiBroj,
                        BrojKartice = x.BrojKartice,
                        AdresaRacuna = x.AdresaRacuna,
                        Telefon = x.Telefon,
                        SigurnosniKod=x.SigurnosniKod,
                        DatumIsteka=x.DatumIsteka
                    })
                    .ToList();

                return Ok(kartice);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dohvatanja korpe: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public ActionResult GetById(int id)
        {
            try
            {
                var kartica = _dbContext.Kartica
                    .Where(x => x.Id == id)
                    .Select(x => new KarticaGetVM
                    {
                        Id = x.Id,
                        TipKartice = x.TipKartice,
                        Ime = x.Ime,
                        Prezime = x.Prezime,
                        Drzava = x.Drzava,
                        Grad = x.Grad,
                        PostanskiBroj = x.PostanskiBroj,
                        BrojKartice = x.BrojKartice,
                        AdresaRacuna = x.AdresaRacuna,
                        Telefon = x.Telefon,
                        SigurnosniKod = x.SigurnosniKod,
                        DatumIsteka = x.DatumIsteka
                    })
                    .SingleOrDefault();

                if (kartica == null)
                {
                    return NotFound("Kartica not found");
                }

                return Ok(kartica);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dohvatanja kartice: {ex.Message}");
            }
        }       
    }
}
