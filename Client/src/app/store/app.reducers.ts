import { ActionReducerMap } from '@ngrx/store';
import * as fromNotifications from './notifications/notification.reducers';
import * as fromAccount from './account/account.reducers';

export interface AppState {
	notificationState: fromNotifications.State;
	accountState: fromAccount.State;
}

export const reducers: ActionReducerMap<AppState> = {
	notificationState: fromNotifications.notificationReducer,
	accountState: fromAccount.bookReducer
};
