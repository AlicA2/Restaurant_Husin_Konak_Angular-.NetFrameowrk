import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";


declare function porukaSuccess(a: string):any;

@Component({
  selector: 'app-registracija-admin',
  templateUrl: './registracija-admin.component.html',
  styleUrls: ['./registracija-admin.component.css']
})
export class RegistracijaAdminComponent implements OnInit {

  constructor(private httpKlijent:HttpClient,private router:Router) { }

  ngOnInit(): void {
    this.ucitajGradove()
  }

  gradovi:any;

  ime:string="";
  prezime:string="";
  datum_rodjenja:Date;
  datum_zaposlenja:Date;
  telefon:string="";
  adresa:string="";
  email:string="";
  spol:string="";
  jmbg:string="";
  grad_ID:number=0;
  strucna_sprema:string="";
  korisnickoIme:string="";
  lozinka:any="";




  ucitajGradove(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(x=>{
      this.gradovi=x;
    })
  }


  registrcaija() {
    if(this.ime!="" && this.prezime!="" && this.datum_rodjenja!=null && this.datum_zaposlenja!=null
      && this.telefon!="" && this.email!="" && this.adresa!="" && this.korisnickoIme!="" && this.lozinka!="" &&
      this.strucna_sprema!=""  && this.jmbg!="" && this.spol!="" && this.spol!="...." && this.grad_ID!=0){
      var korisnik={
        id:0,
        ime:this.ime,
        prezime:this.prezime,
        datum_rodjenja:this.datum_rodjenja,
        datum_zaposlenja:this.datum_zaposlenja,
        telefon:this.telefon,
        adresa:this.adresa,
        email:this.email,
        spol:this.spol,
        jmbg:this.jmbg,
        grad_ID:this.grad_ID,
        strucna_sprema:this.strucna_sprema,
        korisnickoIme:this.korisnickoIme,
        lozinka:this.lozinka
      }
      this.httpKlijent.post(MojConfig.adresa_servera + "/Admin/Snimi", korisnik).subscribe(x=>{
        porukaSuccess("Ospjesna regstracija");
      })
      this.router.navigateByUrl("/login");


    }
    else{
      alert("niste unijeli sve podatke");
    }
  }

}
