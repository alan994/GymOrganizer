import { Action } from '@ngrx/store';
import { Location } from '../../models/Location';

export const ADD = 'Location add';
export const EDIT = 'Location edit';
export const DELETE = 'Location delete';

export const GET_ALL_TRIGGER = 'Location trigger get all';
export const GET_ALL = 'Location get all';

// Handled by effect
export class AddLocation implements Action {
    readonly type = ADD;
    constructor(public payload: Location) { }
}

// Handled by effect
export class EditLocation implements Action {
    readonly type = EDIT;
    constructor(public payload: Location) { }
}

// Handled by effect
export class DeleteLocation implements Action {
    readonly type = DELETE;
    constructor(public payload: Location) { }
}
// Handled by effect
export class GetAllLocationsTrigger implements Action {
    readonly type = GET_ALL_TRIGGER;
}

// Handled by reducer
export class GetAllLocations implements Action {
    readonly type = GET_ALL;
    constructor(public payload: Location[]) { }
}

export type Actions = AddLocation | EditLocation | DeleteLocation | GetAllLocations | GetAllLocationsTrigger;