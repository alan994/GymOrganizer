import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { mergeMap } from 'rxjs/operators/mergeMap';
import { switchMap } from 'rxjs/operators/switchMap';

import { Effect, Actions } from '@ngrx/effects';
import * as RouterActions from '../../store/router/router.actions';
import * as TenantActions from './tenant.actions';

import { Logger } from '../../services/utils/log.service';
import { TenantService } from '../../services/web-api/tenant.service';
import { Tenant } from '../../models/web-api/tenant';

@Injectable()
export class TenantEffects {
	@Effect()
	loadTenant = this.actions$
		.ofType(TenantActions.LOAD_GET_TENANTS)
		.pipe(
		switchMap(() => {
			return this.tenantService.getAllTenants();
		}),
		mergeMap((tenants: Tenant[]) => {
			return [
				{
					type: TenantActions.SAVE_GET_TENANTS,
					payload: tenants
				}
			];
		})
		);

	@Effect({ dispatch: false })
	addTenant = this.actions$
		.ofType(TenantActions.ADD_TENANT)
		.map((action: TenantActions.AddTenant) => {
			return action.payload;
		})
		.map((payload: Tenant) => {
			return this.tenantService.addTenant(payload);
		})
		.map(() => {
			this.logger.info('Adding tenant in progress');
		});

	@Effect({ dispatch: false })
	editTenant = this.actions$
		.ofType(TenantActions.EDIT_TENANT)
		.map((action: TenantActions.EditTenant) => {
			return action.payload;
		})
		.map((payload: Tenant) => {
			return this.tenantService.editTenant(payload);
		})
		.map(() => {
			this.logger.info('Editing tenant in progress');
		});


	@Effect({ dispatch: false })
	deleteTenant = this.actions$
		.ofType(TenantActions.DELETE_TENANT)
		.map((action: TenantActions.DeleteTenant) => {
			return action.payload;
		})
		.map((payload: string) => {
			return this.tenantService.deleteTenant(payload);
		})
		.map(() => {
			this.logger.info('Deleting tenant in progress');
		});

	constructor(private actions$: Actions, private router: Router, private tenantService: TenantService, private logger: Logger) { }
}
