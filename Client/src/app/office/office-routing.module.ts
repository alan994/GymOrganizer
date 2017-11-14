import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { OfficeListComponent } from './office-list/office-list.component';

const routes: Routes = [
  {
    path: 'offices',
    component: OfficeListComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class OfficeRoutingModule {

}
