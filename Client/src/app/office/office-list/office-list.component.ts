import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import { Logger } from '../../services/utils/log.service';
import { Observable } from 'rxjs/Observable';
import { Office } from '../../models/web-api/office';
import * as OfficeActions from '../store/office.actions';

@Component({
	selector: 'go-office-list',
	templateUrl: './office-list.component.html',
	styleUrls: ['./office-list.component.scss']
})
export class OfficeListComponent implements OnInit {

	offices: Observable<Office[]>;

	constructor(private store: Store<fromApp.AppState>,
		private logger: Logger) { }

	ngOnInit() {
		this.store.dispatch(new OfficeActions.LoadGetOffices());
		this.offices = this.store.select(s => s.officeReducer.offices);
	}

	deleteOffice(office: Office) {
		this.store.dispatch(new OfficeActions.DeleteOffice(office.id));
	}

}
