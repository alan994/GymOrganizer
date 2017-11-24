import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Country } from '../../models/web-api/country';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import { Logger } from '../../services/utils/log.service';
import * as TenantActions from '../store/tenant.actions';
import { Tenant } from '../../models/web-api/tenant';

@Component({
	selector: 'go-tenant-list',
	templateUrl: './tenant-list.component.html',
	styleUrls: ['./tenant-list.component.scss']
})
export class TenantListComponent implements OnInit {

	tenants: Observable<Tenant[]>;

	constructor(private store: Store<fromApp.AppState>,
		private logger: Logger) { }

	ngOnInit() {
		this.store.dispatch(new TenantActions.LoadGetTenants());
		this.tenants = this.store.select(s => s.tenantReducer.tenants);
	}
}
