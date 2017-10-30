import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { LocationListComponent } from './location-list/location-list.component';
import { LocationRoutingModule } from './location-routing.module';

@NgModule({
  imports: [
    CommonModule,
    LocationRoutingModule
  ],
  declarations: [LocationListComponent]
})
export class LocationModule { }
