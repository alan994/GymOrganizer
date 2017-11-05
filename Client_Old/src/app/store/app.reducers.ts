import { ActionReducerMap } from '@ngrx/store';
import * as fromLocation from '../locations/store/location.reducers';

export interface AppState {
    locationState: fromLocation.State;
}

export const reducers: ActionReducerMap<AppState> = {
    locationState: fromLocation.locationReducer
};