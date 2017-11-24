import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { mergeMap } from 'rxjs/operators/mergeMap';
import { switchMap } from 'rxjs/operators/switchMap';

import { Effect, Actions } from '@ngrx/effects';
import * as RouterActions from '../../store/router/router.actions';
import * as CountryActions from './country.actions';
import { Country } from '../../models/web-api/country';
import { Logger } from '../../services/utils/log.service';
import { CountryService } from '../../services/web-api/country.service';

@Injectable()
export class CountryEffects {
	@Effect()
	loadCountries = this.actions$
		.ofType(CountryActions.LOAD_GET_COUNTRIES)
		.pipe(
		switchMap(() => {
			return this.countryService.getAllCountries();
		}),
		mergeMap((Countries: Country[]) => {
			return [
				{
					type: CountryActions.SAVE_GET_COUNTRIES,
					payload: Countries
				}
			];
		})
		);

	@Effect({ dispatch: false })
	addCountry = this.actions$
		.ofType(CountryActions.ADD_COUNTRY)
		.map((action: CountryActions.AddCountry) => {
			return action.payload;
		})
		.map((payload: Country) => {
			return this.countryService.addCountry(payload);
		})
		.map(() => {
			this.logger.info('Adding country in progress');
		});

	@Effect({ dispatch: false })
	editCountry = this.actions$
		.ofType(CountryActions.EDIT_COUNTRY)
		.map((action: CountryActions.EditCountry) => {
			return action.payload;
		})
		.map((payload: Country) => {
			return this.countryService.editCountry(payload);
		})
		.map(() => {
			this.logger.info('Editing country in progress');
		});


	@Effect({ dispatch: false })
	deleteCountry = this.actions$
		.ofType(CountryActions.DELETE_COUNTRY)
		.map((action: CountryActions.DeleteCountry) => {
			return action.payload;
		})
		.map((payload: string) => {
			return this.countryService.deleteCountry(payload);
		})
		.map(() => {
			this.logger.info('Deleting country in progress');
		});

	constructor(private actions$: Actions, private router: Router, private countryService: CountryService, private logger: Logger) { }
}
