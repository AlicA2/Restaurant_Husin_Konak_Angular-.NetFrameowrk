import { Component, OnInit } from '@angular/core';
import {LoginInformacije} from "../_helpers/login-informacije";
import {AutentifikacijaHelper} from "../_helpers/autentifikacija-helper";



@Component({
  selector: 'app-pocetna',
  templateUrl: './pocetna.component.html',
  styleUrls: ['./pocetna.component.css']
})
export class PocetnaComponent implements OnInit {
  loginInfo():LoginInformacije {
    return AutentifikacijaHelper.getLoginInfo();
  }

  constructor() {}
  ngOnInit(): void {
  }


}
