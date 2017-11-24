import { Action } from '@ngrx/store';
import { Tenant } from '../../models/web-api/tenant';

export const LOAD_GET_TENANTS = '[Tenant] LoadGetTenants';
export const SAVE_GET_TENANTS = '[Tenant] SaveGetTenants';
export const ADD_TENANT = '[Tenant] AddTenant';
export const EDIT_TENANT = '[Tenant] EditTenant';
export const DELETE_TENANT = '[Tenant] DeleteTenant';


// Handled by effect
export class LoadGetTenants implements Action {
	readonly type = LOAD_GET_TENANTS;
	constructor() { }
}

export class SaveGetTenants implements Action {
	readonly type = SAVE_GET_TENANTS;
	constructor(public payload: Tenant[]) { }
}

export class AddTenant implements Action {
	readonly type = ADD_TENANT;
	constructor(public payload: Tenant) {}
}

export class EditTenant implements Action {
	readonly type = EDIT_TENANT;
	constructor(public payload: Tenant) {}
}
export class DeleteTenant implements Action {
	readonly type = DELETE_TENANT;
	constructor(public payload: string) {}
}

export type Actions = LoadGetTenants | SaveGetTenants | AddTenant | EditTenant | DeleteTenant;
