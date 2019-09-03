import { ReservationStatus } from './reservation-status';

export class ReservationReason {

    public get isApproved(): boolean {
        return this.status == ReservationStatus.Approved;
    }
    
    public get isRejected(): boolean {
        return this.status == ReservationStatus.Rejected;
    }

    public get isReturned(): boolean {
        return this.status == ReservationStatus.Returned;
    }


    constructor(public status: ReservationStatus,
        public description: string) {
    }
}
