import { NgModule } from '@angular/core';
import { Routes } from '@angular/router';
import { CityListComponent } from './city-list/city-list.component';
import { RouterModule } from '@angular/router';
import { NavComponent } from '../components/nav/nav.component';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { CityEditComponent } from './city-edit/city-edit.component';

const routes: Routes = [
	{
		path: 'cities',
		canActivate: [AuthGuard],
		children: [
			{
				path: '',
				component: CityListComponent,
				children: [
					{
						path: 'add',
						component: CityEditComponent,
						data: {
							isEdit: false
						}
					},
					{
						path: ':cityId/edit',
						component: CityEditComponent,
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
export class CityRoutingModule { }
