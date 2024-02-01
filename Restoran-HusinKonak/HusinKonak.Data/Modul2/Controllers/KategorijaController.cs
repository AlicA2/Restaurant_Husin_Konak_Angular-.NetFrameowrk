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
    public class KategorijaController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public KategorijaController(RestaurantDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult DodajKategoriju([FromBody] KategorijaAddVM x)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Neispravni podaci za unos kategorije.");
            }

            try
            {
                Kategorija kategorija = new Kategorija
                {
                    Naziv=x.Naziv
                };

                _dbContext.Kategorija.Add(kategorija);
                _dbContext.SaveChanges();

                return Ok(new { message = "Kategorija uspješno dodana." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dodavanja kategorije: {ex.Message}");
            }

        }
        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var kategorije = _dbContext.Kategorija
                    .Select(k => new KategorijaGetVM
                    {
                        Id = k.Id, 
                        Naziv = k.Naziv
                    })
                    .ToList();

                return Ok(kategorije);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dohvatanja kategorije: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public ActionResult DeleteKategorija(int id)
        {

            var kategorija = _dbContext.Kategorija.Find(id);
            if (kategorija == null)
            {
                return NotFound($"Kategorija sa ID-om {id} nije pronađen.");
            }



            _dbContext.Kategorija.Remove(kategorija);
            _dbContext.SaveChanges();



            return Ok(new { message = $"Kategorija sa ID-om {id} uspješno obrisan." });

        }
    }
}

