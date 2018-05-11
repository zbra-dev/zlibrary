import { ReservationStatus } from './reservation-status';

export class ReservationReason {

    public get isApproved(): boolean {
        return this.status !== null && this.status !== ReservationStatus.Rejected;
    }

    constructor(public status: ReservationStatus,
        public description: string) {
    }
}
