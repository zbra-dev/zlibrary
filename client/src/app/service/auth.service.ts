import {Injectable} from '@angular/core';
import {AuthRepository} from '../repository/auth.repository';
import {Observable} from 'rxjs/Observable';
import {User} from '../model/user';

@Injectable()
export class AuthService {
    constructor(private repository: AuthRepository) {
    }

    public login(username: string, password: string): Observable<User> {
        return this.repository.login(username, password);
    }

    public getLoggedUser(): User {
        return this.repository.getLoggedUser();
    }

    public logout(): void {
        this.repository.logout();
    }
}
