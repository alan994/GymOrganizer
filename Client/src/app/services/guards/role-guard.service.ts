import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, Router, ActivatedRoute } from '@angular/router';

import { Store } from '@ngrx/store';
import { OAuthService } from 'angular-oauth2-oidc';
import * as _ from 'lodash';

import { Observable } from 'rxjs/Observable';
import {mergeMap} from 'rxjs/operators';

import * as fromApp from '../../store/app.reducers';
import { Account } from '../../models/web-api/account';
import { Logger } from '../../services/utils/log.service';
import { ApplicationRole } from '../../models/enums/application-role';

@Injectable()
export class RoleGuard implements CanActivate {

	constructor(private oAuthService: OAuthService,
		private router: Router,
		private activatedRoute: ActivatedRoute,
		private store: Store<fromApp.AppState>,
		private logger: Logger) { }

	canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) {
		const requiredRole = route.data.roles;
		return this.store.select(s => s.accountState.account)
			.map((account: Account): boolean => {
				if (!requiredRole) {
					this.logger.debug('Pušteno od strane role guarda. Nema traženih rola');
					return true;
				}

				const intersection = _.intersection(requiredRole, account.roles);
				if (intersection && intersection.length !== 0) {
					this.logger.debug('Pušteno od strane role guarda. Korisnik ima tražene role', intersection);
					return true;
				}
				this.logger.debug('Odbijeno od strane role guarda');
				return false;
			});
	}
}
