import { Reservation } from '../../model/reservation';
import { ReservationReason } from '../../model/reservation-reason';
import { Book } from '../../model/book';
import { BookViewModelConverter } from './book.view-model-converter';

export class ReservationViewModelConverter {

    public static fromDTO(dto: any): Reservation {
        const reservationReason = new ReservationReason(dto.statusId, dto.description);

        var bookDTO = dto.book;
        var book = BookViewModelConverter.fromDTO(bookDTO);

        const reservation = new Reservation(dto.id, dto.user, book, reservationReason, dto.startDate, dto.loanStatusId);
        return reservation;
    }
}
//bookDTO.id, bookDTO.title, bookDTO.publisher, bookDTO.authors, bookDTO.isbn, bookDTO.synopsis, bookDTO.publicationYear, bookDTO.numberOfCopies, bookDTO.coverImageKey, bookDTO.created, bookDTO.reservation[]
//bookDTO.id, bookDTO.title, bookDTO.publisher, bookDTO.authors, bookDTO.isbn, bookDTO.coverImageKey, bookDTO.synopsis, bookDTO.publicationYear, bookDTO.numberOfCopies, bookDTO.reservations