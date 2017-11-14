import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { mergeMap } from 'rxjs/operators/mergeMap';
import { switchMap } from 'rxjs/operators/switchMap';

import { Effect, Actions } from '@ngrx/effects';
import * as RouterActions from '../router/router.actions';
import * as AccountActions from './account.actions';
import { AccountService } from '../../services/web-api/account.service';

@Injectable()
export class AccountEffects {
  @Effect()
  loadBook = this.actions$
    .ofType(AccountActions.LOAD_GET_USER_PROFILE)
    .pipe(
    switchMap(() => {
      return this.accountService.getUserProfile();
    }),
    mergeMap((account: any) => {

      let navigateToRoute = ['/home'];
      if (localStorage.getItem('navigateToRoute')) {
        const pathArray: string[] = (<string>localStorage.getItem('navigateToRoute')).split('/');
        localStorage.removeItem('navigateToRoute');
        navigateToRoute = [];
        for (let i = 0; i < pathArray.length; i++) {
          navigateToRoute.push(i === 0 ? '/' : '' + pathArray[i]);
        }
      }
      return [
        {
          type: AccountActions.SAVE_GET_USER_PROFILE,
          payload: account
        },
        new RouterActions.Go({
          path: navigateToRoute
        })
      ];
    })
    );

  constructor(private actions$: Actions, private router: Router, private accountService: AccountService) { }
}
