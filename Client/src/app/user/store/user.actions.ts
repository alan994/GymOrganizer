import { Action } from '@ngrx/store';

import { Account } from '../../models/web-api/account';
import { User } from '../../models/web-api/user';

export const LOAD_GET_USERS = '[User] LoadGetUsers';
export const SAVE_GET_USERS = '[User] SaveGetUsers';
export const ADD_USER = '[User] AddUser';
export const EDIT_USER = '[User] EditUser';
export const DELETE_USER = '[User] DeleteUser';


// Handled by effect
export class LoadGetUsers implements Action {
	readonly type = LOAD_GET_USERS;
	constructor() { }
}

export class AddUser implements Action {
	readonly type = ADD_USER;
	constructor(public payload: User) { }
}

export class EditUser implements Action {
	readonly type = EDIT_USER;
	constructor(public payload: User) { }
}

export class DeleteUser implements Action {
	readonly type = DELETE_USER;
	constructor(public payload: string) { }
}

export class SaveGetUsers implements Action {
	readonly type = SAVE_GET_USERS;
	constructor(public payload: User[]) { }
}

export type Actions = LoadGetUsers | SaveGetUsers | AddUser | EditUser | DeleteUser;
