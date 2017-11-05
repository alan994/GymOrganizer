import { Action } from '@ngrx/store';

import {Account} from '../../models/web-api/account';

export const LOAD_GET_USER_PROFILE = '[Account] LoadGetUserProfile';
export const SAVE_GET_USER_PROFILE = '[Account] SaveGetUserProfile';

// Handled by effect
export class LoadGetUserProfile implements Action {
	readonly type = LOAD_GET_USER_PROFILE;
	constructor() { }
}

export class SaveGetUserProfile implements Action {
	readonly type = SAVE_GET_USER_PROFILE;
	constructor(public payload: Account) { }
}

export type Actions = LoadGetUserProfile | SaveGetUserProfile;
