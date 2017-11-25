import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserListComponent } from './user-list/user-list.component';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { NavComponent } from '../components/nav/nav.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { ApplicationRoleNames } from '../models/config/ApplicationRoleNames';
import { RoleGuard } from '../services/guards/role-guard.service';

const routes: Routes = [
	{
		path: 'users',
		canActivate: [AuthGuard, RoleGuard],
		children: [
			{
				path: '',
				component: UserListComponent,
				children: [
					{
						path: 'add',
						component: UserEditComponent,
						data: {
							isEdit: false,
							roles: [ApplicationRoleNames.ADMINISTRATOR]
						}
					},
					{
						path: ':userId/edit',
						component: UserEditComponent,
						data: {
							isEdit: true,
							roles: [ApplicationRoleNames.ADMINISTRATOR]
						}
					}
				]
			},
			{
				path: '',
				component: NavComponent,
				outlet: 'nav'
			}
		],
		data: {
			roles: [ApplicationRoleNames.ADMINISTRATOR]
		}
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class UserRoutingModule {

}
