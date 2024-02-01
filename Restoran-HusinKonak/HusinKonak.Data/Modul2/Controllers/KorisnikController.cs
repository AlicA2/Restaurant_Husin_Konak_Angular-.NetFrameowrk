using HusinKonak.Data.Helpers.AutentifikacijaAutorizacija;
using HusinKonak.Data.Helpers;
using HusinKonak.Data.Modul2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using HusinKonak.Data.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace HusinKonak.Data.Modul2.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class KorisnikController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public KorisnikController(RestaurantDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpGet("{guid}")]
        public ActionResult Aktivacija(string guid)
        {
            var korisnik = _dbContext.Korisnik.FirstOrDefault(s => s.aktivacijaGUID == guid);
            if (korisnik != null)
            {
                korisnik.isAktiviran = true;
                _dbContext.SaveChanges();
                return Redirect("http://localhost:4200/korisnici");
            }

            return BadRequest("pogresan URL");
        }

        [HttpGet]
        public ActionResult<List<KorisnikGetVM>> GetAll()
        {
            var data = _dbContext.Korisnik
                .Include(k => k.grad.drzava)
                .Select(k => new KorisnikGetVM
                {
                    id = k.ID,
                    ime = k.Ime,
                    prezime = k.Prezime,
                    telefon = k.Telefon,
                    email = k.Email,
                    spol = k.Spol,
                    grad = k.grad.Naziv + "(" + k.grad.drzava.Naziv + ")"

                });
            return data.Take(100).ToList();
        }


        [HttpGet]
        public ActionResult<KorisnikAddVM> GetById(int id)
        {
            Korisnik korisnik = _dbContext.Korisnik.FirstOrDefault(k => k.ID == id);
            if (korisnik == null)
                return BadRequest("Ne postoji korisnik");
            KorisnikAddVM korisnikGetVM = new KorisnikAddVM
            {
                id = korisnik.ID,
                ime = korisnik.Ime,
                prezime = korisnik.Prezime,
                telefon = korisnik.Telefon,
                email = korisnik.Email,
                spol = korisnik.Spol,
                grad_ID = korisnik.gradid,

                korisnickoIme = korisnik.KorisnickoIme,
                lozinka = korisnik.Lozinka

            };
            return korisnikGetVM;
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] KorisnikAddVM x)
        {
            Korisnik? korisnik;
            if (x.id == 0)
            {
                korisnik = new Korisnik();
                korisnik.aktivacijaGUID = Guid.NewGuid().ToString();
            }
            else
            {
                korisnik = _dbContext.Korisnik.FirstOrDefault(k => k.ID == x.id);
                if (korisnik == null)
                    return BadRequest("Korisnik ne postoji");
            }
            korisnik.ID = x.id;
            korisnik.Ime = x.ime;
            korisnik.Prezime = x.prezime;
            korisnik.Email = x.email;
            korisnik.Spol = x.spol;
            korisnik.Telefon = x.telefon;
            korisnik.gradid = x.grad_ID;

            korisnik.KorisnickoIme = x.korisnickoIme;
            korisnik.Lozinka = x.lozinka;



            if (x.id == 0)
            {
                List<Admin> adminList = _dbContext.Admin.ToList();
                foreach (var a in adminList)
                {
                    if (korisnik.Lozinka == a.Lozinka && korisnik.KorisnickoIme == a.KorisnickoIme)
                        return BadRequest("Korisnicko ime i lozinka vec postoje");
                }

                List<Korisnik> korisnici = _dbContext.Korisnik.ToList();
                foreach (var k in korisnici)
                {
                    if (korisnik.KorisnickoIme == k.KorisnickoIme && korisnik.Lozinka == k.Lozinka)
                        return BadRequest("Korisnicko ime i lozinka vec postoje");
                }

                _dbContext.Korisnik.Add(korisnik);
            }
            EmailLog.noviKorisnik(korisnik, HttpContext);

            _dbContext.SaveChanges();
            return Ok(korisnik);
        }

        [HttpPost("{id}")]
        public ActionResult Obrisi(int id)
        {
            Korisnik? korisnik = _dbContext.Korisnik.Find(id);

            if (korisnik == null)
                return BadRequest("pogresan ID");

            _dbContext.Remove(korisnik);

            _dbContext.SaveChanges();
            return Ok(korisnik);
        }
    }
}
