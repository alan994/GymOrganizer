import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';

import { RouterModule } from '@angular/router';
import { NavComponent } from '../components/nav/nav.component';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { TenantListComponent } from './tenant-list/tenant-list.component';
import { TenantEditComponent } from './tenant-edit/tenant-edit.component';
import { ApplicationRoleNames } from '../models/config/ApplicationRoleNames';
import { RoleGuard } from '../services/guards/role-guard.service';


const routes: Routes = [
	{
		path: 'tenants',
		canActivate: [AuthGuard, RoleGuard],
		children: [
			{
				path: '',
				component: TenantListComponent,
				children: [
					{
						path: 'add',
						component: TenantEditComponent,
						data: {
							isEdit: false,
							roles: [ApplicationRoleNames.GLOBAL_ADMIN]
						}
					},
					{
						path: ':tenantId/edit',
						component: TenantEditComponent,
						data: {
							isEdit: true,
							roles: [ApplicationRoleNames.GLOBAL_ADMIN]
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
			roles: [ApplicationRoleNames.GLOBAL_ADMIN]
		}
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class TenantRoutingModule { }
