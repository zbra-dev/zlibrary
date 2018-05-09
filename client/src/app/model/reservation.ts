import { User } from './user';
import { ReservationReason } from './reservation-reason';

export class Reservation {
    constructor(public id: number,
                public userId: number,
                public bookId: number,
                public reservationReason: ReservationReason,
                public startDate: string,
                public isReturned: boolean,
                public isLoanExpired: boolean,
                public canBorrow: boolean,
                public loanStart: string,
                public loanEnd: string) {
    }
}
