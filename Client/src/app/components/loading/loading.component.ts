import { Component, OnInit, ViewContainerRef } from '@angular/core';
import { Router, UrlSegment } from '@angular/router';

import { OAuthService } from 'angular-oauth2-oidc';
import { Store } from '@ngrx/store';
import * as _ from 'lodash';

import { AccountService } from '../../services/web-api/account.service';
import * as fromApp from '../../store/app.reducers';
import * as AccountActions from '../../store/account/account.actions';

@Component({
	selector: 'go-loading',
	templateUrl: './loading.component.html',
	styleUrls: ['./loading.component.scss']
})
export class LoadingComponent implements OnInit {
	constructor(private accountService: AccountService, private store: Store<fromApp.AppState>, private router: Router) { }

	ngOnInit(): void {
		// this.accountService.getUserProfile().subscribe(data => {
		// 	this.store.dispatch(new AccountActions.SaveGetUserProfile(data));
		// 	let navigateToRoute = ['/home'];
		// 	if (localStorage.getItem('navigateToRoute')) {
		// 		const pathArray: string[] = (<string>localStorage.getItem('navigateToRoute')).split('/');
		// 		localStorage.removeItem('navigateToRoute');
		// 		navigateToRoute = [];
		// 		for (let i = 0; i < pathArray.length; i++) {
		// 			navigateToRoute.push(i === 0 ? '/' : '' + pathArray[i]);
		// 		}
		// 	}
		// 	this.router.navigate(navigateToRoute);
		// });
		this.router.navigate(['/home']);
	}
}
