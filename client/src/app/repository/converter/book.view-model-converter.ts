import { Book } from '../../model/book';
import { ReservationViewModelConverter } from './reservation.view-model-converter';

export class BookViewModelConverter {

    public static fromDTO(dto: any): Book {
        const book = new Book();
        book.id = dto.id;
        book.title = dto.title;
        book.publisher = dto.publisher;
        book.authors = dto.authors;
        book.isbn = dto.isbn;
        book.synopsis = dto.synopsis;
        book.publicationYear = dto.publicationYear;
        book.numberOfCopies = dto.numberOfCopies;
        book.coverImageKey = dto.coverImageKey;
        book.created = dto.created;
        book.reservations = dto.reservations.map(r => ReservationViewModelConverter.fromDTO(r));
        return book;
    }
}
