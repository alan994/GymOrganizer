import { Component, OnInit } from '@angular/core';

import { Store } from '@ngrx/store';
import { TranslateService } from '@ngx-translate/core';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs/Observable';

import {Account} from '../../models/web-api/account';
import * as fromApp from '../../store/app.reducers';

@Component({
	selector: 'go-home',
	templateUrl: './home.component.html',
	styleUrls: ['./home.component.scss']
})
export class HomeComponent implements OnInit {

	constructor(private oAuthService: OAuthService, private store: Store<fromApp.AppState>) { }

	account: Observable<Account>;

	ngOnInit() {
		this.account = this.store.select(s => s.accountState.account);
	}

	public login() {
		this.oAuthService.initImplicitFlow();
	}

	public logoff() {
		this.oAuthService.logOut();
	}

	getUserInfo() {
		const something = this.oAuthService.getIdentityClaims();
		console.log(something);
	}
}
