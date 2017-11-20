import { QueueResult } from '../../models/web-api/queue-result';
import { ProcessType } from '../../models/enums/process-type';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import { Injectable } from '@angular/core';

import * as CityActions from '../../city/store/city.actions';
import * as OfficeActions from '../../office/store/office.actions';

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
			break;
			case ProcessType.EditCountry:
			break;
			case ProcessType.DeleteCountry:
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
			break;
			case ProcessType.EditTerm:
			break;
			case ProcessType.DeleteTerm:
			break;
			//#endregion
			//#region Tenant
			case ProcessType.AddTenant:
			break;
			case ProcessType.EditTenant:
			break;
			//#endregion
		}
	}
}