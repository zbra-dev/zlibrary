import {User} from './user';
import { ReservationReason } from './reservationReason';

export class Reservation {
    constructor(public id: number, public user: User,  public bookId: number, public reservationReason: ReservationReason, public startDate: string) {
    }
}
