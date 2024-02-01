using FIT_Api_Examples.Helper;
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
    public class MeniController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public MeniController(RestaurantDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult DodajMeni([FromBody]MeniAddVM x)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Neispravni podaci za unos meni-a.");
            }

            try
            {
                if (!string.IsNullOrEmpty(x.SlikaBase64))
                {
                    byte[]? slika_bajtovi = x.SlikaBase64?.ParsirajBase64();

                    if (slika_bajtovi == null)
                        return BadRequest("Format slike nije base64");

                    Meni meni = new Meni
                    {
                        Naziv = x.Naziv,
                        Opis = x.Opis,
                        Cijena = x.Cijena,
                        kategorija_id = x.kategorija_id,
                        Slika = slika_bajtovi
                    };
                    _dbContext.Meni.Add(meni);
                    _dbContext.SaveChanges();

                }
                return Ok(new { message = "Meni uspješno dodan." });

            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dodavanja meni-a: {ex.Message}");
            }

        }
        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAll()
        {
            try
            {
                var meni = _dbContext.Meni
                    .Select(k => new MeniGetVM
                    {
                        Id = k.Id,
                        Naziv=k.Naziv,
                        Opis = k.Opis,
                        Cijena = k.Cijena,
                        kategorija_id = k.kategorija_id,
                        SlikaBase64 =  Convert.ToBase64String(k.Slika) // Konvertuj sliku u URL u formatu Base64
                    })
                    .ToList();

                return Ok(meni);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dohvatanja meni-a: {ex.Message}");
            }
        }
        [HttpGet("{id}")]
        public ActionResult<MeniGetVM> GetById(int id)
        {
            try
            {
                var meni = _dbContext.Meni
                    .Where(k => k.Id == id)
                    .Select(k => new MeniGetVM
                    {
                        Id = k.Id,
                        Naziv = k.Naziv,
                        Opis = k.Opis,
                        Cijena = k.Cijena,
                        kategorija_id = k.kategorija_id,
                        SlikaBase64 = Convert.ToBase64String(k.Slika) 
            })
                    .FirstOrDefault();

                if (meni == null)
                {
                    return NotFound(); 
                }

                return Ok(meni);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dohvatanja meni-a: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteMeni(int id)
        {

            var meni = _dbContext.Meni.Find(id);
            if (meni == null)
            {
                return NotFound($"Meni sa ID-om {id} nije pronađen.");
            }



            _dbContext.Meni.Remove(meni);
            _dbContext.SaveChanges();



            return Ok(new { message = $"Meni sa ID-om {id} uspješno obrisan." });

        }
    }
}
