import { User } from './user';
import { ReservationReason } from './reservation-reason';
import { Loan } from './loan';

export class Reservation {
    constructor(public id: number,
                public userId: number,
                public bookId: number,
                public reservationReason: ReservationReason,
                public startDate: string,
                public loan: Loan) {
    }
}
