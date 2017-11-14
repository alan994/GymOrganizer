import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfficeListComponent } from './office-list/office-list.component';
import { OfficeRoutingModule } from './office-routing.module';
import { EffectsModule } from '@ngrx/effects';
import { OfficeEffects } from './store/office.effects';
import { OfficeService } from '../services/web-api/office.service';

@NgModule({
  imports: [
    CommonModule,
    OfficeRoutingModule,
    EffectsModule.forFeature([OfficeEffects])
  ],
  declarations: [OfficeListComponent],
  providers: [OfficeService]
})
export class OfficeModule { }
