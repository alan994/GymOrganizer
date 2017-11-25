import { Component, OnInit } from '@angular/core';
import { FormGroup, FormBuilder, Validators } from '@angular/forms';
import { Store } from '@ngrx/store';
import * as fromApp from '../../store/app.reducers';
import * as UserActions from '../store/user.actions';
import { ActivatedRoute } from '@angular/router';
import { Logger } from '../../services/utils/log.service';
import { User } from '../../models/web-api/user';
import * as _ from 'lodash';
import { Observable } from 'rxjs/Observable';
import { Account } from '../../models/web-api/account';

@Component({
	selector: 'go-user-edit',
	templateUrl: './user-edit.component.html',
	styleUrls: ['./user-edit.component.scss']
})
export class UserEditComponent implements OnInit {
	userId: string;
	userForm: FormGroup;
	account: Observable<Account>;
	isEdit: boolean;

	constructor(private store: Store<fromApp.AppState>,
		private formBuilder: FormBuilder,
		private activatedRoute: ActivatedRoute,
		private logger: Logger
	) {
		this.userForm = this.formBuilder.group(
			{
				id: [null],
				firstName: [null, Validators.required],
				lastName: [null, Validators.required],
				email: [null, [Validators.required, Validators.email]],
				owed: [null, [Validators.required, Validators.min(0)]],
				claimed: [null, [Validators.required, Validators.min(0)]],
				tempPassword: [null],
				isAdmin: [false],
				isCoach: [false],
				isGlobalAdmin: [false],
				active: true
			}
		);
		activatedRoute.data.subscribe(data => { this.isEdit = data.isEdit; this.update(); });
		activatedRoute.params.subscribe(params => { this.userId = params.userId; this.update(); });
	}

	ngOnInit() {
		this.account = this.store.select(s => s.accountState.account);
	}

	update() {
		if (this.userId) {
			this.store.select(x => x.userReducer.users)
				.take(1)
				.map((users: User[]) => {
					return _.find(users, o => o.id === this.userId);
				})
				.subscribe((user: User) => {
					this.userForm.patchValue({
						id: this.userId,
						firstName: user.firstName,
						lastName: user.lastName,
						email: user.email,
						owed: user.owed,
						claimed: user.claimed,
						active: user.active,
						isAdmin: user.isAdmin,
						isCoach: user.isCoach,
						isGlobalAdmin: user.isGlobalAdmin
					});
				});
		}
	}

	add() {
		this.store.dispatch(new UserActions.AddUser(this.userForm.value));
		this.logger.info('Add: ', this.userForm.value, this.userForm);
	}

	edit() {
		this.store.dispatch(new UserActions.EditUser(this.userForm.value));
		this.logger.info('Edit: ', this.userForm.value, this.userForm);
	}
}
