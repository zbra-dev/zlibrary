import {User} from '../model/user';
import {Observable} from 'rxjs/Observable';
import {Injectable} from '@angular/core';

const USER_KEY = 'user';

@Injectable()
export class AuthRepository {
    private static generateDummyUser(): User {
        return new User({id: 1, username: 'admin', name: 'Administrator', token: 'secret_token'});
    }

    public login(username: string, password: string): Observable<User> {
        if (username === 'admin' && password === 'admin') {
            const user = AuthRepository.generateDummyUser();
            sessionStorage.setItem(USER_KEY, JSON.stringify(user));
            return Observable.of(user);
        }

        sessionStorage.removeItem(USER_KEY);
        return Observable.of(null);
    }

    public getLoggedUser(): User {
        const userData = sessionStorage.getItem(USER_KEY);
        if (!userData) {
            return null;
        }
        const userObject = JSON.parse(userData);
        return new User(userObject);
    }

    public logout(): void {
        sessionStorage.removeItem(USER_KEY);
    }
}
