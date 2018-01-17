import {Book} from '../../model/book';

export class BookViewModelConverter {
    // TODO: Read missing fields
    public static fromDTO(dto: any): Book {
        const book = new Book(dto.id, dto.title,dto.publisher,dto.authors,dto.isbn,dto.synopsis,dto.publicationYear,dto.numberOfCopies,dto.coverImageKey,dto.created);
        return book;
    }
}
