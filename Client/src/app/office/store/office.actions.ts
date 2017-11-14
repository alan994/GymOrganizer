import { Action } from '@ngrx/store';

import { Account } from '../../models/web-api/account';
import { Office } from '../../models/web-api/office';

export const LOAD_GET_OFFICES = '[Account] LoadGetOffices';
export const SAVE_GET_OFFICES = '[Account] SaveGetOffices';

// Handled by effect
export class LoadGetOffices implements Action {
  readonly type = LOAD_GET_OFFICES;
  constructor() { }
}

export class SaveGetOffices implements Action {
  readonly type = SAVE_GET_OFFICES;
  constructor(public payload: Office[]) { }
}

export type Actions = LoadGetOffices | SaveGetOffices;
