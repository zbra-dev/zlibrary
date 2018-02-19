import {Book} from '../../model/book';
import { ReservationViewModelConverter } from './reservation.view-model-converter';

export class BookViewModelConverter {
    // TODO: Read missing fields
    public static fromDTO(dto: any): Book {
        const book = new Book(dto.id, dto.title,dto.publisher,dto.authors,dto.isbn,dto.synopsis,dto.publicationYear,dto.numberOfCopies,dto.coverImageKey,dto.created, dto.reservations.map(r => ReservationViewModelConverter.fromDTO(r)));
        return book;
    }
}
