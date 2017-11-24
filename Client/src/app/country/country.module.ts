import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CountryListComponent } from './country-list/country-list.component';
import { CountryEditComponent } from './country-edit/country-edit.component';
import { EffectsModule } from '@ngrx/effects';
import { CountryEffects } from './store/country.effects';
import { CountryRoutingModule } from './country-routing.module';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
	imports: [
		CommonModule,
		CountryRoutingModule,
		ReactiveFormsModule,
		EffectsModule.forFeature([CountryEffects])
	],
	declarations: [CountryListComponent, CountryEditComponent]
})
export class CountryModule { }
