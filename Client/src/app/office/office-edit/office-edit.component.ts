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

@Component({
	selector: 'go-office-edit',
	templateUrl: './office-edit.component.html',
	styleUrls: ['./office-edit.component.scss']
})
export class OfficeEditComponent implements OnInit {
	cities: Observable<City[]>;
	officeForm: FormGroup;
	isEdit: boolean;

	constructor(private store: Store<fromApp.AppState>,
		private formBuilder: FormBuilder,
		private activatedRoute: ActivatedRoute,
		private logger: Logger
	) {
		this.isEdit = activatedRoute.snapshot.data.isEdit;
		this.officeForm = this.formBuilder.group(
			{
				name: ['', Validators.required],
				address: ['', Validators.required],
				city: ['', Validators.required],
				active: true
			}
		);
	}

	ngOnInit() {
		this.store.dispatch(new CityActions.LoadGetCities());
		this.cities = this.store.select(s => s.cityReducer.cities);

		this.officeForm.patchValue({
			name: 'Zaprešić',
			address: 'neka adresa'
		});

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
