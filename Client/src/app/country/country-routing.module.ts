import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavComponent } from '../components/nav/nav.component';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { CountryListComponent } from './country-list/country-list.component';
import { CountryEditComponent } from './country-edit/country-edit.component';
import { ApplicationRoleNames } from '../models/config/ApplicationRoleNames';
import { RoleGuard } from '../services/guards/role-guard.service';


const routes: Routes = [
	{
		path: 'countries',
		canActivate: [AuthGuard, RoleGuard],
		children: [
			{
				path: '',
				component: CountryListComponent,
				children: [
					{
						path: 'add',
						component: CountryEditComponent,
						data: {
							isEdit: false,
							roles: [ApplicationRoleNames.ADMINISTRATOR]
						}
					},
					{
						path: ':cityId/edit',
						component: CountryEditComponent,
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
export class CountryRoutingModule { }
