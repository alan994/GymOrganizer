import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';

import { RouterModule } from '@angular/router';
import { NavComponent } from '../components/nav/nav.component';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { TenantListComponent } from './tenant-list/tenant-list.component';
import { TenantEditComponent } from './tenant-edit/tenant-edit.component';


const routes: Routes = [
	{
		path: 'tenants',
		canActivate: [AuthGuard],
		children: [
			{
				path: '',
				component: TenantListComponent,
				children: [
					{
						path: 'add',
						component: TenantEditComponent,
						data: {
							isEdit: false
						}
					},
					{
						path: ':tenantId/edit',
						component: TenantEditComponent,
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
			},
		]
	}
];

@NgModule({
	imports: [RouterModule.forChild(routes)],
	exports: [RouterModule]
})
export class TenantRoutingModule { }
