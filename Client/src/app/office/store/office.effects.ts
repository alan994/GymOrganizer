import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { mergeMap } from 'rxjs/operators/mergeMap';
import { switchMap } from 'rxjs/operators/switchMap';

import { Effect, Actions } from '@ngrx/effects';
import * as RouterActions from '../../store/router/router.actions';
import * as OfficeActions from './office.actions';
import { OfficeService } from '../../services/web-api/office.service';

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

  constructor(private actions$: Actions, private router: Router, private officeService: OfficeService) { }
}
