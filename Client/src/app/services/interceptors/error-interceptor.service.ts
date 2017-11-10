import { Injectable } from '@angular/core';
import { Router } from '@angular/router';
import { HttpInterceptor } from '@angular/common/http';
import { HttpEvent } from '@angular/common/http';
import { HttpHandler } from '@angular/common/http';
import { HttpRequest } from '@angular/common/http';
import { HttpResponse } from '@angular/common/http';
import { HttpErrorResponse } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';
import { Logger } from '../../services/utils/log.service';
import { catchError } from 'rxjs/operators/catchError';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {

	constructor(private router: Router, private logger: Logger) { }

	intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		return next.handle(request).pipe(
			catchError((err: HttpErrorResponse, caught: Observable<HttpEvent<any>>) => {
				if (err.status === 401 || err.status === 403) {
					this.logger.debug('Error occurred!', request, err, caught);
					this.router.navigate(['/unauthorized']);
					return Observable.of(null);
				}
				else {
					this.logger.debug('Error occurred, redirecting to error page...', request, err, caught);
					this.router.navigate(['/error']);
					return Observable.of(null);
				}
			})
		);
	}
}
