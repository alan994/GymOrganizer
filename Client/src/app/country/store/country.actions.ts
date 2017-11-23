import { Action } from '@ngrx/store';


import { Country } from '../../models/web-api/country';

export const LOAD_GET_COUNTRIES = '[Country] LoadGetCountries';
export const SAVE_GET_COUNTRIES = '[Country] SaveGetCountries';
export const ADD_COUNTRY = '[Country] AddCountry';
export const EDIT_COUNTRY = '[Country] EditCountry';
export const DELETE_COUNTRY = '[Country] DeleteCountry';


// Handled by effect
export class LoadGetCountries implements Action {
	readonly type = LOAD_GET_COUNTRIES;
	constructor() { }
}

export class SaveGetCountries implements Action {
	readonly type = SAVE_GET_COUNTRIES;
	constructor(public payload: Country[]) { }
}

export class AddCountry implements Action {
	readonly type = ADD_COUNTRY;
	constructor(public payload: Country) {}
}

export class EditCountry implements Action {
	readonly type = EDIT_COUNTRY;
	constructor(public payload: Country) {}
}
export class DeleteCountry implements Action {
	readonly type = DELETE_COUNTRY;
	constructor(public payload: string) {}
}

export type Actions = LoadGetCountries | SaveGetCountries | AddCountry | EditCountry | DeleteCountry;
