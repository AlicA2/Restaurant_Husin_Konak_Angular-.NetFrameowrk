import { Component, OnInit } from '@angular/core';
import {AutentifikacijaHelper} from "../../_helpers/autentifikacija-helper";
import {LoginInformacije} from "../../_helpers/login-informacije";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {Router} from "@angular/router";

declare function porukaSuccess(a: string):any;


@Component({
  selector: 'app-postavka-korisnik',
  templateUrl: './postavka-korisnik.component.html',
  styleUrls: ['./postavka-korisnik.component.css']
})
export class PostavkaKorisnikComponent implements OnInit {

  constructor(private httpKlijent:HttpClient, private router:Router) { }

  ngOnInit(): void {
    this.ucitajGradove();
    console.log(this.loginInfo().autentifikacijaToken.korisnickiNalog.ID)
    if (this.loginInfo().isPremisijaKorisnik){
      this.ucitajKorisnika();
      console.log(this.korisnik)
    }
  }

  gradovi:any;
  korisnik:any ={
  id:0,
  ime:"",
  prezime:"",
  telefon:"",
  email:"",
    spol:"",
  grad_ID:0,
    korisnickoIme:"",
    lozinka:""
}

  ucitajGradove(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(x=>{
      this.gradovi=x;
    })
  }

  ucitajKorisnika(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/Korisnik/GetById?id="+ this.loginInfo().autentifikacijaToken.korisnickiNalogId )
      .subscribe(x=>{
        this.korisnik=x;
        console.log(this.korisnik);

      });

  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  izmjena() {
    if (this.korisnik.ime != "" && this.korisnik.prezime != "" && this.korisnik.telefon != "" && this.korisnik.email != "" && this.korisnik.korisnickoIme != "" && this.korisnik.lozinka != null &&
      this.korisnik.spol != "" && this.korisnik.spol != "...." && this.korisnik.grad_ID != 0){
      this.httpKlijent.post(MojConfig.adresa_servera + "/Korisnik/Snimi", this.korisnik).subscribe(x=>{
        porukaSuccess("Upjesna promjena podataka");
      });

    }
    else {
      alert("polja ne smiju biti prazna");
    }
  }
}
