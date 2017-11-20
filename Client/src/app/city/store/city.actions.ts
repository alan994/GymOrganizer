import { Action } from '@ngrx/store';


import { City } from '../../models/web-api/city';

export const LOAD_GET_CITIES = '[Account] LoadGetCities';
export const SAVE_GET_CITIES = '[Account] SaveGetCities';

// Handled by effect
export class LoadGetCities implements Action {
  readonly type = LOAD_GET_CITIES;
  constructor() { }
}

export class SaveGetCities implements Action {
  readonly type = SAVE_GET_CITIES;
  constructor(public payload: City[]) { }
}

export type Actions = LoadGetCities | SaveGetCities;
