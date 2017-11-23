import { ActionReducerMap } from '@ngrx/store';
import * as fromNotifications from './notifications/notification.reducers';
import * as fromAccount from './account/account.reducers';
import { routerReducer, RouterReducerState } from '@ngrx/router-store';
import * as fromOffice from '../office/store/office.reducers';
import * as fromCity from '../city/store/city.reducers';
import * as fromCountry from '../country/store/country.reducers';

export interface AppState {
	notificationState: fromNotifications.State;
	accountState: fromAccount.State;
	routerReducer: RouterReducerState;
	officeReducer: fromOffice.State;
	cityReducer: fromCity.State;
	countryReducer: fromCountry.State;
}

export const reducers: ActionReducerMap<AppState> = {
	notificationState: fromNotifications.notificationReducer,
	accountState: fromAccount.bookReducer,
	routerReducer: routerReducer,
	officeReducer: fromOffice.officeReducer,
	cityReducer: fromCity.officeReducer,
	countryReducer: fromCountry.officeReducer
};
