import { Component, OnInit } from '@angular/core';
import { HttpClient } from "@angular/common/http";
import { Router } from "@angular/router";
import { LoginInformacije } from "../_helpers/login-informacije";
import { AutentifikacijaHelper } from "../_helpers/autentifikacija-helper";
import { MojConfig } from "../moj-config";
import { UserAuthService } from '../user-auth.service';

declare function porukaSuccess(a: string): any;
declare function porukaError(a: string): any;

@Component({
  selector: 'app-contact-form',
  templateUrl: './kontakt.component.html',
  styleUrls: ['./kontakt.component.css']
})
export class KontaktComponent implements OnInit {
  isVisible = false;
  animationStyles = {};

  kontaktVM = {
    Ime: '',
    Prezime: '',
    Email: '',
    Telefon: '',
    Poruka: '',
    korisnikID: null as number | null
  };
  loginInformation = AutentifikacijaHelper.getLoginInfo();
  constructor(private httpKlijent: HttpClient,
              private router: Router,
              private loginInformacije: LoginInformacije,
              private userAuthService: UserAuthService) { }

  onEmailInput(event: any) {

    const inputElement = event.target;
    let inputValue = inputElement.value;

    if (inputValue.length === 1 && !inputValue.includes('@gmail.com')) {
      inputValue += '@gmail.com';

      this.kontaktVM.Email = inputValue;

      setTimeout(() => {
        const cursorPosition = inputValue.indexOf('d') + 1;
        inputElement.setSelectionRange(cursorPosition, cursorPosition);
      });
    }
  }

  ngOnInit() {
    this.GetContactPoruke();
    this.isVisible = true;
    this.animationStyles = {
      'visibility': 'visible',
      'animation-duration': '1000ms',
      'animation-delay': '300ms',
      'animation-name': 'fadeInDown'
    };
  }
  kontaktPoruke:any;
  GetContactPoruke()
  {
    this.httpKlijent.get(MojConfig.adresa_servera+'/Kontakt/GetAll',MojConfig.http_opcije())
      .subscribe(x=>
        this.kontaktPoruke=x)
  }

  submitKontaktForm() {
    if (!this.validateForm()) {
      return;
    }

    this.kontaktVM.korisnikID = this.loginInformation.autentifikacijaToken.korisnickiNalogId;
    console.log(this.kontaktVM);
    this.httpKlijent.post(MojConfig.adresa_servera + "/Kontakt/DodajKontakt", this.kontaktVM).subscribe(
      (response: any) => {
        porukaSuccess(response.message);
        this.resetForm();
        this.router.navigate(['/kontakt']);

      },
      (error) => {
        porukaError("Greška prilikom slanja kontakta");
      }
    );
  }
  validateForm() {
    if (!this.kontaktVM.Ime || !this.kontaktVM.Prezime || !this.kontaktVM.Email || !this.kontaktVM.Telefon || !this.kontaktVM.Poruka) {
      porukaError("Molimo popunite sva polja.");
      return false;
    }
    return true;
  }
  resetForm() {
    this.kontaktVM = {
      Ime: '',
      Prezime: '',
      Email: '',
      Telefon: '',
      Poruka: '',
      korisnikID: null
    };
  }
}

