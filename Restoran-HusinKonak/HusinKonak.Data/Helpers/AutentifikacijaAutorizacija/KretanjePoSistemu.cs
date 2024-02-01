using Microsoft.AspNetCore.Diagnostics;
using HusinKonak.Data.Modul0_Autentifikacija.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Http.Extensions;
using Microsoft.AspNetCore.Http;

namespace HusinKonak.Data.Helpers.AutentifikacijaAutorizacija
{
    public class KretanjePoSistemu
    {
        public static int Save(HttpContext httpContext, IExceptionHandlerPathFeature? exceptionMessage = null)
        {
            KorisnickiNalog? korisnik = httpContext.GetLoginInfo().korisnickiNalog;

            var request = httpContext.Request;

            var queryString = request.Query;

            //if (queryString.Count == 0 && !request.HasFormContentType)
            //    return 0;

            //IHttpRequestFeature feature = request.HttpContext.Features.Get<IHttpRequestFeature>();
            string detalji = "";
            if (request.HasFormContentType)
            {
                foreach (string key in request.Form.Keys)
                {
                    detalji += " | " + key + "=" + request.Form[key];
                }
            }
            //log svih desavanja na sistemu
            var x = new LogKretanjePoSistemu
            {
                korisnik = korisnik,
                vrijeme = DateTime.Now,
                queryPath = request.GetEncodedPathAndQuery(),
                postData = detalji,
                ipAdresa = request.HttpContext.Connection.RemoteIpAddress?.ToString(),
            };

            if (exceptionMessage != null)
            {
                x.isException = true;
                x.exceptionMessage = exceptionMessage.Error.Message + " |" + exceptionMessage.Error.InnerException;
            }

            RestaurantDBContext db = httpContext.RequestServices.GetService<RestaurantDBContext>();

            db.Add(x);
            db.SaveChanges();

            return x.id;
        }
    }
}
