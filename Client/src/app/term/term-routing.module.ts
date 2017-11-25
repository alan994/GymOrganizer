import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { NavComponent } from '../components/nav/nav.component';
import { TermListComponent } from './term-list/term-list.component';
import { TermEditComponent } from './term-edit/term-edit.component';
import { ApplicationRoleNames } from '../models/config/ApplicationRoleNames';
import { RoleGuard } from '../services/guards/role-guard.service';

const routes: Routes = [
	{
		path: 'terms',
		canActivate: [AuthGuard, RoleGuard],
		children: [
			{
				path: '',
				component: TermListComponent,
				children: [
					{
						path: 'add',
						component: TermEditComponent,
						data: {
							isEdit: false,
							roles: [ApplicationRoleNames.ADMINISTRATOR]
						}
					},
					{
						path: ':termId/edit',
						component: TermEditComponent,
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
export class TermRoutingModule {

}
