import { Action } from '@ngrx/store';

import * as UserActions from './user.actions';
import { Account } from '../../models/web-api/account';
import { User } from '../../models/web-api/User';

export interface State {
	users: User[];
}

const initialState: State = {
	users: []
};

export function UserReducer(state: State = initialState, action: UserActions.Actions) {
	switch (action.type) {
		case UserActions.SAVE_GET_USERS:
			return {
				...state,
				users: action.payload
			};
		default:
			return state;
	}
}
