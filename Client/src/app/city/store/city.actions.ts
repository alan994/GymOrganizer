import { Action } from '@ngrx/store';


import { City } from '../../models/web-api/city';
import { DELEGATE_CTOR } from '@angular/core/src/reflection/reflection_capabilities';

export const LOAD_GET_CITIES = '[City] LoadGetCities';
export const SAVE_GET_CITIES = '[City] SaveGetCities';
export const ADD_CITY = '[City] AddCity';
export const EDIT_CITY = '[City] EditCity';
export const DELETE_CITY = '[City] DeleteCity';


// Handled by effect
export class LoadGetCities implements Action {
	readonly type = LOAD_GET_CITIES;
	constructor() { }
}

export class SaveGetCities implements Action {
	readonly type = SAVE_GET_CITIES;
	constructor(public payload: City[]) { }
}

export class AddCity implements Action {
	readonly type = ADD_CITY;
	constructor(public payload: City) {}
}

export class EditCity implements Action {
	readonly type = EDIT_CITY;
	constructor(public payload: City) {}
}
export class DeleteCity implements Action {
	readonly type = DELETE_CITY;
	constructor(public payload: string) {}
}

export type Actions = LoadGetCities | SaveGetCities | AddCity | EditCity | DeleteCity;
