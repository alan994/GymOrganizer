import { Action } from '@ngrx/store';

import * as TenantActions from './tenant.actions';
import { Tenant } from '../../models/web-api/tenant';


export interface State {
	tenants: Tenant[];
}

const initialState: State = {
	tenants: []
};

export function tenantReducer(state: State = initialState, action: TenantActions.Actions) {
	switch (action.type) {
		case TenantActions.SAVE_GET_TENANTS:
			return {
				...state,
				tenants: action.payload
			};
		default:
			return state;
	}
}
