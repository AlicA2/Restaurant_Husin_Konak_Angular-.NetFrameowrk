import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { MojConfig } from '../moj-config';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { AutentifikacijaHelper } from '../_helpers/autentifikacija-helper';
import { UserAuthService } from '../user-auth.service';
import { Router } from '@angular/router';
import { LoginInformacije } from '../_helpers/login-informacije';


declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-dostava',
  templateUrl: './dostava.component.html',
  styleUrls: ['./dostava.component.css']
})
export class DostavaComponent implements OnInit {
  kategorije: any[];
  odabranaKategorija: any;
  meniUKategoriji: any[];
  sviMeni: any[];
  sastavKorpe:any;
  slanjeUKorpuu={
    Kolicina:0,
    meni_id:0,
    korisnik_id:0
  }
  selectedPaymentMethod:string = 'gotovinsko';
  editableQuantity: number;
  dostavaMeniSastav:any[]=[];
  izbrisiId:any;
  dostavaForma:boolean=false;
  korpaMeniResults: any[] = [];
  ukupnaVrijednostDostave:number;
  formaNova:boolean=false;
  nova:number;
  selectedResult:any;
  loginInformation = AutentifikacijaHelper.getLoginInfo();
  korisnik: any;

  zaPopuniti={
    AdresaDostave:'',
    TelefonDostave:'',
    kartica_id:''
  }
  kartica={
    Ime:'',
    Prezime:'',
    BrojKartice:'',
    TipKartice:"debitcard",
    AdresaRacuna:'',
    Grad:'',
    Drzava:'',
    PostanskiBroj:'',
    Telefon:'',
    SigurnosniKod:'',
    DatumIsteka:'',
    korisnik_id:this.loginInformation.autentifikacijaToken.korisnickiNalogId
  }
  sviKorisnici:any;
  sveDostave:any;
  sveKartice:any;
  mergedData:any = [];
  kartica_id:number =null;
  constructor(
    private httpKlijent: HttpClient,
    private sanitizer: DomSanitizer,
    private userAuthService: UserAuthService,
    private router: Router
  ) {}


  ngOnInit() {
    this.dohvatiKategorije();

    this.dohvatiSveMeni();

    this.GetKorisnici();

    this.dohvatiKorpu();

    this.TajKorisnik();

    this.DohvatiDostavu();
    this.DohvatiKartice();
  }
  DohvatiKartice()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Kartica/GetAll")
      .subscribe(x=>{
        this.sveKartice=x;
      })
  }
  DohvatiDostavu()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Dostava/GetAll")
      .subscribe(x=>{
        this.sveDostave=x;
      })

  }
  Prikazz() {
    const oneSecond = 1000;
    this.sveDostave.sort((a:any, b:any) => (a.datumKreiranja < b.datumKreiranja ? -1 : 1));



    for (let i = 0; i < this.sveDostave.length; i++) {
      const currentItem = this.sveDostave[i];
      const datumKreiranja = new Date(currentItem.datumKreiranja).getTime();

      if (i === 0) {

        this.mergedData.push({
          id: currentItem.id,
          datumKreiranja: datumKreiranja,
          korisnik_id: currentItem.korisnik_id,
          ukupnaCijena: currentItem.cijena,
          Telefon:currentItem.telefonDostave,
          adresaDostave:currentItem.adresaDostave,
          karticno_placanje:currentItem.kartica_id,
          kolicina: [currentItem.kolicina],
          meni_id: [currentItem.meni_id],
        });
      } else {
        const previousEntry = this.mergedData[this.mergedData.length - 1];
        const previousDatumKreiranja = previousEntry.datumKreiranja;

        if (datumKreiranja - previousDatumKreiranja <= oneSecond) {
          previousEntry.kolicina.push(currentItem.kolicina);
          previousEntry.meni_id.push(currentItem.meni_id);
        } else {

          this.mergedData.push({
            id: currentItem.id,
            datumKreiranja: datumKreiranja,
            korisnik_id: currentItem.korisnik_id,
            ukupnaCijena:currentItem.cijena,
            Telefon:currentItem.telefonDostave,
            adresaDostave:currentItem.adresaDostave,
            karticno_placanje:currentItem.kartica_id,
            kolicina: [currentItem.kolicina],
            meni_id: [currentItem.meni_id],
          });
        }
      }
    }

    console.log(this.mergedData);
  }
