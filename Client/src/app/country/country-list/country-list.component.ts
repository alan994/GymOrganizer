import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { Country } from '../../models/web-api/country';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import * as CountryActions from '../store/country.actions';
import { Logger } from '../../services/utils/log.service';

@Component({
	selector: 'go-country-list',
	templateUrl: './country-list.component.html',
	styleUrls: ['./country-list.component.scss']
})
export class CountryListComponent implements OnInit {

	countries: Observable<Country[]>;

	constructor(private store: Store<fromApp.AppState>,
		private logger: Logger) { }

	ngOnInit() {
		this.store.dispatch(new CountryActions.LoadGetCountries());
		this.countries = this.store.select(s => s.countryReducer.countries);
	}

	deleteCity(country: Country) {
		this.store.dispatch(new CountryActions.DeleteCountry(country.id));
	}

}
