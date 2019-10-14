import { ReservationStatus } from './reservation-status';

export class ReservationReason {

    public get isApproved(): boolean {
        return this.status == ReservationStatus.Approved;
    }
    
    public get isRejected(): boolean {
        return this.status == ReservationStatus.Canceled;
    }

    public get isReturned(): boolean {
        return this.status == ReservationStatus.Returned;
    }

    public get isWaiting(): boolean {
        return this.status == ReservationStatus.Waiting;
    }


    constructor(public status: ReservationStatus,
        public description: string) {
    }
}
