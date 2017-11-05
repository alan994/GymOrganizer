import { Component, OnInit, ViewContainerRef } from '@angular/core';

import { OAuthService } from 'angular-oauth2-oidc';
import { Store } from '@ngrx/store';

import * as fromApp from '../../store/app.reducers';
import * as AccountActions from '../../store/account/account.actions';

@Component({
	selector: 'go-welcome',
	templateUrl: './welcome.component.html',
	styleUrls: ['./welcome.component.scss']
})
export class WelcomeComponent implements OnInit {
	constructor(private oAuthService: OAuthService) {

	}

	ngOnInit(): void {
	}

	public login() {
		this.oAuthService.initImplicitFlow();
	}
}
