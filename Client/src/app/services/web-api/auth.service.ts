import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';

@Injectable()
export class AuthService {
	constructor(private http: HttpClient) {}

	// isUserRealEducationOwner(educationId: string) {
	// 	return this.http.get<boolean>('/api/auth/isEducationOwner/' + educationId);
	// }
}
