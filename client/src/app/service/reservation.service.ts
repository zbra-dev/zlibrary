import { ReservationRepository } from '../repository/reservation.repository';
import { Observable } from 'rxjs/Observable';
import { Book } from '../model/book';
import { User } from '../model/user';
import { Reservation } from '../model/reservation';
import { Injectable } from '@angular/core';

@Injectable()
export class ReservationService {

    constructor(private repository: ReservationRepository) {
    }

    public findByUserId(user: User): Observable<Reservation[]> {
        return this.repository.findByUserId(user);
    }

    public order(user: User, book: Book): Observable<Reservation> {
        return this.repository.order(user, book);
    }
}
