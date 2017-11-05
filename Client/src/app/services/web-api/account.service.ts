import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import {Account} from '../../models/web-api/account';

@Injectable()
export class AccountService {
	constructor(private http: HttpClient) {}

	getUserProfile() {
		return this.http.get<Account>('/api/account');
	}
}
