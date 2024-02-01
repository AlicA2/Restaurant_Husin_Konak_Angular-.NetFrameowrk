using Azure.Core;
using HusinKonak.Data.Helpers.AutentifikacijaAutorizacija;
using HusinKonak.Data.Helpers;
using HusinKonak.Data.Modul0_Autentifikacija.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using HusinKonak.Data.Modul0_Autentifikacija.ViewModels;

namespace HusinKonak.Data.Modul0_Autentifikacija.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class AutentifikacijaController : ControllerBase
    {
        private readonly RestaurantDBContext _dbContext;

        public AutentifikacijaController(RestaurantDBContext dbContext)
        {
            this._dbContext = dbContext;
        }

        [HttpPost]
        public ActionResult DodajKontakt([FromBody] KontaktAddVM kontakt)
        {
            var korisnik = HttpContext.GetLoginInfo().korisnickiNalog;

            if (korisnik == null)
            {
                return BadRequest("Korisnik nije autentificiran");
            }

            return Ok(new { message = "Kontakt je uspješno poslan." });
        }


        [HttpGet("{code}")]
        public ActionResult Otkljucaj(string code)
        {
            var korisnickiNalog = HttpContext.GetLoginInfo().korisnickiNalog;

            if (korisnickiNalog == null)
            {
                return BadRequest("korisnik nije logiran");
            }

            var token = _dbContext.AutentifikacijaToken.FirstOrDefault(s => s.twoFCode == code && s.KorisnickiNalogId == korisnickiNalog.ID);
            if (token != null)
            {
                token.twoFJelOtkljucano = true;
                _dbContext.SaveChanges();
                return Ok();
            }

            return BadRequest("pogresan URL");
        }

        [HttpPost]
        public ActionResult<MyAuthTokenExtension.LoginInformacije> Login([FromBody] LoginVM x)
        {
            KorisnickiNalog? logiraniKorisnik = _dbContext.KorisnickiNalog
                .FirstOrDefault(k =>
                k.KorisnickoIme == x.korisnickoIme && k.Lozinka == x.lozinka);

            if (logiraniKorisnik == null)
            {
                return new MyAuthTokenExtension.LoginInformacije(null);
            }

            string randomString = TokenGenerator.Generate(10);
            string twoFCode = TokenGenerator.Generate(4);

            var noviToken = new AutentifikacijaToken()
            {
                ipAdresa = Request.HttpContext.Connection.RemoteIpAddress?.ToString(),
                vrijednost = randomString,
                KorisnickiNalog = logiraniKorisnik,
                vrijemeEvidentiranja = DateTime.Now,
                twoFCode = twoFCode
            };

            _dbContext.Add(noviToken);
            _dbContext.SaveChanges();
     

            try
            {
                EmailLog.uspjesnoLogiranKorisnik(noviToken, Request.HttpContext);
            }
            catch (System.Net.Mail.SmtpException smtpEx)
            {
                Console.WriteLine("SMTP Exception: " + smtpEx.Message);
            }

            //4- vratiti token string
            return new MyAuthTokenExtension.LoginInformacije(noviToken);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            AutentifikacijaToken? autentifikacijaToken = HttpContext.GetAuthToken();

            if (autentifikacijaToken == null)
                return Ok();

            _dbContext.Remove(autentifikacijaToken);
            _dbContext.SaveChanges();
            return Ok();
        }

        [HttpGet]
        public ActionResult<AutentifikacijaToken?> Get()
        {
            AutentifikacijaToken? autentifikacijaToken = HttpContext.GetAuthToken();

            return autentifikacijaToken;
        }
    }
}
