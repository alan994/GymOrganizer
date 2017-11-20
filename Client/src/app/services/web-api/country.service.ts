import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Country } from '../../models/web-api/country';

@Injectable()
export class CountryService {
	constructor(private http: HttpClient) { }

	getAllCountries() {
		return this.http.get<Country[]>('/api/countries');
	}

	getAllActiveCountries() {
		return this.http.get<Country[]>('/api/countries/active');
	}

	getCountryById(id: string) {
		return this.http.get<Country>('/api/countries/' + id);
	}

	addCountry(country: Country) {
		return this.http.post('/api/countries', country).subscribe();
	}

	editCountry(country: Country) {
		return this.http.put('/api/countries', country).subscribe();
	}

	deleteCountry(id: string) {
		return this.http.delete('/api/countries/' + id).subscribe();
	}
}
