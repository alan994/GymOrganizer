import { Action } from '@ngrx/store';

import * as TermActions from './term.actions';
import { Account } from '../../models/web-api/account';
import { Term } from '../../models/web-api/term';

export interface State {
	terms: Term[];
}

const initialState: State = {
	terms: []
};

export function TermReducer(state: State = initialState, action: TermActions.Actions) {
	switch (action.type) {
		case TermActions.SAVE_GET_TERMS:
			return {
				...state,
				terms: action.payload
			};
		default:
			return state;
	}
}
