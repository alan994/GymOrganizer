import { Action } from '@ngrx/store';

export const NOTIFICATION_ADD = '[Notification] Add';
export const NOTIFICATION_REMOVE = '[Notification] Remove';

export class AddNotification implements Action {
	readonly type = NOTIFICATION_ADD;
	constructor(public payload: string) {}
}

export class RemoveNotification implements Action {
	readonly type = NOTIFICATION_REMOVE;
	constructor(public payload: string) {}
}

export type NotificationActions = AddNotification | RemoveNotification;
