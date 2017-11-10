import {Book} from '../../model/book';

export class BookViewModelConverter {
    // TODO: Read missing fields
    public static fromDTO(dto: any): Book {
        const book = new Book(dto.id, dto.title);
        book.synopsis = dto.synopsis;
        return book;
    }
}
