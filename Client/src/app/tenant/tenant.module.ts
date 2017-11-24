import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TenantListComponent } from './tenant-list/tenant-list.component';
import { TenantEditComponent } from './tenant-edit/tenant-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TenantRoutingModule } from './tenant-routing.module';
import { EffectsModule } from '@ngrx/effects';
import { TenantEffects } from './store/tenant.effects';

@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule,
		EffectsModule.forFeature([TenantEffects]),
		TenantRoutingModule
	],
	declarations: [TenantListComponent, TenantEditComponent]
})
export class TenantModule { }
