import { Action } from '@ngrx/store';

import * as OfficeActions from './office.actions';
import { Account } from '../../models/web-api/account';
import { Office } from '../../models/web-api/office';

export interface State {
	offices: Office[];
}

const initialState: State = {
	offices: []
};

export function officeReducer(state: State = initialState, action: OfficeActions.Actions) {
	switch (action.type) {
		case OfficeActions.SAVE_GET_OFFICES:
			return {
				...state,
				offices: action.payload
			};
		default:
			return state;
	}
}
