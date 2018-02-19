import {User} from './user';
import { ReservationReason } from './reservationReason';
import { LoanStatus } from './loanStatus';

export class Reservation {
    constructor(public id: number, public userId: number,  public bookId: number, public reservationReason: ReservationReason, public startDate: string, public loanStatus: LoanStatus) {
    }
}
