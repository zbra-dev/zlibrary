import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { User } from '../model/user';
import { UserRepository } from '../repository/user.repository';

@Injectable()
export class UserService {

    constructor(private repository: UserRepository) {
    }

    public findById(userId: number): Observable<User> {
        return this.repository.findById(userId);
    }
}
