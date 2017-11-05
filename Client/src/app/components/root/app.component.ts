import { Component, ViewContainerRef, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

import { OAuthService } from 'angular-oauth2-oidc';

import { Logger } from '../../services/utils/log.service';
import { JwksValidationHandler } from 'angular-oauth2-oidc';
import { authConfig } from '../../models/config/auth-config';

@Component({
	selector: 'go-root',
	templateUrl: './app.component.html',
	styleUrls: ['./app.component.scss']
})
export class AppComponent implements OnInit {
	constructor(vRef: ViewContainerRef, private oAuthService: OAuthService, private activatedRoute: ActivatedRoute, private logger: Logger, private router: Router) {
		// this.oAuthService.issuer = 'http://localhost:5000';
		// this.oAuthService.clientId = 'angular-client';

		// this.oAuthService.redirectUri = 'http://' + document.location.host + '/loading';
		// this.oAuthService.postLogoutRedirectUri = window.location.origin;
		// this.oAuthService.scope = 'openid profile email api.fullAccess';
		// this.oAuthService.silentRefreshRedirectUri = window.location.origin + '/silent-refresh.html';
		// //this.oAuthService.timeoutFactor = 0.1;
		// this.oAuthService.requireHttps = false;

		this.oAuthService.configure({
			issuer: 'http://localhost:5000',
			redirectUri: window.location.origin + '/home',
			clientId: 'angular-client',
			scope: 'openid profile email api.fullAccess',
		});
		this.oAuthService.tokenValidationHandler = new JwksValidationHandler();
		this.oAuthService.loadDiscoveryDocumentAndTryLogin();
		//this.oAuthService.setupAutomaticSilentRefresh();
		this.oAuthService.events.subscribe(e => {
			this.logger.debug('oauth/oidc event', e);
		});
	}
	ngOnInit(): void {

		//this.signalRService.init();
		// this.oAuthService.tryLogin().then((success: any)  => {
		// 	this.logger.debug('Try login success', success);
		// }, (error: any)  => {
		// 	this.logger.error('Error try login: ', error);
		// });
	}
}