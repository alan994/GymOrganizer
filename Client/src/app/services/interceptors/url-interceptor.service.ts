import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest } from '@angular/common/http';
import { Observable } from 'rxjs/Observable';

export class UrlInterceptor implements HttpInterceptor {
	intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
		console.log('Request intercepted');
		const host = window.location.host;
		if (req.url.startsWith('/api/')) {
			const newUrl = 'http://localhost:5002' + req.url;
			const copiedReq = req.clone({ url:  newUrl});
			return next.handle(copiedReq);
		}
		else {
			return next.handle(req);
		}
	}
}
