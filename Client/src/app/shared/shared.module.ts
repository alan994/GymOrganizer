import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { HttpClient } from '@angular/common/http';

import { TranslateModule, TranslateLoader, MissingTranslationHandler, MissingTranslationHandlerParams } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';

export function HttpLoaderFactory(http: HttpClient) {
	return new TranslateHttpLoader(http);
}

export class Performa365MissingTranslationHandler implements MissingTranslationHandler {
	handle(params: MissingTranslationHandlerParams) {
		return `Key '${params.key}' not found!`;
	}
}

@NgModule({
	declarations: [],
	imports: [
		TranslateModule.forRoot({
			missingTranslationHandler: {
				provide: MissingTranslationHandler,
				useClass: Performa365MissingTranslationHandler
			},
			loader: {
				provide: TranslateLoader,
				useFactory: HttpLoaderFactory,
				deps: [HttpClient]
			}
		})
	],
	exports: [
		CommonModule,
		TranslateModule
	]
})
export class SharedModule {
}
