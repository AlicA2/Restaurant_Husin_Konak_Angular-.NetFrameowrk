import { Component } from '@angular/core';
import { MojConfig } from "../moj-config";
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { LoginInformacije } from "../_helpers/login-informacije";
import { UserAuthService } from "../user-auth.service";
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";

declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-meni',
  templateUrl: './meni.component.html',
  styleUrls: ['./meni.component.css']
})

export class MeniComponent {
  loginInformation = AutentifikacijaHelper.getLoginInfo();

  novaForma:boolean=false;
  otvoriFormuNovu:boolean=false;

  otvoriFormu: boolean = false;
  kategorije:any;
  menii:any;

  kategorija={
    Naziv:''
  };
  meni: {
    naziv: string;
    opis: string;
    cijena: number;
    kategorija_id: number | null;
    slikaBase64: string | null;
  } = {
    naziv: '',
    opis: '',
    cijena: 0,
    kategorija_id: null,
    slikaBase64: null
  };

  constructor(private httpKlijent: HttpClient,
              private sanitizer: DomSanitizer,
              private router: Router,
              private loginInformacije: LoginInformacije,
              private userAuthService: UserAuthService) {}

  sanitizeImage(imageData: string): SafeUrl {
    if (imageData) {
      const imageUrl = 'data:image/png;base64,' + imageData;
      return this.sanitizer.bypassSecurityTrustUrl(imageUrl);
    }
    return '';
  }
  SpasiKategoriju()
  {
    if(!this.ValidirajKategoriju())
    {
      return;
    }
    this.httpKlijent.post(MojConfig.adresa_servera+"/Kategorija/DodajKategoriju",this.kategorija)
      .subscribe((x)=>{
        porukaSuccess("Uspjesno dodana kategorija");
        this.GetMeni();
        this.GetKategorije();
        this.resetirajFormuKategorije();
      },(y)=>{
        porukaError("Greska");
      })
  }

  dodajMeni() {
    if (!this.validirajPodatke()) {
      return;
    }
    const meniData = {
      Naziv: this.meni.naziv,
      Opis: this.meni.opis,
      Cijena: this.meni.cijena,
      kategorija_id: this.meni.kategorija_id,
      SlikaBase64: this.meni.slikaBase64
    };
    console.log(meniData);

    this.httpKlijent.post(MojConfig.adresa_servera+"/Meni/DodajMeni", meniData)
      .subscribe(
        (response) => {
          porukaSuccess("Uspjesno dodan meni");
          console.log(response);
          this.meni = {
            naziv: '',
            opis: '',
            cijena: 0,
            kategorija_id: null,
            slikaBase64: null
          };
          this.GetMeni();
          this.GetKategorije();
          this.imagePreview = null;
          this.novaForma = false;
        },
        (error) => {
          porukaError("Greška u dodavanju menija");
          console.error(error);
        }
      );
  }
  ObrisiMeni(s: any) {
    this.httpKlijent.delete(MojConfig.adresa_servera + "/Meni/DeleteMeni/" + s)
      .subscribe(() => {
        porukaSuccess("Uspjesno izbrisan meni");
        this.GetMeni();

      });
  }

  GetMeni()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Meni/GetAll")
      .subscribe(x=>{
        this.menii=x;
        console.log("Data received from server:", x);
        console.log("menii array:", this.menii);
      })
  }
  GetKategorije()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Kategorija/GetAll")
      .subscribe(x=>{
        this.kategorije=x;
        console.log(this.kategorije);
      })
  }
  deleteKategorija(s:any)
  {
    this.httpKlijent.delete(MojConfig.adresa_servera + "/Kategorija/DeleteKategorija/" + s)
      .subscribe(() => {
        porukaSuccess("Uspjesno izbrisana kategorija");
        this.GetKategorije();
      });
  }
  getJelaByKategorija(kategorijaId: number) {
    return this.menii.filter((prikaz: any) => prikaz.kategorija_id === kategorijaId);
  }
  ngOnInit(): void {
    this.GetKategorije();
    this.menii=this.GetMeni();
  }
  imagePreview: string | null = null;

  handleSlikaInput(event: any) {
    const file = event.target.files[0];
    if (file) {
      const reader = new FileReader();
      reader.onload = (e: any) => {
        this.imagePreview = e.target.result;
        this.meni.slikaBase64 = e.target.result;
      };
      reader.readAsDataURL(file);
    }
  }

  validirajPodatke() {
    if (!this.meni.naziv || !this.meni.opis || !this.meni.cijena || !this.meni.slikaBase64) {
      console.log('Niste unijeli sve podatke.');
      return false;
    }
    return true;
  }
  resetirajFormuKategorije()
  {
    this.kategorija=
      {
        Naziv:''
      }
  }
  ValidirajKategoriju()
  {
    if (!this.kategorija.Naziv) {
      porukaError("Molimo popunite polje.");
      return false;
    }
    return true;
  }
}

