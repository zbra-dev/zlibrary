import { Reservation } from '../../model/reservation';
import { ReservationReason } from '../../model/reservationReason';

export class ReservationViewModelConverter {

    public static fromDTO(dto: any): Reservation {
        var reservationReason = new ReservationReason(dto.statusId, dto.description);
        const reservation = new Reservation(dto.id, dto.user, dto.bookId, reservationReason , dto.startDate);

        return reservation;
    }
}
