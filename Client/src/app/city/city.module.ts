import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CityListComponent } from './city-list/city-list.component';
import { EffectsModule } from '@ngrx/effects';
import { CityEffects } from './store/city.effects';

@NgModule({
  imports: [
    CommonModule,
    EffectsModule.forFeature([CityEffects])
  ],
  declarations: [CityListComponent]
})
export class CityModule { }
