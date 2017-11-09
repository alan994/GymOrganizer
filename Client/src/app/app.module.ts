import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { HttpModule } from '@angular/http';

import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { StoreModule } from '@ngrx/store';
import { EffectsModule } from '@ngrx/effects';
import { OAuthModule } from 'angular-oauth2-oidc';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './components/root/app.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { LoadingComponent } from './components/loading/loading.component';
import { NavComponent } from './components/nav/nav.component';
import { WelcomeComponent } from './components/welcome/welcome.component';
import { SharedModule } from './shared/shared.module';
import { HomeModule } from './home/home.module';
import { ErrorModule } from './error/error.module';
import { reducers } from './store/app.reducers';
import { environment } from '../environments/environment';
import { AccountService } from './services/web-api/account.service';
import { Logger } from './services/utils/log.service';
import { UrlInterceptor } from './services/interceptors/url-interceptor.service';
import { ErrorInterceptor } from './services/interceptors/error-interceptor.service';
import { AuthInterceptor } from './services/interceptors/auth-interceptor.service';
import { WA18396Interceptor } from './services/interceptors/json-interceptor.service';


@NgModule({
	declarations: [
		AppComponent,
		NotFoundComponent,
		LoadingComponent,
		NavComponent,
		WelcomeComponent
	],
	imports: [
		BrowserModule,
		HttpClientModule,
		HttpModule,

		SharedModule,
		OAuthModule.forRoot(),
		HomeModule,
		ErrorModule,
		StoreModule.forRoot(reducers),
		EffectsModule.forRoot([]),
		!environment.production ? StoreDevtoolsModule.instrument({ maxAge: 25 }) : [],

		AppRoutingModule
	],
	providers: [
		AccountService,
		Logger,
		{ provide: HTTP_INTERCEPTORS, useClass: UrlInterceptor, multi: true},
		{ provide: HTTP_INTERCEPTORS, useClass: WA18396Interceptor, multi: true},
		// { provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true},
		// { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true},
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
