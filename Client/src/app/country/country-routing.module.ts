import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NavComponent } from '../components/nav/nav.component';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { CountryListComponent } from './country-list/country-list.component';
import { CountryEditComponent } from './country-edit/country-edit.component';


const routes: Routes = [
	{
		path: 'countries',
		canActivate: [AuthGuard],
		children: [
			{
				path: '',
				component: CountryListComponent,
				children: [
					{
						path: 'add',
						component: CountryEditComponent,
						data: {
							isEdit: false
						}
					},
					{
						path: ':cityId/edit',
						component: CountryEditComponent,
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
export class CountryRoutingModule { }
