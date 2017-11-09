import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import 'rxjs/add/operator/mergemap';
import { Effect, Actions } from '@ngrx/effects';

import * as AccountActions from './account.actions';
import { AccountService } from '../../services/web-api/account.service';

@Injectable()
export class AccountEffects {
	@Effect()
	loadBook = this.actions$
		.ofType(AccountActions.LOAD_GET_USER_PROFILE)
		.switchMap(() => {
			return this.accountService.getUserProfile();
		})
		.mergeMap((account: any) => {
			return [
				{
					type: AccountActions.SAVE_GET_USER_PROFILE,
					payload: account
				}
			];
		});

	constructor(private actions$: Actions, private router: Router, private accountService: AccountService) { }
}
