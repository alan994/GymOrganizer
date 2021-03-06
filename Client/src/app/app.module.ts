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
import { AccountEffects } from './store/account/account.effect';
import { UnauthorizedComponent } from './components/unauthorized/unauthorized.component';
import { StoreRouterConnectingModule } from '@ngrx/router-store';
import { RouterEffects } from './store/router/router.effects';
import { OfficeModule } from './office/office.module';
import { AuthGuard } from './services/guards/auth-guard.service';
import { CityModule } from './city/city.module';
import { CityService } from './services/web-api/city.service';
import { ReactiveFormsModule } from '@angular/forms';
import { SignalRService } from './services/utils/signalR.service';
import { NotificationHelperService } from './services/utils/notification-helper.service';
import { CountryModule } from './country/country.module';
import { CountryService } from './services/web-api/country.service';
import { TenantModule } from './tenant/tenant.module';
import { TenantService } from './services/web-api/tenant.service';
import { UserService } from './services/web-api/user.service';
import { TermService } from './services/web-api/term.service';
import { TermModule } from './term/term.module';
import { UserModule } from './user/user.module';
import { RoleGuard } from './services/guards/role-guard.service';

@NgModule({
	declarations: [
		AppComponent,
		NotFoundComponent,
		LoadingComponent,
		NavComponent,
		UnauthorizedComponent,
		WelcomeComponent
	],
	imports: [
		BrowserModule,
		HttpClientModule,
		ReactiveFormsModule,
		HttpModule,

		SharedModule,
		OAuthModule.forRoot(),
		HomeModule,
		OfficeModule,
		CityModule,
		CountryModule,
		UserModule,
		TermModule,
		ErrorModule,
		TenantModule,
		StoreModule.forRoot(reducers),
		EffectsModule.forRoot([AccountEffects, RouterEffects]),
		StoreRouterConnectingModule,
		!environment.production ? StoreDevtoolsModule.instrument({ maxAge: 25 }) : [],

		AppRoutingModule
	],
	providers: [
		AccountService,
		SignalRService,
		AuthGuard,
		RoleGuard,
		NotificationHelperService,
		Logger,
		{ provide: HTTP_INTERCEPTORS, useClass: UrlInterceptor, multi: true },
		{ provide: HTTP_INTERCEPTORS, useClass: WA18396Interceptor, multi: true },
		{ provide: HTTP_INTERCEPTORS, useClass: AuthInterceptor, multi: true },
		{ provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true },
		CityService,
		CountryService,
		TenantService,
		UserService,
		TermService
	],
	bootstrap: [AppComponent]
})
export class AppModule { }
