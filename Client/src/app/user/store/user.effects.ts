import { Injectable } from '@angular/core';
import { Router } from '@angular/router';

import { mergeMap } from 'rxjs/operators/mergeMap';
import { switchMap } from 'rxjs/operators/switchMap';

import { Effect, Actions } from '@ngrx/effects';
import * as RouterActions from '../../store/router/router.actions';
import * as UserActions from './User.actions';
import { UserService } from '../../services/web-api/User.service';

import { User } from '../../models/web-api/User';
import { Logger } from '../../services/utils/log.service';

@Injectable()
export class UserEffects {
	@Effect()
	loadUsers = this.actions$
		.ofType(UserActions.LOAD_GET_USERS)
		.pipe(
		switchMap(() => {
			return this.userService.getAllUsers();
		}),
		mergeMap((account: any) => {
			return [
				{
					type: UserActions.SAVE_GET_USERS,
					payload: account
				}
			];
		})
		);

	@Effect({ dispatch: false })
	addUser = this.actions$
		.ofType(UserActions.ADD_USER)
		.map((action: UserActions.AddUser) => {
			return action.payload;
		})
		.map((payload: User) => {
			return this.userService.addUser(payload);
		})
		.map(() => {
			this.logger.info('Adding user in progress');
		});

	@Effect({ dispatch: false })
	editUser = this.actions$
		.ofType(UserActions.EDIT_USER)
		.map((action: UserActions.EditUser) => {
			return action.payload;
		})
		.map((payload: User) => {
			return this.userService.editUser(payload);
		})
		.map(() => {
			this.logger.info('Editing user in progress');
		});


	@Effect({ dispatch: false })
	deleteUser = this.actions$
		.ofType(UserActions.DELETE_USER)
		.map((action: UserActions.DeleteUser) => {
			return action.payload;
		})
		.map((payload: string) => {
			return this.userService.deleteUser(payload);
		})
		.map(() => {
			this.logger.info('Deleting user in progress');
		});

	constructor(private actions$: Actions, private router: Router, private userService: UserService, private logger: Logger) { }
}
