import { Component, OnInit } from '@angular/core';
import {MojConfig} from "../moj-config";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {LoginInformacije} from "../_helpers/login-informacije";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

@Component({
  selector: 'app-forum',
  templateUrl: './forum.component.html',
  styleUrls: ['./forum.component.css']
})
export class ForumComponent implements OnInit {
  teme: any;
  temaObject:any;
  clanovi=false;
  admini:any;
  koisnici:any;

  filter_tema=false;
  tema_naziv:"";
  filter_korisnik:boolean=false;
  korisnik_naziv:string="";

  filetr_clan=false;
  clan_ime:"";

  constructor(private httpKlijent:HttpClient, private router:Router) { }

  ngOnInit(): void {
    this.ucitajTeme();
    this.ucitajAdmine();
    this.ucitajKorisnike();
  }

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ucitajAdmine(){
    this.httpKlijent.get(MojConfig.adresa_servera +"/Admin/GetAll").subscribe(x=>{
      this.admini=x;
    })
  }
  ucitajKorisnike(){
    this.httpKlijent.get(MojConfig.adresa_servera +"/Korisnik/GetAll", MojConfig.http_opcije()).subscribe(x=>{
      this.koisnici=x;
    })
  }

  ucitajTeme(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/ForumTema/GetAll").subscribe(x=>{
      this.teme=x;
    })
  }

  otvoriTemu() {
    this.temaObject={
      tema:"",
      pitanje:"",
      korsinciki_nalog_id:this.loginInfo().autentifikacijaToken.korisnickiNalogId
    }
  }

  dodajTemu() {
    if(this.temaObject.tema!="" && this.temaObject.pitanje!=""){
      this.httpKlijent.post(MojConfig.adresa_servera + "/ForumTema/Dodaj", this.temaObject).subscribe(x=>{
        this.temaObject=null;
        this.ucitajTeme();
      })
    }
    else{
      alert("niste unijeli sve podatke");
    }

  }

  obrsisiTemu(t: any) {
    let con= confirm("Da li zelite obrisati temu?");
    if(con.valueOf()==true) {
      this.httpKlijent.post(MojConfig.adresa_servera + `/ForumTema/Obrisi?id=${t.id}`, null).subscribe(x => {

        this.ucitajTeme();
      })
    }
  }

  otvoriDiskujiu(t: any) {
    this.router.navigate(['/forum-odgovor',t.id]);
  }

  filtrirano() {
    if(this.teme==null)
      return [];
    return this.teme.filter(
      (x:any)=>
        (!this.filter_tema ||
          (x.tema).toLowerCase().startsWith(this.tema_naziv.toLowerCase())
        ) && (!this.filter_korisnik ||
          (x.autor).toLowerCase().startsWith(this.korisnik_naziv.toLowerCase())
        )) ;
  }
  filtriranoKorsinici() {
    if(this.koisnici==null)
      return [];
    return this.koisnici.filter(
      (x:any)=>
        (!this.filetr_clan ||
          (x.ime).toLowerCase().startsWith(this.clan_ime.toLowerCase() ) || (x.prezime).toLowerCase().startsWith(this.clan_ime.toLowerCase()
          ) ));
  }
  filtriranoAdmini() {
    if(this.admini==null)
      return [];
    return this.admini.filter(
      (x:any)=>
        (!this.filetr_clan ||
          (x.ime).toLowerCase().startsWith(this.clan_ime.toLowerCase() ) || (x.prezime).toLowerCase().startsWith(this.clan_ime.toLowerCase()
          ) ));
  }
}
