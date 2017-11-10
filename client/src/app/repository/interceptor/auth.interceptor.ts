import {HttpHandler, HttpInterceptor, HttpRequest} from '@angular/common/http';
import {AuthRepository} from '../auth.repository';
import {Injectable} from '@angular/core';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {
    constructor(private authRepository: AuthRepository) {
    }

    public intercept(req: HttpRequest<any>, next: HttpHandler): any {
        const loggedUser = this.authRepository.getLoggedUser();
        if (!!loggedUser) {
            const authReq = req.clone({headers: req.headers.set('Authorization', `Bearer ${loggedUser.token}`)});
            return next.handle(authReq);
        }

        return next.handle(req);
    }
}
