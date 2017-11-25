import { QueueResult } from '../../models/web-api/queue-result';
import { ProcessType } from '../../models/enums/process-type';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import { Injectable } from '@angular/core';

import * as CityActions from '../../city/store/city.actions';
import * as OfficeActions from '../../office/store/office.actions';
import * as CountryActions from '../../country/store/country.actions';
import * as TenantActions from '../../tenant/store/tenant.actions';
import * as TermActions from '../../term/store/term.actions';
import * as UserActions from '../../user/store/user.actions';
import * as AccountActions from '../../store/account/account.actions';

@Injectable()
export class NotificationHelperService {
	constructor(private store: Store<fromApp.AppState>) { }

	handleQueueResponse(response: QueueResult) {
		switch (response.processType) {
			//#region City
			case ProcessType.AddCity:
				this.store.dispatch(new CityActions.LoadGetCities());
				break;
			case ProcessType.EditCity:
				this.store.dispatch(new CityActions.LoadGetCities());
				break;
			case ProcessType.DeleteCity:
				this.store.dispatch(new CityActions.LoadGetCities());
				break;
			//#endregion
			//#region Country
			case ProcessType.AddCountry:
				this.store.dispatch(new CountryActions.LoadGetCountries());
				break;
			case ProcessType.EditCountry:
				this.store.dispatch(new CountryActions.LoadGetCountries());
				break;
			case ProcessType.DeleteCountry:
				this.store.dispatch(new CountryActions.LoadGetCountries());
				break;
			//#endregion
			//#region Office
			case ProcessType.AddOffice:
				this.store.dispatch(new OfficeActions.LoadGetOffices());
				break;
			case ProcessType.EditOffice:
				this.store.dispatch(new OfficeActions.LoadGetOffices());
				break;
			case ProcessType.DeleteOffice:
				this.store.dispatch(new OfficeActions.LoadGetOffices());
				break;
			//#endregion
			//#region Term
			case ProcessType.AddTerm:
				this.store.dispatch(new TermActions.LoadGetTerms());
				break;
			case ProcessType.EditTerm:
				this.store.dispatch(new TermActions.LoadGetTerms());
				break;
			case ProcessType.DeleteTerm:
				this.store.dispatch(new TermActions.LoadGetTerms());
				break;
			//#endregion
			//#region Tenant
			case ProcessType.AddTenant:
				this.store.dispatch(new TenantActions.LoadGetTenants());
				break;
			case ProcessType.EditTenant:
				this.store.dispatch(new TenantActions.LoadGetTenants());
				break;
			//#endregion
			//#region Users
			case ProcessType.AddUser:
				this.store.dispatch(new UserActions.LoadGetUsers());
				this.store.dispatch(new AccountActions.LoadGetUserProfile());
				break;
			case ProcessType.EditUser:
				this.store.dispatch(new UserActions.LoadGetUsers());
				this.store.dispatch(new AccountActions.LoadGetUserProfile());
				break;
			case ProcessType.DeleteUser:
				this.store.dispatch(new UserActions.LoadGetUsers());
				this.store.dispatch(new AccountActions.LoadGetUserProfile());
				break;
			//#endregion
		}
	}
}