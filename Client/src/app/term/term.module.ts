import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { TermListComponent } from './term-list/term-list.component';
import { TermEditComponent } from './term-edit/term-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { TermRoutingModule } from './term-routing.module';
import { EffectsModule } from '@ngrx/effects';
import { TermEffects } from './store/term.effects';

@NgModule({
	imports: [
		CommonModule,
		TermRoutingModule,
		ReactiveFormsModule,
		EffectsModule.forFeature([TermEffects])
	],
	declarations: [TermListComponent, TermEditComponent]
})
export class TermModule { }
