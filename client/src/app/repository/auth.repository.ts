import { User } from '../model/user';
import { Injectable } from '@angular/core';
import { CookieService } from 'ngx-cookie-service';
import { UserViewModelConverter } from './converter/user.view-model-converter';
const USER_KEY = 'user';
const ERROR_KEY = 'loginError';

@Injectable()
export class AuthRepository {

    constructor(private cookieService: CookieService) {
    }

    public getLoggedUser(): User {
        const userData = this.cookieService.get(USER_KEY);
        if (!userData) {
            return null;
        }
        const userObject = JSON.parse(userData);
        return UserViewModelConverter.fromDTO(userObject);
    }

    public logout(): void {
        this.cookieService.delete(USER_KEY);
    }

    public getLoginError() {
        const data = this.cookieService.get(ERROR_KEY);
        if (!data) {
            return null;
        }
        return data;
    }

    public deleteLoginError(): void {
        this.cookieService.delete(ERROR_KEY);
    }
}
