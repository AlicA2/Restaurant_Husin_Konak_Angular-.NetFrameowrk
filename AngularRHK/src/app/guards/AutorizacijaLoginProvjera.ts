import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot } from '@angular/router';
import { AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";


@Injectable()
export class AutorizacijaLoginProvjera implements CanActivate {

  constructor(private router: Router) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    try {
      if (AutentifikacijaHelper.getLoginInfo().isLogiran) {

        let isAktiviran = AutentifikacijaHelper.getLoginInfo().autentifikacijaToken?.korisnickiNalog?.isAktiviran;

        //redirekcija na novu komponentu ako korisnik nije aktiviran, onemogućeno mu je otvaranje ostalih komponenti sa canActivate u app.module
        if (!isAktiviran)
        {
          this.router.navigate(['/user-not-active']);
          return false;
        }

        return true;
      }
    }catch (e) {
    }

    this.router.navigate(['login'], { queryParams: { returnUrl: state.url }});
    return false;
  }
}
