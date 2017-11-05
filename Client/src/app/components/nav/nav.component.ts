import { Component, OnInit } from '@angular/core';

import { TranslateService } from '@ngx-translate/core';
import { Store } from '@ngrx/store';
import { OAuthService } from 'angular-oauth2-oidc';
import { Observable } from 'rxjs/Observable';

import * as fromApp from '../../store/app.reducers';
import { Account } from '../../models/web-api/account';

@Component({
	selector: 'go-nav',
	templateUrl: './nav.component.html',
	styleUrls: ['./nav.component.scss']
})
export class NavComponent implements OnInit {

	account: Observable<Account>;
	constructor(private translate: TranslateService, private oAuthService: OAuthService, private store: Store<fromApp.AppState>) {
	}

	ngOnInit() {
		this.translate.setDefaultLang(this.translate.getBrowserLang());
		console.log('Postavljen inicijalni jezik: ', this.translate.getBrowserLang());
		this.account = this.store.select(s => s.accountState.account);
	}

	public logout() {
		this.oAuthService.logOut();
	}

	changeLanguage(key: string) {
		this.translate.use(key);
		console.log(`Pomijenjen jezik na ${key}`);
	}
}
