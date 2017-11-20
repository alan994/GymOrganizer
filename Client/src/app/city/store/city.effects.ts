import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { mergeMap } from 'rxjs/operators/mergeMap';
import { switchMap } from 'rxjs/operators/switchMap';

import { Effect, Actions } from '@ngrx/effects';
import * as RouterActions from '../../store/router/router.actions';
import * as CityActions from './city.actions';
import { CityService } from '../../services/web-api/city.service';
import { City } from '../../models/web-api/city';

@Injectable()
export class CityEffects {
  @Effect()
  loadOffices = this.actions$
    .ofType(CityActions.LOAD_GET_CITIES)
    .pipe(
    switchMap(() => {
      return this.cityService.getAllActiveCities();
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

  constructor(private actions$: Actions, private router: Router, private cityService: CityService) { }
}
