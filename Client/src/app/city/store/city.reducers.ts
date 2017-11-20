import { Action } from '@ngrx/store';

import * as CityActions from './city.actions';
import { City } from '../../models/web-api/city';

export interface State {
  cities: City[];
}

const initialState: State = {
  cities: []
};

export function officeReducer(state: State = initialState, action: CityActions.Actions) {
  switch (action.type) {
    case CityActions.SAVE_GET_CITIES:
      return {
        ...state,
        cities: action.payload
      };
    default:
      return state;
  }
}
