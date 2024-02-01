import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../moj-config';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { AutentifikacijaHelper } from '../_helpers/autentifikacija-helper';
import { UserAuthService } from '../user-auth.service';
import { Router } from '@angular/router';

declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-o-nama',
  templateUrl: './onama.component.html',
  styleUrls: ['./onama.component.css']
})

export class ONamaComponent implements OnInit {
  formaVidljiva: boolean = false;
  tekst: string = '';
  naslov: string = '';
  poruke: { id: number,title:string, text: string }[] = [];
  formaUredivanja: boolean = false;
  porukaZaUredjivanje: { id: number, title: string, text: string } | null = null;

  loginInformation = AutentifikacijaHelper.getLoginInfo();

  constructor(private httpKlijent: HttpClient,
              private router: Router) { }

  ngOnInit() {
    this.dohvatiPodatke();
  }

  otvoriFormu() {
    this.formaVidljiva = true;
  }

  spasiTekst() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/AboutMe/Dodaj", { Title: this.naslov, Text: this.tekst }).subscribe((response: any) => {
      this.naslov = '';
      this.tekst = '';
      this.formaVidljiva = false;
      porukaSuccess("Dodano!");
      this.dohvatiPodatke();
    });
  }

  dohvatiPodatke() {
    this.httpKlijent.get<{ id: number,title:string, text: string }[]>(MojConfig.adresa_servera + "/AboutMe/DohvatiSve").subscribe((response: { id: number,title:string, text: string }[]) => {
      this.poruke = response;
    });
  }
  obrisiPoruku(id: number) {
    this.httpKlijent.delete(MojConfig.adresa_servera + "/AboutMe/Izbrisi/" + id).subscribe(() => {
      this.poruke = this.poruke.filter(poruka => poruka.id !== id);
      this.dohvatiPodatke();
      porukaSuccess("Obrisano!");
    });
  }
  urediPoruku(poruka: { id: number, title: string, text: string }) {
    this.porukaZaUredjivanje = poruka;
    this.naslov = poruka.title;
    this.tekst = poruka.text;
    this.formaUredivanja = true;
  }
  spasiUredjenuPoruku() {
    if (this.porukaZaUredjivanje) {
      this.httpKlijent.put(MojConfig.adresa_servera + "/AboutMe/Uredi/" + this.porukaZaUredjivanje.id, {
        Title: this.naslov,
        Text: this.tekst
      }).subscribe(() => {
        porukaSuccess("Poruka je uspješno uređena.");
        this.dohvatiPodatke();
        this.naslov = '';
        this.tekst = '';
        this.formaUredivanja = false;
        this.porukaZaUredjivanje = null;
      });
    }
  }
  odustaniUredjivanje() {
    this.naslov = '';
    this.tekst = '';
    this.formaUredivanja = false;
    this.porukaZaUredjivanje = null;
  }
  odustani() {
    this.formaVidljiva = false;
    this.tekst = '';
  }
}
