import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { City } from '../../models/web-api/city';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import * as CityActions from '../store/city.actions';
import { Logger } from '../../services/utils/log.service';

@Component({
	selector: 'go-city-list',
	templateUrl: './city-list.component.html',
	styleUrls: ['./city-list.component.scss']
})
export class CityListComponent implements OnInit {
	cities: Observable<City[]>;

	constructor(private store: Store<fromApp.AppState>,
	private logger: Logger) { }

	ngOnInit() {
		this.store.dispatch(new CityActions.LoadGetCities());
		this.cities = this.store.select(s => s.cityReducer.cities);
	}

	deleteCity(city: City){
		this.store.dispatch(new CityActions.DeleteCity(city.id));
	}

}
