import { Component, OnInit } from '@angular/core';
import { User } from '../../models/web-api/user';
import * as fromApp from '../../store/app.reducers';
import { Observable } from 'rxjs/Observable';
import { Store } from '@ngrx/store';
import { Logger } from '../../services/utils/log.service';
import * as UserActions from '../store/user.actions';

@Component({
	selector: 'go-user-list',
	templateUrl: './user-list.component.html',
	styleUrls: ['./user-list.component.scss']
})
export class UserListComponent implements OnInit {
	users: Observable<User[]>;

	constructor(private store: Store<fromApp.AppState>,
		private logger: Logger) { }

	ngOnInit() {
		this.store.dispatch(new UserActions.LoadGetUsers());
		this.users = this.store.select(s => s.userReducer.users);
	}

	deleteOffice(user: User) {
		this.store.dispatch(new UserActions.DeleteUser(user.id));
	}
}
