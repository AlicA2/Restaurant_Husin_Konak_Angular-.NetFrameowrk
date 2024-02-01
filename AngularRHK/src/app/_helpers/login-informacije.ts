
export class LoginInformacije{
  autentifikacijaToken:        AutentifikacijaToken=null;
  isLogiran:                   boolean=false;
  isPremisijaAdmin:boolean=false;
  isPremisijaKorisnik:boolean=false;
}

export interface AutentifikacijaToken {

  iD: number;
  vrijednost: string;
  korisnickiNalogId: number;
  korisnickiNalog: KorisnickiNalog;
  vrijemeEvidentiranja: Date;
  ipAdresa: string;
}
export interface KorisnickiNalog {
  ID: number;
  korisnickoIme: string;
  isAdmin: boolean;
  isKorisnik: boolean;
  isTrener:boolean;
  isAktiviran:boolean;
}
