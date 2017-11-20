import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { City } from '../../models/web-api/city';

@Injectable()
export class CityService {
	constructor(private http: HttpClient) { }

	getAllCities() {
		return this.http.get<City[]>('/api/cities');
	}

	getAllActiveCities() {
		return this.http.get<City[]>('/api/cities/active');
	}

	getCityById(id: string) {
		return this.http.get<City>('/api/cities/' + id);
	}

	addCity(city: City) {
		return this.http.post('/api/cities', city).subscribe();
	}

	editCity(city: City) {
		return this.http.put('/api/cities', city).subscribe();
	}

	deleteCity(id: string) {
		return this.http.delete('/api/cities/' + id).subscribe();
	}
}
