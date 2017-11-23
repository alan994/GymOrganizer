import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OfficeListComponent } from './office-list/office-list.component';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { NavComponent } from '../components/nav/nav.component';
import { OfficeEditComponent } from './office-edit/office-edit.component';

const routes: Routes = [
	{
		path: 'offices',
		canActivate: [AuthGuard],
		children: [
			{
				path: '',
				component: OfficeListComponent,
				children: [
					{
						path: 'add',
						component: OfficeEditComponent,
						data: {
							isEdit: false
						}
					},
					{
						path: ':officeId/edit',
						component: OfficeEditComponent,
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
export class OfficeRoutingModule {

}
