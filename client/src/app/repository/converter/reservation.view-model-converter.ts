import { ReservationReason } from './../../model/reservation-reason';
import { Book } from './../../model/book';
import { User } from './../../model/user';
import { Reservation } from '../../model/reservation';
import { BookViewModelConverter } from './book.view-model-converter';
import { LoanViewModelConverter } from './loan.view-model-converter';

export class ReservationViewModelConverter {
    public static fromDTO(dto: any): Reservation {
         const reservationReason = new ReservationReason(dto.statusId, dto.description);
         const reservation = new Reservation(dto.id,
                                             dto.userId,
                                             dto.bookId,
                                             reservationReason,
                                             dto.startDate,
                                             LoanViewModelConverter.fromDTO(dto.loan));
         return reservation;
    }
}
