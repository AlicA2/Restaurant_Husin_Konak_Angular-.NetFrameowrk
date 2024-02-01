import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {MojConfig} from "../moj-config";
import { UserAuthService } from '../user-auth.service';


declare function porukaSuccess(a: string):any;
declare function porukaError(a: string):any;

@Component({
  selector: 'app-login',
  templateUrl: './login.component.html',
  styleUrls: ['./login.component.css']
})
export class LoginComponent implements OnInit {

  txtKorisnickiIme:any;
  txtLozinka:any;
  korisnik_id:any;
  pomocna:any;
  constructor(private httpKlijent: HttpClient, private router: Router, private userAuthService: UserAuthService) { }

  ngOnInit(): void {
  }

  btnLogiranje() {
    let saljemo={
      korisnickoIme:this.txtKorisnickiIme,
      lozinka:this.txtLozinka
    }
    if(saljemo.korisnickoIme==null || saljemo.lozinka==null)
    {
      this.pomocna=saljemo;
    }
    this.httpKlijent.post<LoginInformacije>(MojConfig.adresa_servera+ "/Autentifikacija/Login", saljemo).subscribe(
      (x:LoginInformacije)=>{
        if(x.isLogiran){
          this.userAuthService.setLoginInfo(x);
          porukaSuccess("login upjesan");
          AutentifikacijaHelper.setLoginInfo(x);
          if(x.isPremisijaKorisnik)
          {
            console.log(x);
            this.korisnik_id = x.autentifikacijaToken.korisnickiNalogId;
            this.router.navigateByUrl("/pocetna");
            porukaSuccess("Korisnik!!!");
          }
          if (x.autentifikacijaToken?.korisnickiNalog.isAdmin)
            this.router.navigateByUrl("/two-f-otkljucaj");

        }
        else{
          AutentifikacijaHelper.setLoginInfo(null);
          porukaError("neuspjesan login");
        }
      }
    )
  }
}
