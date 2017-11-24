import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { UserListComponent } from './User-list/User-list.component';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { NavComponent } from '../components/nav/nav.component';
import { UserEditComponent } from './User-edit/User-edit.component';

const routes: Routes = [
	{
		path: 'users',
		canActivate: [AuthGuard],
		children: [
			{
				path: '',
				component: UserListComponent,
				children: [
					{
						path: 'add',
						component: UserEditComponent,
						data: {
							isEdit: false
						}
					},
					{
						path: ':userId/edit',
						component: UserEditComponent,
						data: {
							isEdit: true
						}
					}
				]
			},
			{
				path: '',
				component: NavComponent,
				outlet: 'nav'
			}
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class UserRoutingModule {

}
