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
    public class ForumOdgovorController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public ForumOdgovorController(RestaurantDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<ForumOdgovorGetVM>> GetAll()
        {
            var data = _dbContext.ForumOdgovor.Select(o => new ForumOdgovorGetVM()
            {
                id = o.ID,
                odgovor = o.Odgovor,
                autor_name = o.AutorIme,
                datum_kreiranja = o.DatumKreiranja.ToString("yyyy-dd-MM")
            });
            return data.Take(100).ToList();
        }

        [HttpPost]
        public ActionResult Dodaj([FromBody] ForumOdgovorAddVM x)
        {
            ForumOdgovor forumOdgovor = new ForumOdgovor();
            forumOdgovor.Odgovor = x.odgovor;
            forumOdgovor.DatumKreiranja = DateTime.Now;
            forumOdgovor.AutorIme = x.autor_name;
            forumOdgovor.forumTema_id = x.forum_tema_id;

            _dbContext.Add(forumOdgovor);
            _dbContext.SaveChanges();

            return new JsonResult(true);

        }

        [HttpPost]
        public ActionResult Obrisi(int id)
        {
            ForumOdgovor forumOdgovor = _dbContext.ForumOdgovor.FirstOrDefault(f => f.ID == id);
            if (forumOdgovor == null)
                return BadRequest("ne postoji forum odgovor");

            _dbContext.Remove(forumOdgovor);
            _dbContext.SaveChanges();
            return new JsonResult(true);
        }
    }
}
