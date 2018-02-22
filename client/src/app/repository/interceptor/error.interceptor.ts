import { Injectable } from '@angular/core';
import {
    HTTP_INTERCEPTORS,
    HttpErrorResponse,
    HttpEvent,
    HttpHandler,
    HttpInterceptor,
    HttpRequest
} from '@angular/common/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import { _throw } from 'rxjs/observable/throw';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
    public intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
        return next.handle(req)
            .catch(errorResponse => {
                let errorMessage: string;
                if (errorResponse instanceof HttpErrorResponse) {
                    errorMessage = errorResponse.message || JSON.stringify(errorResponse.error);
                } else {
                    errorMessage = !!errorResponse.message ? errorResponse.message : errorResponse.toString();
                }

                return _throw(errorMessage);
            });
    }
}

export const ErrorInterceptorProvider = {
    provide: HTTP_INTERCEPTORS,
    useClass: ErrorInterceptor,
    multi: true,
};
