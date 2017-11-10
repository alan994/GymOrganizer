import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LoadingComponent } from './components/loading/loading.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';

const routes: Routes = [
	{
		path: 'loading',
		component: LoadingComponent
	},
	{
		path: 'welcome',
		component: WelcomeComponent
	},
	{
		path: 'unauthorized',
		component: UnauthorizedComponent
	},
	{
		path: '',
		redirectTo: '/welcome', pathMatch: 'full'
	},
	{
		path: '**',
		component: NotFoundComponent
	}
];

@NgModule({
	imports: [RouterModule.forRoot(routes)],
	exports: [RouterModule]
})
export class AppRoutingModule { }
