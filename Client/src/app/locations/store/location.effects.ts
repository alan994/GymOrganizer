import { Injectable } from '@angular/core';
import { Actions, Effect } from '@ngrx/effects';
import { Router } from '@angular/router';
import * as LocationAcctions from './location.actions';
import { LocationService } from '../../services/location.service';
import { Location } from '../../models/Location';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/mergeMap';

@Injectable()
export class LocationEffects {
    @Effect()
    getLocations = this.actions$
        .ofType(LocationAcctions.GET_ALL_TRIGGER)
        .switchMap(() => {
            return this.locationService.getLocations();
        })
        .mergeMap((locations: Location[]) => {
            return [
                {
                    type: LocationAcctions.GET_ALL,
                    payload: locations
                }
            ];
        });

    constructor(private actions$: Actions, private router: Router, private locationService: LocationService) { }
}