import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';

import { AppComponent } from './components/app/app.component';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { HomeModule } from './home/home.module';
import { AppRoutingModule } from './app-routing.module';
import { NavComponent } from './components/nav/nav.component';
import { BsDropdownModule } from 'ngx-bootstrap/dropdown';
import { LocationModule } from './locations/location.module';
import { StoreModule } from '@ngrx/store';
import { reducers } from './store/app.reducers';
import { environment } from '../environments/environment';
import { StoreDevtoolsModule } from '@ngrx/store-devtools';
import { OAuthModule } from 'angular-oauth2-oidc';
import { HttpModule } from '@angular/http';


@NgModule({
  declarations: [
    AppComponent,
    NotFoundComponent,
    NavComponent
  ],
  imports: [
    BrowserModule,
    HttpModule,
    HomeModule,
    OAuthModule.forRoot(),
    BsDropdownModule.forRoot(),
    LocationModule,
    AppRoutingModule,
    StoreModule.forRoot(reducers),
    !environment.production ? StoreDevtoolsModule.instrument({ maxAge: 25 }) : []
  ],
  providers: [],
  bootstrap: [AppComponent]
})
export class AppModule { }
