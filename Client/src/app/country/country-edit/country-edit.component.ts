import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Country } from '../../models/web-api/country';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import * as CountryActions from '../store/country.actions';
import { ActivatedRoute } from '@angular/router';
import { Logger } from '../../services/utils/log.service';
import { count } from 'rxjs/operators/count';
import * as _ from 'lodash';

@Component({
	selector: 'go-country-edit',
	templateUrl: './country-edit.component.html',
	styleUrls: ['./country-edit.component.scss']
})
export class CountryEditComponent implements OnInit {

	countryId: string;
	countries: Observable<Country[]>;
	countryForm: FormGroup;
	isEdit: boolean;

	constructor(private store: Store<fromApp.AppState>,
		private formBuilder: FormBuilder,
		private activatedRoute: ActivatedRoute,
		private logger: Logger) {

		this.countryForm = this.formBuilder.group({
			id: [null],
			name: [null, Validators.required],
			iso2Code: [null, [Validators.required, Validators.maxLength(2)]],
			iso3Code: [null, [Validators.required, Validators.maxLength(3)]],
			numericCode: [null, Validators.required],
			active: true
		});

		activatedRoute.data.subscribe(data => { this.isEdit = data.isEdit; this.update(); });
		activatedRoute.params.subscribe(params => { this.countryId = params.cityId; this.update(); });
	}

	update() {
		if (this.countryId) {
			this.store.select(x => x.countryReducer.countries)
				.take(1)
				.map((countries: Country[]) => {
					return _.find(countries, o => o.id === this.countryId);
				})
				.subscribe((country: Country) => {
					//Handle form
					this.countryForm.patchValue({
						id: this.countryId,
						name: country.name,
						iso2Code: country.iso2Code,
						iso3Code: country.iso3Code,
						numericCode: country.numericCode,
						active: country.active
					});
				});
		}
	}

	ngOnInit() {
	}

	add() {
		this.store.dispatch(new CountryActions.AddCountry(this.countryForm.value));
		this.logger.info('Add: ', this.countryForm.value, this.countryForm);
	}

	edit() {
		this.store.dispatch(new CountryActions.EditCountry(this.countryForm.value));
		this.logger.info('Edit: ', this.countryForm.value, this.countryForm);
	}
}
