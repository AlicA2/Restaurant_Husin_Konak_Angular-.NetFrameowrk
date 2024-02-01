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
    public class AdminController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public AdminController(RestaurantDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult Snimi([FromBody] AdminAddVM x)
        {
            Admin? admin;
            if (x.id == 0)
            {
                admin = new Admin();
            }
            else
            {
                admin = _dbContext.Admin.FirstOrDefault(a => a.ID == x.id);
                if (admin == null)
                    return BadRequest("Ne postoji admin");
            }
            admin.ID = x.id;
            admin.Ime = x.ime;
            admin.Prezime = x.prezime;
            admin.DatumRodjenja = DateTime.Parse(x.datum_rodjenja);
            admin.Telefon = x.telefon;
            admin.Email = x.email;
            admin.Adresa = x.adresa;
            admin.JMBG = x.jmbg;
            admin.Spol = x.spol;
            admin.DatumZaposlenja = DateTime.Parse(x.datum_zaposlenja);
            admin.StrucnaSprema = x.strucna_sprema;
            admin.gradid = x.grad_ID;
            admin.KorisnickoIme = x.korisnickoIme;
            admin.Lozinka = x.lozinka;

            if (x.id == 0)
            {
                List<Admin> adminList = _dbContext.Admin.ToList();
                foreach (var a in adminList)
                {
                    if (admin.Lozinka == a.Lozinka && admin.KorisnickoIme == a.KorisnickoIme)
                        return BadRequest("Korisnicko ime i lozinka vec postoje");
                }

                List<Korisnik> korisnici = _dbContext.Korisnik.ToList();
                foreach (var k in korisnici)
                {
                    if (admin.KorisnickoIme == k.KorisnickoIme && admin.Lozinka == k.Lozinka)
                        return BadRequest("Korisnicko ime i lozinka vec postoje");
                }

                _dbContext.Add(admin);
            }


            _dbContext.SaveChanges();
            return Ok(admin);
        }


        [HttpGet]
        public ActionResult<List<AdminGetVM>> GetAll()
        {
            var data = _dbContext.Admin
                .Include(k => k.grad.drzava)
                .Select(k => new AdminGetVM
                {
                    id = k.ID,
                    ime = k.Ime,
                    prezime = k.Prezime,
                    godina_rodjenja = k.DatumRodjenja.Year,
                    telefon = k.Telefon,
                    email = k.Email,
                    adresa = k.Adresa,
                    spol = k.Spol,
                    jmbg = k.JMBG,
                    strucna_sprema = k.StrucnaSprema,
                    datum_zaposlenja = k.DatumZaposlenja.ToShortDateString(),
                    grad = k.grad.Naziv + "(" + k.grad.drzava.Naziv + ")"

                });
            return data.Take(100).ToList();
        }

        [HttpGet]
        public ActionResult<AdminAddVM> GetById(int id)
        {
            Admin admin = _dbContext.Admin.FirstOrDefault(a => a.ID == id);
            if (admin == null)
                return BadRequest("Ne postoji admin");
            AdminAddVM adminGetVM = new AdminAddVM
            {
                id = admin.ID,
                ime = admin.Ime,
                prezime = admin.Prezime,
                telefon = admin.Telefon,
                email = admin.Email,
                adresa = admin.Adresa,
                spol = admin.Spol,
                jmbg = admin.JMBG,
                grad_ID = admin.gradid,
                datum_rodjenja = admin.DatumRodjenja.ToString("yyyy-MM-dd"),
                strucna_sprema = admin.StrucnaSprema,
                datum_zaposlenja = admin.DatumZaposlenja.ToString("yyyy-MM-dd"),
                korisnickoIme = admin.KorisnickoIme,
                lozinka = admin.Lozinka

            };
            return adminGetVM;
        }


    }
}
