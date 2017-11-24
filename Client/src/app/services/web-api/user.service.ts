import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { User } from '../../models/web-api/user';

@Injectable()
export class UserService {
	constructor(private http: HttpClient) { }

	getAllUsers() {
		return this.http.get<User[]>('/api/users');
	}

	getAllActiveUsers() {
		return this.http.get<User[]>('/api/users/active');
	}

	getUserById(id: string) {
		return this.http.get<User>('/api/users/' + id);
	}

	addUser(user: User) {
		return this.http.post('/api/users', user).subscribe();
	}

	editUser(user: User) {
		return this.http.put('/api/users', user).subscribe();
	}

	deleteUser(id: string) {
		return this.http.delete('/api/users/' + id).subscribe();
	}
}
