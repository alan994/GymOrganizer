import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, HttpResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { HttpErrorResponse } from '@angular/common/http';
import 'rxjs/add/operator/catch';

// Remove when https://github.com/angular/angular/pull/18466 gets merged.

@Injectable()
export class WA18396Interceptor implements HttpInterceptor {
	constructor() { }

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		const clonedRequest = req.clone({
			responseType: 'text'
		});

		return next.handle(clonedRequest)
			.map((event: HttpEvent<any>) => {
				if (event instanceof HttpResponse) {
					return event.clone({
						body: JSON.parse(event.body),
					});
				}
			})
			.catch((error: HttpErrorResponse) => {
				const parsedError = Object.assign({}, error, { error: JSON.parse(error.error) });
				return Observable.throw(new HttpErrorResponse(parsedError));
			});
	}
}
