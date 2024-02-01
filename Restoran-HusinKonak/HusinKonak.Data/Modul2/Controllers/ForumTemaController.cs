using HusinKonak.Data.Modul2.Models;
using HusinKonak.Data.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class ForumTemaController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public ForumTemaController(RestaurantDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet]
        public ActionResult<List<ForumTemaGetAllVM>> GetAll()
        {
            var data = _dbContext.ForumTema
                .Include(f => f.korisnickiNalog).Select(f => new ForumTemaGetAllVM()
                {
                    id = f.ID,
                    tema = f.Tema,
                    pitanje = f.Pitanje,
                    datum_kreiranja = f.DatumKreiranja.ToString("yyyy-dd-MM"),
                    autor = f.korisnickiNalog.KorisnickoIme
                });

            return data.Take(100).ToList();
        }

        [HttpGet]
        public ActionResult<ForumTemaOdgovorGetVM> GetPorukaOdgovori(int id)
        {
            var tema = _dbContext.ForumTema.Include(k => k.korisnickiNalog).FirstOrDefault(z => z.ID == id);
            if (tema == null)
                return BadRequest("ne postoji tema");
            var obj = new ForumTemaOdgovorGetVM();

            obj.tema = tema.Tema;
            obj.pitanje = tema.Pitanje;
            obj.datum_kreiranja = tema.DatumKreiranja.ToString("yyyy-dd-MM");
            obj.kreator = tema.korisnickiNalog.KorisnickoIme;
            obj.forum_odgovori = _dbContext.ForumOdgovor.Where(k => k.forumTema_id == tema.ID).Select(k => new ForumOdgovorGetVM()
            {
                id = k.ID,
                odgovor = k.Odgovor,
                autor_name = k.AutorIme,
                datum_kreiranja = k.DatumKreiranja.ToString("yyyy-dd-MM")
            }).ToList();


            return obj;

        }

        [HttpPost]
        public ActionResult Dodaj([FromBody] ForumTemaAddVM x)
        {
            ForumTema tema = new ForumTema();
            tema.Tema = x.tema;
            tema.Pitanje = x.pitanje;
            tema.DatumKreiranja = DateTime.Now;
            tema.korisnickiNalogID = x.korsinciki_nalog_id;

            _dbContext.ForumTema.Add(tema);
            _dbContext.SaveChanges();
            return Ok(x);
        }

        [HttpPost]
        public ActionResult Obrisi(int id)
        {
            ForumTema tema = _dbContext.ForumTema.FirstOrDefault(p => p.ID == id);
            if (tema == null)
            {
                return BadRequest("ne postoji ta tema");
            }
            _dbContext.Remove(tema);
            _dbContext.SaveChanges();
            return new JsonResult(true);
        }
    }
}
