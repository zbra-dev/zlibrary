import { Reservation } from '../../model/reservation';
import { ReservationReason } from '../../model/reservation-reason';

export class ReservationViewModelConverter {

    public static fromDTO(dto: any): Reservation {
        const reservationReason = new ReservationReason(dto.statusId, dto.description);
        const reservation = new Reservation(dto.id, dto.userId, dto.bookId, reservationReason, dto.startDate, dto.loanStatusId);

        return reservation;
    }
}