PosaljiDostavu() {
    if (this.selectedPaymentMethod === 'gotovinsko') {
      if (!this.zaPopuniti.AdresaDostave || !this.zaPopuniti.TelefonDostave) {
        porukaError("Popunite polja");
        return;
      }

      for (let i = 0; i < this.sastavKorpe.length; i++) {
        if (this.sastavKorpe[i].korisnik_id === this.loginInformation.autentifikacijaToken.korisnickiNalogId) {
           const dataToSend = {
            korisnik_id: this.loginInformation.autentifikacijaToken.korisnickiNalogId,
            AdresaDostave: this.zaPopuniti.AdresaDostave,
            TelefonDostave: this.zaPopuniti.TelefonDostave,
            meni_id: this.sastavKorpe[i].meni_id,
            Kolicina: this.sastavKorpe[i].kolicina,
            Cijena: this.ukupnaVrijednostDostave,
             Ime:'',
             Prezime:'',
             BrojKartice:'',
             TipKartice:'',
             AdresaRacuna:'',
             Grad:'',
             Drzava:'',
             PostanskiBroj:'',
             Telefon:'',
             SigurnosniKod:'',
             DatumIsteka:'',
             kartica_id:this.kartica_id
          };

          if (
            dataToSend.Ime == null ||
            dataToSend.Prezime == null ||
            dataToSend.BrojKartice == null ||
            dataToSend.TipKartice == null ||
            dataToSend.AdresaRacuna == null ||
            dataToSend.Grad == null ||
            dataToSend.Drzava == null ||
            dataToSend.PostanskiBroj == null ||
            dataToSend.Telefon == null ||
            dataToSend.SigurnosniKod == null ||
            dataToSend.DatumIsteka == null
          ) {
            dataToSend.kartica_id = null;
          }
          console.log(dataToSend);
          this.httpKlijent.post(MojConfig.adresa_servera+"/Dostava/DodajDostavu",dataToSend)
            .subscribe(x=>{
              porukaSuccess("Uspjesno dodana dostava");
            },y=>{
              porukaError("Greska prilikom dodavanja dostave");
            })
        }
      }
      this.Resetuj();
      this.IzbrisiKorpu();
      this.dostavaForma = false;

    }
  if (this.selectedPaymentMethod === 'karticno') {
    if (!this.zaPopuniti.AdresaDostave || !this.zaPopuniti.TelefonDostave) {
      porukaError("Popunite polja");
      return;
    }
    if (
      this.kartica.Ime == null ||
      this.kartica.Prezime == null ||
      this.kartica.BrojKartice == null ||
      this.kartica.TipKartice == null ||
      this.kartica.AdresaRacuna == null ||
      this.kartica.Grad == null ||
      this.kartica.Drzava == null ||
      this.kartica.PostanskiBroj == null ||
      this.kartica.Telefon == null ||
      this.kartica.SigurnosniKod == null ||
      this.kartica.DatumIsteka == null
    )
    {
      porukaError("Unesite podatke o kartici");
    }
    for (let i = 0; i < this.sastavKorpe.length; i++) {
      if (this.sastavKorpe[i].korisnik_id === this.loginInformation.autentifikacijaToken.korisnickiNalogId) {
        const dataToSend = {
          korisnik_id: this.loginInformation.autentifikacijaToken.korisnickiNalogId,
          AdresaDostave: this.zaPopuniti.AdresaDostave,
          TelefonDostave: this.zaPopuniti.TelefonDostave,
          meni_id: this.sastavKorpe[i].meni_id,
          Kolicina: this.sastavKorpe[i].kolicina,
          Cijena:this.ukupnaVrijednostDostave,
          Ime:this.kartica.Ime,
          Prezime:this.kartica.Prezime,
          BrojKartice:this.kartica.BrojKartice,
          TipKartice:this.kartica.TipKartice,
          AdresaRacuna:this.kartica.AdresaRacuna,
          Grad:this.kartica.Grad,
          Drzava:this.kartica.Drzava,
          PostanskiBroj:this.kartica.PostanskiBroj,
          Telefon:this.kartica.Telefon,
          SigurnosniKod:this.kartica.SigurnosniKod,
          DatumIsteka:this.kartica.DatumIsteka,
          kartica_id:this.kartica_id
        };
        this.httpKlijent.post(MojConfig.adresa_servera+"/Dostava/DodajDostavu",dataToSend)
          .subscribe(x=>{
            porukaSuccess("Uspjesno dodana dostava");
          },y=>{
            porukaError("Greska prilikom dodavanja dostave");
          })
      }
    }
    this.Resetuj();
    this.IzbrisiKorpu();
    this.dostavaForma = false;

  }


  }
  Resetuj()
  {
    this.zaPopuniti={
      AdresaDostave:'',
      TelefonDostave:'',
      kartica_id:''
    }
  }
  IzbrisiKorpu()
  {
    for (let i=0;i<this.sastavKorpe.length;i++)
    {
      if(this.sastavKorpe[i].korisnik_id===this.loginInformation.autentifikacijaToken.korisnickiNalogId)
      {
          this.httpKlijent.delete(MojConfig.adresa_servera+"/Korpa/Delete/"+this.sastavKorpe[i].id)
            .subscribe((x)=>{
              setTimeout(() => {
                window.location.reload();
              }, 1000);
            },(y)=>{
              porukaError("Greska prilikom brisanja korpe");
            })
      }
    }
  }
  ImePrezimeKorisnika(korisnik:any)
  {
    var prikazImena:any;
    for(let i=0;i<this.sviKorisnici.length;i++)
    {
      if(this.sviKorisnici[i].id===korisnik)
      {
        prikazImena=this.sviKorisnici[i].ime+" "+this.sviKorisnici[i].prezime;
      }
    }
    return prikazImena;

  }
  Poziv(meni_id:number[], kolicina:number[])
  {
    const menuDetails = [];

    for (let j = 0; j < meni_id.length; j++) {
      const currentMeniId = meni_id[j];
      const currentKolicina = kolicina[j];

      for (let i = 0; i < this.sviMeni.length; i++) {
        if (this.sviMeni[i].id === currentMeniId) {
          const menuInfo = {
            Name: this.sviMeni[i].naziv,
            Description: this.sviMeni[i].opis,
            Price: this.sviMeni[i].cijena,
            Quantity: currentKolicina,
          };
          menuDetails.push(menuInfo);
        }
      }
    }

    return menuDetails;
  }
  GetSveKorisnike()
{
  this.httpKlijent.get(MojConfig.adresa_servera+"/Korisnik/GetAll")
    .subscribe(x=>{
      this.sviKorisnici=x;
    })

}
  GetKorisnici() {
    this.httpKlijent
      .get(MojConfig.adresa_servera + '/Korisnik/GetById?id=' + this.loginInformation.autentifikacijaToken.korisnickiNalogId)
      .subscribe((x) => {
        this.korisnik = x;
        this.DohvatiDostavu();
      });
  }
  dohvatiKorpu()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+"/Korpa/GetAll")
      .subscribe((x)=>{
        this.sastavKorpe=x;
        this.Ispis();
      })
  }
  Ispis() {
      for (let i = 0; i < this.sastavKorpe.length; i++) {
       if (this.sastavKorpe[i].korisnik_id === this.loginInformation.autentifikacijaToken.korisnickiNalogId) {
         const meniId = this.sastavKorpe[i].meni_id;
          this.UhvatiMeni(meniId);

           }
      }
  }
  TajKorisnik() {
    for (let i = 0; i < this.sastavKorpe.length; i++) {
      if (this.sastavKorpe[i].korisnik_id === this.loginInformation.autentifikacijaToken.korisnickiNalogId) {
        for (let j = 0; j < this.sviMeni.length; j++) {
          if (this.sastavKorpe[i].meni_id === this.sviMeni[j].id) {
            this.dostavaMeniSastav.push(this.sviMeni[j]);

          }
        }
      }
    }
  }
  UhvatiMeni(meniId: number) {
    this.httpKlijent.get(MojConfig.adresa_servera + "/Meni/GetById/" + meniId)
      .subscribe((x) => {
        this.korpaMeniResults.push(x);
      });
  }
  calculateUkupnaCijena() {
    let ukupnaCijena = 0;

    const loggedUserId = this.loginInformation.autentifikacijaToken.korisnickiNalogId;

    for (let i = 0; i < this.korpaMeniResults.length; i++) {
      const meniId = this.korpaMeniResults[i].id;

      for (let j = 0; j < this.sastavKorpe.length; j++) {
        const sastav = this.sastavKorpe[j];

        if (sastav.korisnik_id === loggedUserId && sastav.meni_id === meniId) {
          ukupnaCijena += this.korpaMeniResults[i].cijena * sastav.kolicina;
        }
      }
    }
    ukupnaCijena = parseFloat(ukupnaCijena.toFixed(2));
    this.ukupnaVrijednostDostave=ukupnaCijena;
    return ukupnaCijena;
  }
  getSastavKorpaKolicina(meniId: number): number {
    const sastav = this.sastavKorpe.find((item:any) => item.meni_id === meniId);
    return sastav ? sastav.kolicina : 0;
  }

  saveEditableQuantity() {
    for (let i=0; i<this.sastavKorpe.length;i++)
    {
      for (let j=0;j<this.meniUKategoriji.length;j++)
      {
        if(this.sastavKorpe[i].meni_id===this.meniUKategoriji[j].id&&this.sastavKorpe[i].korisnik_id===this.loginInformation.autentifikacijaToken.korisnickiNalogId)
        {
        if(this.sastavKorpe[i].meni_id===this.selectedResult.id)
          {
            this.nova=this.sastavKorpe[i].id;
            this.sastavKorpe[i].kolicina=this.editableQuantity;

            console.log(this.nova);
            console.log(this.sastavKorpe)
            this.httpKlijent.put(MojConfig.adresa_servera+"/Korpa/UpdateKolicina/"+this.nova,this.sastavKorpe[i])
              .subscribe((x)=>{
                porukaSuccess("Uspjesno");
                this.dohvatiSveMeni();
                this.formaNova=false;
              },(y)=>{
                porukaError("Greska");
              })


          }
        }
      }
    }
  }
