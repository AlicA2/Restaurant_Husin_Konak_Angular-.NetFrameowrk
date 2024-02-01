import { Component, OnInit } from '@angular/core';
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";

@Component({
  selector: 'app-korisnici',
  templateUrl: './korisnici.component.html',
  styleUrls: ['./korisnici.component.css']
})
export class KorisniciComponent implements OnInit {
  korisnici:any;
  ime_prezime:string = '';
  filter_ime_prezime: boolean;
  gradovi: any;


  constructor(private httpKlijent:HttpClient, private router: Router) { }

  ngOnInit(): void {
    this.ucitajKorisnike();
    this.ucitajGradove();

  }

  ucitajKorisnike(){
    this.httpKlijent.get(MojConfig.adresa_servera+"/Korisnik/GetAll", MojConfig.http_opcije()).subscribe(x=>{
      this.korisnici=x;
    })
  }

  getPodaci() {
    if(this.korisnici==null)
      return [];
    return this.korisnici.filter(
      (x:any)=>
        (!this.filter_ime_prezime ||
          (x.ime + ' ' + x.prezime).toLowerCase().startsWith(this.ime_prezime)
          || (x.prezime + ' ' + x.ime).toLowerCase().startsWith(this.ime_prezime)));
  }

  obrisi(k: any) {
    this.httpKlijent.post(`${MojConfig.adresa_servera}/Korisnik/Obrisi/${k.id}`, MojConfig.http_opcije()).subscribe(x=>{
      this.getPodaci();
      this.ucitajKorisnike();
    });
  }

  ucitajGradove() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(x => {
      this.gradovi = x;
    })
  }
}
