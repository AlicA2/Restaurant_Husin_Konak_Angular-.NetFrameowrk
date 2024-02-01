import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import  {RouterModule, RouterOutlet} from "@angular/router";
import {FormsModule} from "@angular/forms";
import {HttpClientModule} from "@angular/common/http";
import { LoginInformacije } from "./_helpers/login-informacije";
import { UserAuthService } from "./user-auth.service";
import { AppComponent } from './app.component';
import { LoginComponent } from './login/login.component';
import {ReactiveFormsModule} from "@angular/forms";
import { RegistracijaAdminComponent } from './registracija-admin/registracija-admin.component';
import { RegistracijaKorisnikComponent } from './registracija-korisnik/registracija-korisnik.component';
import { PostavkeProfilaComponent } from './postavke-profila/postavke-profila.component';
import { PostavkaAdminComponent } from './postavke-profila/postavka-admin/postavka-admin.component';
import { PostavkaKorisnikComponent } from './postavke-profila/postavka-korisnik/postavka-korisnik.component';
import { KorisniciComponent } from './korisnici/korisnici.component';
import { GalerijaComponent } from './galerija/galerija.component';
import { PocetnaComponent } from './pocetna/pocetna.component';
import { AdminComponent } from './pocetna/admin/admin.component';
import { TwoFOtkljucajComponent } from './two-f-otkljucaj/two-f-otkljucaj.component';
import { KontaktComponent } from "./kontakt/kontakt.component";
import { MeniComponent } from './meni/meni.component';
import { ForumComponent } from './forum/forum.component';
import { ForumOdgovoriComponent } from './forum-odgovori/forum-odgovori.component';
import { DostavaComponent } from './dostava/dostava.component';
import { ONamaComponent} from './o-nama/onama.component';
import { RezervacijaComponent } from './rezervacija/rezervacija.component';
@NgModule({
  declarations: [
    AppComponent,
    LoginComponent,
    RegistracijaAdminComponent,
    RegistracijaKorisnikComponent,
    PostavkeProfilaComponent,
    PostavkaAdminComponent,
    PostavkaKorisnikComponent,
    KorisniciComponent,
    PocetnaComponent,
    AdminComponent,
    GalerijaComponent,
    TwoFOtkljucajComponent,
    KontaktComponent,
    MeniComponent,
    ForumComponent,
    ForumOdgovoriComponent,
    DostavaComponent,
    ONamaComponent,
    RezervacijaComponent
  ],
  imports: [
    BrowserModule,
    HttpClientModule,
    FormsModule,
    ReactiveFormsModule,
    RouterModule.forRoot([
      {path: 'login', component: LoginComponent},
      {path: 'registracija-admin', component: RegistracijaAdminComponent},
      {path: 'registracija-korisnik', component: RegistracijaKorisnikComponent},
      {path: 'postavke-profila', component: PostavkeProfilaComponent},
      {path: 'korisnici', component: KorisniciComponent},
      {path:"pocetna", component:PocetnaComponent},
      {path:"galerija", component:GalerijaComponent},
      {path: "two-f-otkljucaj", component: TwoFOtkljucajComponent},
      {path: "kontakt", component:KontaktComponent},
      {path: "meni", component:MeniComponent},
      {path: "forum", component:ForumComponent},
      {path: "forum-odgovor/:forumid", component: ForumOdgovoriComponent},
      {path: "dostava", component: DostavaComponent},
      {path: "o-nama", component: ONamaComponent},
      {path: "rezervacija", component: RezervacijaComponent},

    ])
  ],
  providers: [LoginInformacije, UserAuthService],
  bootstrap: [AppComponent]
})
export class AppModule { }
