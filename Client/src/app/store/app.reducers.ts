import { ActionReducerMap } from '@ngrx/store';
import * as fromNotifications from './notifications/notification.reducers';
import * as fromAccount from './account/account.reducers';
import { routerReducer, RouterReducerState } from '@ngrx/router-store';
import * as fromOffice from '../office/store/office.reducers';
import * as fromCity from '../city/store/city.reducers';
import * as fromCountry from '../country/store/country.reducers';
import * as fromTenant from '../tenant/store/tenant.reducers';
import * as fromTerm from '../term/store/term.reducers';
import * as fromUser from '../user/store/user.reducers';

export interface AppState {
	notificationState: fromNotifications.State;
	accountState: fromAccount.State;
	routerReducer: RouterReducerState;
	officeReducer: fromOffice.State;
	cityReducer: fromCity.State;
	countryReducer: fromCountry.State;
	tenantReducer: fromTenant.State;
	termReducer: fromTerm.State;
	userReducer: fromUser.State;
}

export const reducers: ActionReducerMap<AppState> = {
	notificationState: fromNotifications.notificationReducer,
	accountState: fromAccount.bookReducer,
	routerReducer: routerReducer,
	officeReducer: fromOffice.officeReducer,
	cityReducer: fromCity.cityReducer,
	countryReducer: fromCountry.countryReducer,
	tenantReducer: fromTenant.tenantReducer,
	termReducer: fromTerm.TermReducer,
	userReducer: fromUser.UserReducer
};
