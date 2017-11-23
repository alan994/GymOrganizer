import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { mergeMap } from 'rxjs/operators/mergeMap';
import { switchMap } from 'rxjs/operators/switchMap';

import { Effect, Actions } from '@ngrx/effects';
import * as RouterActions from '../../store/router/router.actions';
import * as OfficeActions from './office.actions';
import { OfficeService } from '../../services/web-api/office.service';

import { Office } from '../../models/web-api/office';
import { Logger } from '../../services/utils/log.service';

@Injectable()
export class OfficeEffects {
	@Effect()
	loadOffices = this.actions$
		.ofType(OfficeActions.LOAD_GET_OFFICES)
		.pipe(
		switchMap(() => {
			return this.officeService.getAllActiveOffices();
		}),
		mergeMap((account: any) => {
			return [
				{
					type: OfficeActions.SAVE_GET_OFFICES,
					payload: account
				}
			];
		})
		);

	@Effect({ dispatch: false })
	addOffice = this.actions$
		.ofType(OfficeActions.ADD_OFFICE)
		.map((action: OfficeActions.AddOffice) => {
			return action.payload;
		})
		.map((payload: Office) => {
			return this.officeService.addOffice(payload);
		})
		.map(() => {
			this.logger.info('Adding office in progress');
		});

	@Effect({ dispatch: false })
	editOffice = this.actions$
		.ofType(OfficeActions.EDIT_OFFICE)
		.map((action: OfficeActions.EditOffice) => {
			return action.payload;
		})
		.map((payload: Office) => {
			return this.officeService.editOffice(payload);
		})
		.map(() => {
			this.logger.info('Editing office in progress');
		});


	@Effect({ dispatch: false })
	deleteOffice = this.actions$
		.ofType(OfficeActions.DELETE_OFFICE)
		.map((action: OfficeActions.DeleteOffice) => {
			return action.payload;
		})
		.map((payload: string) => {
			return this.officeService.deleteOffice(payload);
		})
		.map(() => {
			this.logger.info('Deleting office in progress');
		});

	constructor(private actions$: Actions, private router: Router, private officeService: OfficeService, private logger: Logger) { }
}
