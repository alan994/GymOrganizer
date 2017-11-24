import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { mergeMap } from 'rxjs/operators/mergeMap';
import { switchMap } from 'rxjs/operators/switchMap';

import { Effect, Actions } from '@ngrx/effects';
import * as RouterActions from '../../store/router/router.actions';
import * as TermActions from './Term.actions';
import { TermService } from '../../services/web-api/Term.service';

import { Term } from '../../models/web-api/Term';
import { Logger } from '../../services/utils/log.service';

@Injectable()
export class TermEffects {
	@Effect()
	loadTerms = this.actions$
		.ofType(TermActions.LOAD_GET_TERMS)
		.pipe(
		switchMap(() => {
			return this.termService.getAllTerms();
		}),
		mergeMap((terms: Term[]) => {
			return [
				{
					type: TermActions.SAVE_GET_TERMS,
					payload: terms
				}
			];
		})
		);

	@Effect({ dispatch: false })
	addTerm = this.actions$
		.ofType(TermActions.ADD_TERM)
		.map((action: TermActions.AddTerm) => {
			return action.payload;
		})
		.map((payload: Term) => {
			return this.termService.addTerm(payload);
		})
		.map(() => {
			this.logger.info('Adding Term in progress');
		});

	@Effect({ dispatch: false })
	editTerm = this.actions$
		.ofType(TermActions.EDIT_TERM)
		.map((action: TermActions.EditTerm) => {
			return action.payload;
		})
		.map((payload: Term) => {
			return this.termService.editTerm(payload);
		})
		.map(() => {
			this.logger.info('Editing Term in progress');
		});


	@Effect({ dispatch: false })
	deleteTerm = this.actions$
		.ofType(TermActions.DELETE_TERM)
		.map((action: TermActions.DeleteTerm) => {
			return action.payload;
		})
		.map((payload: string) => {
			return this.termService.deleteTerm(payload);
		})
		.map(() => {
			this.logger.info('Deleting Term in progress');
		});

	constructor(private actions$: Actions, private router: Router, private termService: TermService, private logger: Logger) { }
}
