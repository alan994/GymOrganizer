import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityListComponent } from './city-list/city-list.component';
import { EffectsModule } from '@ngrx/effects';
import { CityEffects } from './store/city.effects';
import { CityEditComponent } from './city-edit/city-edit.component';
import { CityRoutingModule } from './city-routing.module';
import { ReactiveFormsModule } from '@angular/forms';


@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule,
		EffectsModule.forFeature([CityEffects]),
		CityRoutingModule
	],
	declarations: [CityListComponent, CityEditComponent]
})
export class CityModule { }
