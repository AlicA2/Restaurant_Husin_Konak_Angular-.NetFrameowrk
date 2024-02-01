using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HusinKonak.Data.Helpers.AutentifikacijaAutorizacija
{
    public class AutorizacijaAttribute : TypeFilterAttribute
    {
        public AutorizacijaAttribute(bool korisnici, bool admini)
            : base(typeof(MyAuthorizeImpl))
        {
            Arguments = new object[] { korisnici, admini };
        }
    }


    public class MyAuthorizeImpl : IActionFilter
    {
        private readonly bool _korisnici;
        private readonly bool _admini;

        public MyAuthorizeImpl(bool korisnici, bool admini)
        {
            _korisnici = korisnici;
            _admini = admini;
        }
        public void OnActionExecuted(ActionExecutedContext filterContext)
        {
            KretanjePoSistemu.Save(filterContext.HttpContext);
        }

        public void OnActionExecuting(ActionExecutingContext filterContext)
        {
            MyAuthTokenExtension.LoginInformacije loginInfo = filterContext.HttpContext.GetLoginInfo();
            if (!loginInfo.isLogiran || loginInfo.korisnickiNalog == null)
            {
                filterContext.Result = new UnauthorizedResult();
                return;
            }

            if (!loginInfo.korisnickiNalog.isAktiviran)
            {
                filterContext.Result = new UnauthorizedObjectResult("korisnik nije aktiviran - provjerite email poruke ");
                return;
            }

            if (loginInfo.korisnickiNalog.isAdmin && _admini)
            {
                if (loginInfo.autentifikacijaToken == null || !loginInfo.autentifikacijaToken.twoFJelOtkljucano)
                {
                    filterContext.Result = new UnauthorizedObjectResult("potrebno je otkljucati login sa codom poslat na email " +
                        loginInfo.korisnickiNalog.admin.Email);
                    return;
                }
                return;//ok - ima pravo pristupa
            }

            if (loginInfo.korisnickiNalog.isKorisnik && _korisnici)
            {
                return;//ok - ima pravo pristupa
            }


            //else nema pravo pristupa
            filterContext.Result = new UnauthorizedResult();
        }

    }
}
