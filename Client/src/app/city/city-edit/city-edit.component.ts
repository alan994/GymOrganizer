import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Country } from '../../models/web-api/country';
import { FormGroup } from '@angular/forms';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import * as CityActions from '../store/city.actions';
import { FormBuilder } from '@angular/forms';
import { ActivatedRoute } from '@angular/router';
import { Logger } from '../../services/utils/log.service';
import { Validators } from '@angular/forms';
import { City } from '../../models/web-api/city';
import * as CountryActions from '../../country/store/country.actions';
import * as _ from 'lodash';

@Component({
	selector: 'go-city-edit',
	templateUrl: './city-edit.component.html',
	styleUrls: ['./city-edit.component.scss']
})
export class CityEditComponent implements OnInit {

	cityId: string;
	countries: Observable<Country[]>;
	cityForm: FormGroup;
	isEdit: boolean;

	constructor(private store: Store<fromApp.AppState>,
		private formBuilder: FormBuilder,
		private activatedRoute: ActivatedRoute,
		private logger: Logger) {

		this.store.dispatch(new CountryActions.LoadGetCountries());
		this.cityForm = this.formBuilder.group({
			id: [{ value: '', disabled: true }, Validators.required],
			name: ['', Validators.required],
			postalCode: ['', Validators.required],
			country: ['', Validators.required],
			active: true
		});

		activatedRoute.data.subscribe(data => { this.isEdit = data.isEdit; this.update(); });
		activatedRoute.params.subscribe(params => { this.cityId = params.cityId; this.update(); });
	}

	update() {
		if (this.cityId) {
			this.store.select(x => x.cityReducer.cities)
				.take(1)
				.map((cities: City[]) => {
					return _.find(cities, o => o.id === this.cityId);
				})
				.subscribe((city: City) => {
					//Handle form
					this.cityForm.patchValue({
						id: this.cityId,
						name: city.name,
						postalCode: city.postalCode,
						country: city.country,
						active: city.active
					});
				});
		}
	}

	ngOnInit() {
		this.store.select(s => s.countryReducer.countries);
	}

}
