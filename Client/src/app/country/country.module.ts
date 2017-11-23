import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { CountryListComponent } from './country-list/country-list.component';
import { CountryEditComponent } from './country-edit/country-edit.component';

@NgModule({
	imports: [
		CommonModule
	],
	declarations: [CountryListComponent, CountryEditComponent]
})
export class CountryModule { }
