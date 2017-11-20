import { NgModule } from '@angular/core/src/metadata/ng_module';
import { Route } from '@angular/compiler/src/core';
import { Routes } from '@angular/router/src/config';
import { CityListComponent } from './city-list/city-list.component';
import { RouterModule } from '@angular/router/src/router_module';
import { NavComponent } from '../components/nav/nav.component';
import { AuthGuard } from '../services/guards/auth-guard.service';

const routes: Routes = [
  {
    path: 'cities',
    children: [
      {
        path: '',
        component: CityListComponent,
        canActivate: [AuthGuard]
      },
      {
        path: '',
        component: NavComponent,
        outlet: 'nav'
      },
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class CityRoutingModule { }
