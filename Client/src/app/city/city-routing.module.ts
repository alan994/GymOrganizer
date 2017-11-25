import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';
import { CityListComponent } from './city-list/city-list.component';
import { RouterModule } from '@angular/router';
import { NavComponent } from '../components/nav/nav.component';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { CityEditComponent } from './city-edit/city-edit.component';
import { ApplicationRoleNames } from '../models/config/ApplicationRoleNames';
import { RoleGuard } from '../services/guards/role-guard.service';

const routes: Routes = [
	{
		path: 'cities',
		canActivate: [AuthGuard, RoleGuard],
		children: [
			{
				path: '',
				component: CityListComponent,
				children: [
					{
						path: 'add',
						component: CityEditComponent,
						data: {
							isEdit: false,
							roles: [ApplicationRoleNames.ADMINISTRATOR]
						}
					},
					{
						path: ':cityId/edit',
						component: CityEditComponent,
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
			},
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
export class CityRoutingModule { }
