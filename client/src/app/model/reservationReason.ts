import {ReservationStatus} from './reservationStatus';

export class ReservationReason {
    constructor(public status: ReservationStatus,public description:string) {
    }
}
