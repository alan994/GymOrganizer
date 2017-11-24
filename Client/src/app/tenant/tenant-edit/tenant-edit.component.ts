import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Tenant } from '../../models/web-api/tenant';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import { ActivatedRoute } from '@angular/router';
import { Logger } from '../../services/utils/log.service';
import * as _ from 'lodash';
import * as TenantActions from '../store/tenant.actions';

@Component({
	selector: 'go-tenant-edit',
	templateUrl: './tenant-edit.component.html',
	styleUrls: ['./tenant-edit.component.scss']
})
export class TenantEditComponent implements OnInit {

	tenantId: string;
	tenants: Observable<Tenant[]>;
	tenantForm: FormGroup;
	isEdit: boolean;

	constructor(private store: Store<fromApp.AppState>,
		private formBuilder: FormBuilder,
		private activatedRoute: ActivatedRoute,
		private logger: Logger) {

		this.tenantForm = this.formBuilder.group({
			id: [null],
			name: [null, Validators.required],
			active: true
		});

		activatedRoute.data.subscribe(data => { this.isEdit = data.isEdit; this.update(); });
		activatedRoute.params.subscribe(params => { this.tenantId = params.tenantId; this.update(); });
	}

	update() {
		if (this.tenantId) {
			this.store.select(x => x.tenantReducer.tenants)
				.take(1)
				.map((tenants: Tenant[]) => {
					return _.find(tenants, o => o.id === this.tenantId);
				})
				.subscribe((tenant: Tenant) => {
					//Handle form
					this.tenantForm.patchValue({
						id: this.tenantId,
						name: tenant.name,
						active: tenant.active
					});
				});
		}
	}

	ngOnInit() {
	}

	add() {
		this.store.dispatch(new TenantActions.AddTenant(this.tenantForm.value));
		this.logger.info('Add: ', this.tenantForm.value, this.tenantForm);
	}

	edit() {
		this.store.dispatch(new TenantActions.EditTenant(this.tenantForm.value));
		this.logger.info('Edit: ', this.tenantForm.value, this.tenantForm);
	}

}
