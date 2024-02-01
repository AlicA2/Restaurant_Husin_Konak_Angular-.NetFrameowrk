using FIT_Api_Examples.Helper;
using HusinKonak.Data.Helpers.AutentifikacijaAutorizacija;
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
    public class GalerijaController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public GalerijaController(RestaurantDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<IEnumerable<string>> GetAllSlike()
        {
            var slike = _dbContext.Galerija.ToList();
            if (slike == null)
                throw new Exception("Slike nisu pronađene.");

            var imageUrls = slike.Select(slika => Convert.ToBase64String(slika.slika)).ToList();

            return Ok(imageUrls);
        }

        [HttpGet]
        public ActionResult<int> GetFirstImageId()
        {
            var firstImage = _dbContext.Galerija.FirstOrDefault();

            if (firstImage == null)
            {
                return NotFound("Nijedna slika nije pronađena.");
            }

            return firstImage.ID;
        }



        [HttpPost]
        public ActionResult DodajSliku([FromBody] GalerijaAddVM x)
        {
            if (!string.IsNullOrEmpty(x.novaSlika))
            {
                byte[]? slika_bajtovi = x.novaSlika?.ParsirajBase64();

                if (slika_bajtovi == null)
                    return BadRequest("Format slike nije base64");

                Galerija slikaADD = new Galerija
                {
                    slika = slika_bajtovi
                };
                _dbContext.Galerija.Add(slikaADD);
                _dbContext.SaveChanges();
            }
            return Ok();
        }

        [HttpDelete("{id}")]
        public ActionResult DeleteSlika(int id)
        {
            var slika = _dbContext.Galerija.FirstOrDefault(s => s.ID == id);

            if (slika == null)
            {
                return NotFound("Slika nije pronađena.");
            }

            _dbContext.Galerija.Remove(slika);
            _dbContext.SaveChanges();

            return Ok("Slika je uspješno obrisana.");
        }

    }
}
