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
    public class DrzavaController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;
        public DrzavaController(RestaurantDBContext dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<DrzavaGetAllVM>> GetAll()
        {
            var data = _dbContext.Drzava
                .Select(d => new DrzavaGetAllVM
                {
                    id = d.ID,
                    naziv = d.Naziv,
                    skracenica = d.Skracenica

                });
            return data.Take(100).ToList();
        }

        [HttpPost]
        public Drzava Snimi([FromBody] DrzavaSnimiVM x)
        {
            Drzava? objekat;

            if (x.id == 0)
            {
                objekat = new Drzava();
                _dbContext.Add(objekat);
            }
            else
            {
                objekat = _dbContext.Drzava.Find(x.id);
            }

            objekat.Naziv = x.naziv;
            objekat.Skracenica = x.skracenica;

            _dbContext.SaveChanges();
            return objekat;
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Drzava? drzava = _dbContext.Drzava.Find(id);

            if (drzava == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(drzava);

            _dbContext.SaveChanges();
            return Ok(drzava);
        }
    }
}
