import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router } from '@angular/router';

import { Store } from '@ngrx/store';
import { OAuthService } from 'angular-oauth2-oidc';

import * as fromApp from '../../store/app.reducers';
import * as fromAccount from '../../store/account/account.reducers';
import { Logger } from '../utils/log.service';
import * as AccountActions from '../../store/account/account.actions';

@Injectable()
export class AuthGuard implements CanActivate {

  constructor(private oAuthService: OAuthService, private router: Router, private store: Store<fromApp.AppState>, private logger: Logger) { }

  canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
    const isAllowed = this.oAuthService.getIdentityClaims() ? true : false;
    return this.store.select(s => s.accountState)
      .map((accountState: fromAccount.State) => {
        if (!accountState.isAuthenticated || !isAllowed) {
          this.logger.debug('Odbijeno od strane auth guarda. Korisnik je autenticiran');
          localStorage.setItem('navigateToRoute', location.pathname);

          if (!isAllowed) {
            this.oAuthService.initImplicitFlow();
          }
          else {
            this.store.dispatch(new AccountActions.LoadGetUserProfile());
          }
        }
        return accountState.isAuthenticated && isAllowed;
      });
  }
}
