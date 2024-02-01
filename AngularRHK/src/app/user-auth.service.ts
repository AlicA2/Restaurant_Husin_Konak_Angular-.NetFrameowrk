import { Injectable } from '@angular/core';
import { LoginInformacije } from '../app/_helpers/login-informacije';

@Injectable({
  providedIn: 'root'
})
export class UserAuthService {
  private loginInformacije: LoginInformacije | null = null;

  setLoginInfo(loginInfo: LoginInformacije) {
    this.loginInformacije = loginInfo;
  }

  getKorisnikID(): number | null {
    if (this.loginInformacije && this.loginInformacije.autentifikacijaToken) {
      return this.loginInformacije.autentifikacijaToken.korisnickiNalogId;
    }
    return null;
  }
}
