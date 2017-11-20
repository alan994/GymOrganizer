import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { OfficeListComponent } from './office-list/office-list.component';
import { OfficeRoutingModule } from './office-routing.module';
import { EffectsModule } from '@ngrx/effects';
import { OfficeEffects } from './store/office.effects';
import { OfficeService } from '../services/web-api/office.service';
import { OfficeEditComponent } from './office-edit/office-edit.component';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule,
		OfficeRoutingModule,
		EffectsModule.forFeature([OfficeEffects])
	],
	declarations: [OfficeListComponent, OfficeEditComponent],
	providers: [OfficeService]
})
export class OfficeModule { }
