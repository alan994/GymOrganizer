import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { mergeMap } from 'rxjs/operators/mergeMap';
import { switchMap } from 'rxjs/operators/switchMap';

import { Effect, Actions } from '@ngrx/effects';
import * as RouterActions from '../../store/router/router.actions';
import * as CityActions from './city.actions';
import { CityService } from '../../services/web-api/city.service';
import { City } from '../../models/web-api/city';
import { Logger } from '../../services/utils/log.service';

@Injectable()
export class CityEffects {
	@Effect()
	loadCities = this.actions$
		.ofType(CityActions.LOAD_GET_CITIES)
		.pipe(
		switchMap(() => {
			return this.cityService.getAllCities();
		}),
		mergeMap((cities: City[]) => {
			return [
				{
					type: CityActions.SAVE_GET_CITIES,
					payload: cities
				}
			];
		})
		);

	@Effect({ dispatch: false })
	addCity = this.actions$
		.ofType(CityActions.ADD_CITY)
		.map((action: CityActions.AddCity) => {
			return action.payload;
		})
		.map((payload: City) => {
			return this.cityService.addCity(payload);
		})
		.map(() => {
			this.logger.info('Adding city in progress');
		});

	@Effect({ dispatch: false })
	editCity = this.actions$
		.ofType(CityActions.EDIT_CITY)
		.map((action: CityActions.EditCity) => {
			return action.payload;
		})
		.map((payload: City) => {
			return this.cityService.editCity(payload);
		})
		.map(() => {
			this.logger.info('Editing city in progress');
		});


	@Effect({ dispatch: false })
	deleteCity = this.actions$
		.ofType(CityActions.DELETE_CITY)
		.map((action: CityActions.DeleteCity) => {
			return action.payload;
		})
		.map((payload: string) => {
			return this.cityService.deleteCity(payload);
		})
		.map(() => {
			this.logger.info('Deleting city in progress');
		});

	constructor(private actions$: Actions, private router: Router, private cityService: CityService, private logger: Logger) { }
}
