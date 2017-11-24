import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { NavComponent } from '../components/nav/nav.component';
import { TermListComponent } from './term-list/term-list.component';
import { TermEditComponent } from './term-edit/term-edit.component';

const routes: Routes = [
	{
		path: 'offices',
		canActivate: [AuthGuard],
		children: [
			{
				path: '',
				component: TermListComponent,
				children: [
					{
						path: 'add',
						component: TermEditComponent,
						data: {
							isEdit: false
						}
					},
					{
						path: ':termId/edit',
						component: TermEditComponent,
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
export class TermRoutingModule {

}
