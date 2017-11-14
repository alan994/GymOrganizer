import { ActionReducerMap } from '@ngrx/store';
import * as fromNotifications from './notifications/notification.reducers';
import * as fromAccount from './account/account.reducers';
import { routerReducer, RouterReducerState } from '@ngrx/router-store';
import * as fromOffice from '../office/store/office.reducers';

export interface AppState {
  notificationState: fromNotifications.State;
  accountState: fromAccount.State;
  routerReducer: RouterReducerState;
  officeReducer: fromOffice.State;
}

export const reducers: ActionReducerMap<AppState> = {
  notificationState: fromNotifications.notificationReducer,
  accountState: fromAccount.bookReducer,
  routerReducer: routerReducer,
  officeReducer: fromOffice.officeReducer
};
