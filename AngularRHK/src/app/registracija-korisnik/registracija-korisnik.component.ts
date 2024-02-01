import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {MojConfig} from "../moj-config";
import {Router} from "@angular/router";

declare function porukaSuccess(a: string):any;

@Component({
  selector: 'app-registracija-korisnik',
  templateUrl: './registracija-korisnik.component.html',
  styleUrls: ['./registracija-korisnik.component.css']
})
export class RegistracijaKorisnikComponent implements OnInit {

  constructor(private httpKlijent: HttpClient, private router: Router) {
  }

  ngOnInit(): void {
    this.ucitajGradove();
  }

  gradovi: any;

  ime: string = "";
  prezime: string = "";
  telefon: string = "";
  email: string = "";
  spol: string = "";
  grad_ID: number = 0;
  korisnickoIme: string = "";
  lozinka: any = "";


  ucitajGradove() {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Grad/GetAll").subscribe(x => {
      this.gradovi = x;
    })
  }

  registrcaija() {

    if (this.ime != "" && this.prezime != "" && this.telefon != "" && this.email != "" && this.korisnickoIme != "" && this.lozinka != "" &&
       this.spol != "" && this.spol != "...." && this.grad_ID != 0) {
      var korisnik = {
        id: 0,
        ime: this.ime,
        prezime: this.prezime,
        telefon: this.telefon,
        email: this.email,
        spol: this.spol,
        grad_ID: this.grad_ID,
        korisnickoIme: this.korisnickoIme,
        lozinka: this.lozinka
      }
      this.httpKlijent.post(MojConfig.adresa_servera + "/Korisnik/Snimi", korisnik).subscribe(x => {
        porukaSuccess("Uspjesna registracija");
      })
      this.router.navigateByUrl("/login");


    } else {
      alert("Niste unijeli sve podatke");
    }


  }
}