Delete(id:any)
{
  console.log(id);
  const nadji= this.sastavKorpe.find((result:any)=>result.meni_id===id);
  this.izbrisiId=nadji.id;
  console.log(nadji);
  console.log(this.izbrisiId);
  if(this.loginInformation.autentifikacijaToken.korisnickiNalogId===nadji.korisnik_id)
  {
      this.httpKlijent.delete(MojConfig.adresa_servera+"/Korpa/Delete/"+ this.izbrisiId)
        .subscribe((x)=>{
          porukaSuccess("Uspjesno");
          setTimeout(() => {
            window.location.reload();
          }, 3000);
        },(y)=>{
          porukaError("Greska");
        })
  }


}
openEditModal(result: any) {
    this.selectedResult = result;
    this.editableQuantity = this.getSastavKorpaKolicina(result.id);
  }
DodajKorpa(meni:any)
{
  this.slanjeUKorpuu.meni_id = meni;
  this.slanjeUKorpuu.korisnik_id = this.loginInformation.autentifikacijaToken.korisnickiNalogId;
  const specificMeni = this.meniUKategoriji.find((item) => item.id === meni);

  if (specificMeni) {
    this.slanjeUKorpuu.Kolicina = specificMeni.Kolicina;
  } else {
    this.slanjeUKorpuu.Kolicina = 0;
  }

  this.httpKlijent.post(MojConfig.adresa_servera+"/Korpa/DodajKorpu",this.slanjeUKorpuu)
    .subscribe((x)=>{
      porukaSuccess("Poslano");
      setTimeout(() => {
        window.location.reload();
      }, 3000);
    },(y)=>{
      porukaError("Greska");
    })
}
 postaviDefaultnuKolicinu(meni: any) {
    if (!meni.kolicina || meni.kolicina < 1) {
      meni.kolicina = 1;
    }
  }
  dohvatiKategorije() {
    this.httpKlijent.get(MojConfig.adresa_servera + '/Kategorija/GetAll').subscribe((data: any) => {
      this.kategorije = data;
    });
  }
  dohvatiSveMeni() {
    this.httpKlijent.get(MojConfig.adresa_servera + '/Meni/GetAll').subscribe((data: any) => {
      this.sviMeni = data;
    });
  }
  odaberiKategoriju(kategorija: any) {
    this.odabranaKategorija = kategorija;
    this.meniUKategoriji = this.sviMeni.filter((meni) => meni.kategorija_id === kategorija.id);
  }
  sanitizeImage(imageData: string): SafeUrl {
    if (imageData) {
      const imageUrl = 'data:image/png;base64,' + imageData;
      return this.sanitizer.bypassSecurityTrustUrl(imageUrl);
    }
    return '';
  }
}

