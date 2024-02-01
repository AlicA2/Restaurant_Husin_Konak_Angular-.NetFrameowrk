using HusinKonak.Data.Modul2.Models;
using HusinKonak.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;


namespace HusinKonak.Data.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AboutMeController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public AboutMeController(RestaurantDBContext dbContext)
        {
            _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        [HttpPost]
        public ActionResult Dodaj([FromBody] AboutMeAddVM x)
        {
            if (x == null)
            {
                return BadRequest("Podaci nisu validni.");
            }
            var aboutMe = new AboutMe
            {
                Title = x.Title,
                Text = x.Text
            };

            _dbContext.AboutMe.Add(aboutMe);
            _dbContext.SaveChanges();

            return Ok(new { message = "Podaci su uspješno dodani." });
        }

        [HttpGet]
        public ActionResult<IEnumerable<AboutMe>> DohvatiSve()
        {
            var aboutMeList = _dbContext.AboutMe.ToList();

            return aboutMeList;
        }


        [HttpDelete("{id}")]
        public ActionResult Izbrisi(int id)
        {
            var aboutMe = _dbContext.AboutMe.FirstOrDefault(a => a.Id == id);

            if (aboutMe == null)
            {
                return NotFound("Podaci nisu pronađeni.");
            }

            _dbContext.AboutMe.Remove(aboutMe);
            _dbContext.SaveChanges();

            return Ok(new { message = "Podaci su uspješno obrisani." });
        }

        [HttpPut("{id}")]
        public ActionResult Uredi(int id, [FromBody] AboutMe x)
        {
            var aboutMe = _dbContext.AboutMe.FirstOrDefault(a => a.Id == id);

            if (aboutMe == null)
            {
                return NotFound("Podaci nisu pronađeni.");
            }

            if (x == null)
            {
                return BadRequest("Podaci nisu validni.");
            }

            aboutMe.Title = x.Title;
            aboutMe.Text = x.Text;

            _dbContext.Entry(aboutMe).State = EntityState.Modified;
            _dbContext.SaveChanges();

            return Ok(new { message = "Podaci su uspješno uređeni." });
        }

    }
}
