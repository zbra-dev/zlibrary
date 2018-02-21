import {User} from './user';
import { ReservationReason } from './reservation-reason';
import { LoanStatus } from './loan-status';

export class Reservation {
    constructor(public id: number,
                public userId: number,
                public bookId: number,
                public reservationReason: ReservationReason,
                public startDate: string,
                public loanStatus: LoanStatus) {
    }
}
