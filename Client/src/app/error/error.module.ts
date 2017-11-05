import { NgModule } from '@angular/core';
import { ErrorPageComponent } from './error-page/error-page.component';
import { ErrorRoutingModule } from './error-routing.module';


@NgModule({
	declarations: [ErrorPageComponent],
	imports: [
		ErrorRoutingModule
	]
})
export class ErrorModule {
}
