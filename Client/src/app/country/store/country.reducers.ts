import { Action } from '@ngrx/store';

import * as CountryActions from './country.actions';
import { Country } from '../../models/web-api/country';

export interface State {
	countries: Country[];
}

const initialState: State = {
	countries: []
};

export function officeReducer(state: State = initialState, action: CountryActions.Actions) {
	switch (action.type) {
		case CountryActions.SAVE_GET_COUNTRIES:
			return {
				...state,
				countries: action.payload
			};
		default:
			return state;
	}
}
