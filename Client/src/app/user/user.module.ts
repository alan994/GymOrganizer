import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { UserListComponent } from './user-list/user-list.component';
import { UserEditComponent } from './user-edit/user-edit.component';
import { ReactiveFormsModule } from '@angular/forms';
import { UserRoutingModule } from './user-routing.module';
import { EffectsModule } from '@ngrx/effects';
import { UserEffects } from './store/user.effects';

@NgModule({
	imports: [
		CommonModule,
		ReactiveFormsModule,
		UserRoutingModule,
		EffectsModule.forFeature([UserEffects])
	],
	declarations: [UserListComponent, UserEditComponent]
})
export class UserModule { }
