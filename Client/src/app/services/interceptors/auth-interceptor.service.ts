import { Injectable } from '@angular/core';
import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';

import { Observable } from 'rxjs/Observable';
import { OAuthService } from 'angular-oauth2-oidc';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
	constructor(private oAuthService: OAuthService) { }

	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		if (req.url.indexOf('/api/') !== -1) {
			const accessToken = this.oAuthService.getIdToken();
			if (!accessToken) {
				this.oAuthService.initImplicitFlow();
				return next.handle(req);
			}
			const copiedReq = req.clone({ headers: req.headers.append('Authorization', 'Bearer ' + accessToken) });
			return next.handle(copiedReq);
		}
		else {
			return next.handle(req);
		}
	}
}
