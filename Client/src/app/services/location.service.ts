import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Location } from '../models/Location';

@Injectable()
export class LocationService {
    constructor(private httpClient: HttpClient) { }

    getLocations() {
        return this.httpClient.get<Location[]>('api/locations');
    }

    addLocation(location: Location) {
        return this.httpClient.post('api/locations', location);
    }

    editLocation(location: Location) {
        return this.httpClient.put('api/locations', location);
    }

    deleteLocation(id: string) {
        return this.httpClient.delete('api/locations/' + id);
    }
}