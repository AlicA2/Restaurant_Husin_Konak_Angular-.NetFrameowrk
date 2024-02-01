using HusinKonak.Data.Modul0_Autentifikacija.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace HusinKonak.Data.Helpers.AutentifikacijaAutorizacija
{
    public static class MyAuthTokenExtension
    {
        public class LoginInformacije
        {
            public LoginInformacije(AutentifikacijaToken? autentifikacijaToken)
            {
                this.autentifikacijaToken = autentifikacijaToken;
            }

            [JsonIgnore]
            public KorisnickiNalog? korisnickiNalog => autentifikacijaToken?.KorisnickiNalog;
            public AutentifikacijaToken? autentifikacijaToken { get; set; }

            public bool isLogiran => korisnickiNalog != null;
            public bool isPremisijaAdmin => isLogiran && korisnickiNalog.isAdmin;
            public bool isPremisijaKorisnik => isLogiran && korisnickiNalog.isKorisnik;

        }

        public static LoginInformacije GetLoginInfo(this HttpContext httpContext)
        {
            var token = httpContext.GetAuthToken();

            return new LoginInformacije(token);
        }

        public static AutentifikacijaToken? GetAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.GetMyAuthToken();
            RestaurantDBContext db = httpContext.RequestServices.GetService<RestaurantDBContext>();

            AutentifikacijaToken? korisnickiNalog = db.AutentifikacijaToken
                .Include(s => s.KorisnickiNalog)
                .SingleOrDefault(x => x.vrijednost == token);

            return korisnickiNalog;
        }


        public static string GetMyAuthToken(this HttpContext httpContext)
        {
            string token = httpContext.Request.Headers["autentifikacija-token"];
            return token;
        }
    }
}
