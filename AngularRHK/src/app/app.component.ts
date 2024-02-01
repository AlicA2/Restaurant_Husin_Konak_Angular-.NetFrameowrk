import { Component } from '@angular/core';
import {AutentifikacijaHelper} from "./_helpers/autentifikacija-helper";
import {LoginInformacije} from "./_helpers/login-informacije";
import {MojConfig} from "./moj-config";
import {HttpClient} from "@angular/common/http";
import {Router} from "@angular/router";

//declare function porukaSuccess(a: string):any;
//declare function porukaError(a: string):any;

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  constructor(private httpKlijent:HttpClient, private router:Router) {
  }
  loginInformation = AutentifikacijaHelper.getLoginInfo();

  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  odjava() {
    let token = MojConfig.http_opcije();
    AutentifikacijaHelper.setLoginInfo(null);

    this.httpKlijent.post(MojConfig.adresa_servera + "/Autentifikacija/Logout/", null, token)
      .subscribe((x: any) => {
        this.router.navigateByUrl("/login");
        //porukaSuccess("logout uspjesan");
      });
  }

}
