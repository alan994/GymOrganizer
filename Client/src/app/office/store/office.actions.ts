import { Action } from '@ngrx/store';

import { Account } from '../../models/web-api/account';
import { Office } from '../../models/web-api/office';
import { DELEGATE_CTOR } from '@angular/core/src/reflection/reflection_capabilities';

export const LOAD_GET_OFFICES = '[Office] LoadGetOffices';
export const SAVE_GET_OFFICES = '[Office] SaveGetOffices';
export const ADD_OFFICE = '[Office] AddOffice';
export const EDIT_OFFICE = '[Office] EditOffice';
export const DELETE_OFFICE = '[Office] DeleteOffice';


// Handled by effect
export class LoadGetOffices implements Action {
	readonly type = LOAD_GET_OFFICES;
	constructor() { }
}

export class AddOffice implements Action {
	readonly type = ADD_OFFICE;
	constructor(public payload: Office) { }
}

export class EditOffice implements Action {
	readonly type = EDIT_OFFICE;
	constructor(public payload: Office) { }
}

export class DeleteOffice implements Action {
	readonly type = DELETE_OFFICE;
	constructor(public payload: string) { }
}

export class SaveGetOffices implements Action {
	readonly type = SAVE_GET_OFFICES;
	constructor(public payload: Office[]) { }
}

export type Actions = LoadGetOffices | SaveGetOffices | AddOffice | EditOffice | DeleteOffice;
