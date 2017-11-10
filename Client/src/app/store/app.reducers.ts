import { ActionReducerMap } from '@ngrx/store';
import * as fromNotifications from './notifications/notification.reducers';
import * as fromAccount from './account/account.reducers';
import { routerReducer, RouterReducerState } from '@ngrx/router-store';

export interface AppState {
	notificationState: fromNotifications.State;
	accountState: fromAccount.State;
	routerReducer: RouterReducerState;
}

export const reducers: ActionReducerMap<AppState> = {
	notificationState: fromNotifications.notificationReducer,
	accountState: fromAccount.bookReducer,
	routerReducer: routerReducer
};
