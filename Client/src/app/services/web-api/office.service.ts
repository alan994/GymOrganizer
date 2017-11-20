import { HttpClient } from '@angular/common/http';
import { Office } from '../../models/web-api/office';
import { Injectable } from '@angular/core';

@Injectable()
export class OfficeService {
	constructor(private http: HttpClient) { }

	getAllOffices() {
		return this.http.get<Office[]>('/api/offices');
	}

	getAllActiveOffices() {
		return this.http.get<Office[]>('/api/offices/active');
	}

	getOfficeById(id: string) {
		return this.http.get<Office>('/api/offices/' + id);
	}

	addOffice(office: Office) {
		return this.http.post('/api/offices', office).subscribe();
	}

	editOffice(office: Office) {
		return this.http.put('/api/offices', office).subscribe();
	}

	deleteOffice(id: string) {
		return this.http.delete('/api/offices/' + id).subscribe();
	}
}
