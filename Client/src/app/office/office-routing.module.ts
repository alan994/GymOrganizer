import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OfficeListComponent } from './office-list/office-list.component';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { NavComponent } from '../components/nav/nav.component';
import { OfficeEditComponent } from './office-edit/office-edit.component';
import { ApplicationRoleNames } from '../models/config/ApplicationRoleNames';
import { RoleGuard } from '../services/guards/role-guard.service';

const routes: Routes = [
	{
		path: 'offices',
		canActivate: [AuthGuard, RoleGuard],
		children: [
			{
				path: '',
				component: OfficeListComponent,
				children: [
					{
						path: 'add',
						component: OfficeEditComponent,
						data: {
							isEdit: false,
							roles: [ApplicationRoleNames.ADMINISTRATOR]
						}
					},
					{
						path: ':officeId/edit',
						component: OfficeEditComponent,
						data: {
							roles: [ApplicationRoleNames.ADMINISTRATOR],
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
export class OfficeRoutingModule {

}
