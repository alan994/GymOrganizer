import { Component, OnInit } from '@angular/core';
import { OAuthService } from 'angular-oauth2-oidc';

@Component({
  selector: 'navigation',
  templateUrl: './nav.component.html',
  styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

  claims: any;

  constructor(private oAuthService: OAuthService) {
  }

  ngOnInit() {
    this.claims = <any>this.oAuthService.getIdentityClaims();
    console.log('Constructor: ', this.claims);
  }

  login() {
    this.oAuthService.initImplicitFlow();
  }

  logout() {
    this.oAuthService.logOut();
  }

  
  public get username() {
    if (!this.claims) {
      return null;
    }
    console.log('User claims: ', this.claims);
    return this.claims.given_name;
  }
}
