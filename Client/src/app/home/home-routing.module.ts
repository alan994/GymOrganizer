import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { HomeComponent } from './home/home.component';
import { AuthGuard } from '../services/guards/auth-guard.service';
import { NavComponent } from '../components/nav/nav.component';

const routes: Routes = [
	{
		path: 'home',
		children: [
			{
				path: '',
				component: HomeComponent,
				canActivate: [AuthGuard]
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
	imports: [
		RouterModule.forChild(routes)
	],
	exports: [
		RouterModule
	],
	//providers: [AuthGuard]
})
export class HomeRoutingModule {

}
