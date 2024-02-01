using HusinKonak.Data.Modul0_Autentifikacija.Models;
using HusinKonak.Data.Modul2.Models;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Helpers
{
    public class EmailLog
    {
        public static void uspjesnoLogiranKorisnik(AutentifikacijaToken token, HttpContext httpContext)
        {
            //slanje maila pri loginu admina
            //if (logiraniKorisnik.isAdmin)
            //{
            //    EmailSender.Posalji(logiraniKorisnik.admin.Email, "Logiran admin", $"Login info {DateTime.Now}");
            //}

            var logiraniKorisnik = token.KorisnickiNalog;
            if (logiraniKorisnik.isAdmin)
            {
                var poruka = $"Postovani {logiraniKorisnik.KorisnickoIme}, <br> " +
                              $"Code za 2F je <br>" +
                              $"{token.twoFCode}<br>" +
                              $"Login info {DateTime.Now}";


                EmailSender.Posalji(logiraniKorisnik.admin.Email, "Code za 2F autorizaciju", poruka, true);
            }
        }

        public static void noviKorisnik(Korisnik korisnik, HttpContext httpContext)
        {
            if (!korisnik.isAktiviran)
            {
                var Request = httpContext.Request;
                var location = $"{Request.Scheme}://{Request.Host}";


                string url = location + "/korisnik/Aktivacija/" + korisnik.aktivacijaGUID;
                string poruka = $"Postovani/a {korisnik.Ime}, <br> Link za aktivaciju <a href='{url}'>{url}</a>... {DateTime.Now}";

                EmailSender.Posalji(korisnik.Email, "Aktivacija korisnika", poruka, true);

            }
        }
    }
}
