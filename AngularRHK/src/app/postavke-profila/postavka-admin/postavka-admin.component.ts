import { Component, OnInit } from '@angular/core';
import {AutentifikacijaHelper} from "../../_helpers/autentifikacija-helper";
import {LoginInformacije} from "../../_helpers/login-informacije";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../../moj-config";
import {Router} from "@angular/router";
import {DatePipe} from "@angular/common";

declare function porukaSuccess(a: string):any;

@Component({
  selector: 'app-postavka-admin',
  templateUrl: './postavka-admin.component.html',
  styleUrls: ['./postavka-admin.component.css']
})
export class PostavkaAdminComponent implements OnInit {

  constructor(private httpKlijent:HttpClient, private router:Router) { }

  ngOnInit(): void {
    this.ucitajGradove();
    console.log(this.loginInfo().autentifikacijaToken.korisnickiNalog.ID)
    if (this.loginInfo().isPremisijaAdmin){
      this.ucitajAdmin();

    }
  }

  gradovi:any;
  admin:any={
    id:0,
    ime:"",
    prezime:"",
    datum_rodjenja:Date,
    datum_zaposlenja:Date,
    telefon:"",
    adresa:"",
    email:"",
    spol:",",
    jmbg:"",
    grad_ID:0,
    strucna_sprema:"",
    korisnickoIme:"",
    lozinka:""
  }

  ucitajGradove(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(x=>{
      this.gradovi=x;
    })
  }

  ucitajAdmin(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/Admin/GetById?id="+ this.loginInfo().autentifikacijaToken.korisnickiNalogId )
      .subscribe(x=>{
        this.admin=x;
        console.log(this.admin);

      });

  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  izmjena(){
    if(this.admin.ime!="" && this.admin.prezime!="" && this.admin.datum_rodjenja!=null && this.admin.datum_zaposlenja!=null
      && this.admin.telefon!="" && this.admin.email!="" && this.admin.adresa!="" && this.admin.korisnickoIme!="" && this.admin.lozinka!="" &&
      this.admin.strucna_sprema!=""  && this.admin.jmbg!="" && this.admin.spol!="" && this.admin.spol!="...." && this.admin.grad_ID!=0){
    this.httpKlijent.post(MojConfig.adresa_servera + "/Admin/Snimi", this.admin).subscribe(x=>{
      porukaSuccess("Upjesna promjena podataka");
    })

      this.router.navigateByUrl("/pocetna");
    }
    else{
      alert("polja ne smiju biti prazna");
    }
  }

}


