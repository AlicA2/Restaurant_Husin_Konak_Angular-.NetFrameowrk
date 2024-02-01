using HusinKonak.Data.Modul2.Models;
using HusinKonak.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace HusinKonak.Data.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class RezervacijaController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public RezervacijaController(RestaurantDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        private bool DaLiJeDostignutMaksimum(DateTime datumRezervacije, DateTime vrijeme)
        {
            var brojRezervacija = _dbContext.Rezervacije
                .Where(r => r.DatumRezervacije.Date == datumRezervacije.Date && r.Vrijeme == vrijeme)
                .Count();

            return brojRezervacija >= 3;
        }


        [HttpPost]
        public ActionResult DodajRezervaciju([FromBody] RezervacijaAddVM novaRezervacija)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest("Neispravni podaci za unos rezervacije.");
            }

            try
            {
                DateTime datumRezervacije = novaRezervacija.DatumRezervacije;
                DateTime vrijeme = novaRezervacija.Vrijeme;

                if (DaLiJeDostignutMaksimum(datumRezervacije, vrijeme))
                {
                    return BadRequest("Dostignut je maksimalni broj rezervacija za odabrani datum i vrijeme.");
                }
                Models.Rezervacija rezervacija = new Models.Rezervacija
                {
                    Ime=novaRezervacija.Ime,
                    Prezime=novaRezervacija.Prezime,
                    DatumRezervacije=novaRezervacija.DatumRezervacije,
                    BrojOsoba=novaRezervacija.BrojOsoba,
                    Vrijeme=novaRezervacija.Vrijeme,
                    Rezervisano=novaRezervacija.Rezervisano,
                    korisnik_id=novaRezervacija.korisnik_id
                };
                _dbContext.Rezervacije.Add(rezervacija);
                _dbContext.SaveChanges();
                return Ok(new { message = "Rezervacija uspješno dodana." });
            }

            catch (Exception ex)
            {
                return StatusCode(500, $"Greška prilikom dodavanja rezervacije: {ex.Message}");
            }
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Rezervacija>>> SveRezervacije()
        {
            try
            {
                var sveRezervacije = await _dbContext.Rezervacije.ToListAsync();

                return Ok(sveRezervacije);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greska prilikom dohvatanja rezervacija: {ex.Message}");
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Rezervacija>> GetById(int id)
        {
            try
            {
                var rezervacija = await _dbContext.Rezervacije
                    .Where(r => r.ID == id)
                    .FirstOrDefaultAsync();

                if (rezervacija == null)
                {
                    return NotFound();
                }

                return Ok(rezervacija);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Greska prilikom dohvatanja rezervacije: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> ObrisiRezervaciju(int id)
        {
            var rezervacija = await _dbContext.Rezervacije.FindAsync(id);

            if (rezervacija == null)
            {
                return NotFound($"Rezervacija sa ID-om {id} nije pronadjena.");
            }

            _dbContext.Rezervacije.Remove(rezervacija);
            await _dbContext.SaveChangesAsync();

            return Ok(new { message = $"Rezervacija sa ID-om {id} uspjesno obrisana." });
        }
        [HttpPut("{id}")]
        public IActionResult UpdateRezervisano(int id, [FromBody] RezervacijaUpdateVM updateVM)
        {
            try
            {
                var rezervacija = _dbContext.Rezervacije.Find(id);

                if (rezervacija == null)
                {
                    return NotFound($"Rezervacija with ID {id} not found.");
                }

                rezervacija.Rezervisano = updateVM.Rezervisano; 

                _dbContext.Rezervacije.Update(rezervacija);
                _dbContext.SaveChanges();

                return Ok(new { message = $"Rezervisano status for Rezervacija with ID {id} updated to {updateVM.Rezervisano}." });
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error updating Rezervisano status: {ex.Message}");
            }
        }
        [HttpGet("GetByKorisnikId/{korisnikId}")]
        public async Task<ActionResult<IEnumerable<int>>> GetRezervacijaIdsByKorisnikId(int korisnikId)
        {
            try
            {
                var rezervacijaIds = await _dbContext.Rezervacije
                    .Where(r => r.korisnik_id == korisnikId)
                    .ToListAsync();

                return Ok(rezervacijaIds);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Error retrieving Rezervacija IDs for Korisnik ID {korisnikId}: {ex.Message}");
            }
        }
    }
}
