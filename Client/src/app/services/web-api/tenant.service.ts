import { HttpClient } from '@angular/common/http';
import { Tenant } from '../../models/web-api/tenant';
import { Injectable } from '@angular/core';

@Injectable()
export class TenantService {
	constructor(private http: HttpClient) { }

	getAllTenants() {
		return this.http.get<Tenant[]>('/api/tenants');
	}

	getAllActiveTenants() {
		return this.http.get<Tenant[]>('/api/tenants/active');
	}

	getTenantById(id: string) {
		return this.http.get<Tenant>('/api/tenants/' + id);
	}

	addTenant(tenant: Tenant) {
		return this.http.post('/api/tenants', tenant).subscribe();
	}

	editTenant(tenant: Tenant) {
		return this.http.put('/api/tenants', tenant).subscribe();
	}

	deleteTenant(id: string) {
		return this.http.delete('/api/tenants/' + id).subscribe();
	}
}
