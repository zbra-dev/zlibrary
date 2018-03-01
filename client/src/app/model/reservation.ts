import { User } from './user';
import { Book } from './book';
import { ReservationReason } from './reservation-reason';
import { LoanStatus } from './loan-status';

export class Reservation {
    constructor(public id: number,
                public user: User,
                public book: Book,
                public reservationReason: ReservationReason,
                public startDate: string,
                public loanStatus: LoanStatus) {
    }
}
