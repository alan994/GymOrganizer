import { Component, OnInit } from '@angular/core';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';

import { City } from '../../models/web-api/city';
import * as CityActions from '../../city/store/city.actions';
import { FormGroup } from '@angular/forms';
import { FormBuilder, Validators } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Logger } from '../../services/utils/log.service';
import { Observable } from 'rxjs/Observable';
import * as OfficeActions from '../store/office.actions';
import { Office } from '../../models/web-api/office';
import * as _ from 'lodash';

@Component({
	selector: 'go-office-edit',
	templateUrl: './office-edit.component.html',
	styleUrls: ['./office-edit.component.scss']
})
export class OfficeEditComponent implements OnInit {
	officeId: string;
	cities: Observable<City[]>;
	officeForm: FormGroup;
	isEdit: boolean;

	constructor(private store: Store<fromApp.AppState>,
		private formBuilder: FormBuilder,
		private activatedRoute: ActivatedRoute,
		private logger: Logger
	) {
		this.store.dispatch(new CityActions.LoadGetCities());

		this.officeForm = this.formBuilder.group(
			{
				id: [{ value: '', disabled: true }, Validators.required],
				name: ['', Validators.required],
				address: ['', Validators.required],
				city: ['', Validators.required],
				active: true
			}
		);
		activatedRoute.data.subscribe(data => { this.isEdit = data.isEdit; this.update(); });
		activatedRoute.params.subscribe(params => { this.officeId = params.officeId; this.update(); });
	}

	ngOnInit() {
		this.cities = this.store.select(s => s.cityReducer.cities);
	}

	update() {
		if (this.officeId) {
			this.store.select(x => x.officeReducer.offices)
				.take(1)
				.map((offices: Office[]) => {
					return _.find(offices, o => o.id === this.officeId);
				})
				.subscribe((office: Office) => {
					//Handle cities
					this.officeForm.patchValue({
						id: this.officeId,
						name: office.name,
						address: office.address,
						city: office.city,
						active: office.active
					});
				});
		}
	}

	add() {
		this.store.dispatch(new OfficeActions.AddOffice(this.officeForm.value));
		this.logger.info('Add: ', this.officeForm.value, this.officeForm);
	}

	edit() {
		this.store.dispatch(new OfficeActions.EditOffice(this.officeForm.value));
		this.logger.info('Edit: ', this.officeForm.value, this.officeForm);
	}
}
