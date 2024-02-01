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
        public class KorpaController : ControllerBase
        {
            private readonly RestaurantDBContext _dbContext;
            public KorpaController(RestaurantDBContext dbContext)
            {
                _dbContext = dbContext;
            }

        [HttpPost]
        public ActionResult DodajKorpu([FromBody] KorpaAddVM x)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Neispravni podaci za unos korpe.");
            }

            try
            {

                Models.Korpa korpa = new Models.Korpa
                {
                    Kolicina = x.Kolicina,
                    korisnik_id= x.korisnik_id,
                    meni_id= x.meni_id
                };
                _dbContext.Korpa.Add(korpa);
                _dbContext.SaveChanges();
                return Ok(new { message = "Korpa uspješno dodan." });
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dodavanja korpe: {ex.Message}");
            }

        }

        [HttpGet]
        public ActionResult GetAll()
        {
            try
            {
                var kontakte = _dbContext.Korpa
                    .Select(k => new KorpaGetVM
                    {
                        Id = k.Id,
                        korisnik_id=k.korisnik_id,
                        meni_id=k.meni_id,
                        Kolicina=k.Kolicina,
                    })
                    .ToList();

                return Ok(kontakte);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dohvatanja korpe: {ex.Message}");
            }
        }
        [HttpPut("{id}")]
        public ActionResult UpdateKolicina(int id, [FromBody] KorpaUpdateVM x)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Neispravni podaci za ažuriranje korpe.");
            }

            try
            {
                var korpa = _dbContext.Korpa.Find(id);

                if (korpa == null)
                {
                    return NotFound($"Korpa sa ID-em {id} nije pronađena.");
                }

                korpa.Kolicina = x.Kolicina;
                _dbContext.SaveChanges();

                return Ok(new { message = "Količina uspješno ažurirana." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom ažuriranja količine: {ex.Message}");
            }
        }
        [HttpDelete("{id}")]
        public ActionResult Delete(int id)
        {
            try
            {
                var korpa = _dbContext.Korpa.Find(id);

                if (korpa == null)
                {
                    return NotFound($"Korpa sa ID-em {id} nije pronađena.");
                }

                _dbContext.Korpa.Remove(korpa);
                _dbContext.SaveChanges();

                return Ok(new { message = "Korpa uspješno obrisana." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom brisanja korpa: {ex.Message}");
            }
        }

    }
}
