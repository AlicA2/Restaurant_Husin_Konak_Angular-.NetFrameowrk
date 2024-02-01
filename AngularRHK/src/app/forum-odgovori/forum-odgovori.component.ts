import { Component, OnInit } from '@angular/core';
import {HttpClient} from "@angular/common/http";
import {ActivatedRoute} from "@angular/router";
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";
import {MojConfig} from "../moj-config";

@Component({
  selector: 'app-forum-odgovori',
  templateUrl: './forum-odgovori.component.html',
  styleUrls: ['./forum-odgovori.component.css']
})
export class ForumOdgovoriComponent implements OnInit {
  forumID: number;
  forumOdgovori: any;
  forumOdgovorObject:any;

  constructor(private httpKlijent:HttpClient, private route:ActivatedRoute) { }

  ngOnInit(): void {
    this.route.params.subscribe(params => {
      this.forumID = +params['forumid'];
    });
    this.ucitajForumOdgovorTema();
  }
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  ucitajForumOdgovorTema(){
    this.httpKlijent.get(MojConfig.adresa_servera + "/ForumTema/GetPorukaOdgovori?id=" + this.forumID).subscribe(x=>{
      this.forumOdgovori=x;

    });
  }

  odgovori() {
    this.forumOdgovorObject={
      odgovor:"",
      autor_name:this.loginInfo().autentifikacijaToken.korisnickiNalog.korisnickoIme,
      forum_tema_id:this.forumID
    }
  }

  dodajForumOdgovor() {
    this.httpKlijent.post(MojConfig.adresa_servera + "/ForumOdgovor/Dodaj", this.forumOdgovorObject).subscribe(x=>{
      this.ucitajForumOdgovorTema();
      this.forumOdgovorObject=null;
    });
  }

  obrisiOdgovor(o: any) {
    let con= confirm("Da li zelite obrisati odgovor?");
    if(con.valueOf()==true) {
      this.httpKlijent.post(MojConfig.adresa_servera + "/ForumOdgovor/Obrisi?id=" + o.id, null).subscribe(x => {
        this.ucitajForumOdgovorTema();
      })
    }
  }
}
