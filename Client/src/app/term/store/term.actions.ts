import { Action } from '@ngrx/store';

import { Term } from '../../models/web-api/Term';

export const LOAD_GET_TERMS = '[Term] LoadGetTerms';
export const SAVE_GET_TERMS = '[Term] SaveGetTerms';
export const ADD_TERM = '[Term] AddTerm';
export const EDIT_TERM = '[Term] EditTerm';
export const DELETE_TERM = '[Term] DeleteTerm';


// Handled by effect
export class LoadGetTerms implements Action {
	readonly type = LOAD_GET_TERMS;
	constructor() { }
}

export class AddTerm implements Action {
	readonly type = ADD_TERM;
	constructor(public payload: Term) { }
}

export class EditTerm implements Action {
	readonly type = EDIT_TERM;
	constructor(public payload: Term) { }
}

export class DeleteTerm implements Action {
	readonly type = DELETE_TERM;
	constructor(public payload: string) { }
}

export class SaveGetTerms implements Action {
	readonly type = SAVE_GET_TERMS;
	constructor(public payload: Term[]) { }
}

export type Actions = LoadGetTerms | SaveGetTerms | AddTerm | EditTerm | DeleteTerm;
