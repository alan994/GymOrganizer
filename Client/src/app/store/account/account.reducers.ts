import { Action } from '@ngrx/store';

import * as AccountActions from './account.actions';
import { Account } from '../../models/web-api/account';

export interface State {
	account: Account;
	isAuthenticated: boolean;
}

const initialState: State = {
	account: null,
	isAuthenticated: false
};

export function bookReducer(state: State = initialState, action: AccountActions.Actions) {
	switch (action.type) {
		case AccountActions.SAVE_GET_USER_PROFILE:
			return {
				...state,
				account: action.payload,
				isAuthenticated: true
			};
		default:
			return state;
	}
}
