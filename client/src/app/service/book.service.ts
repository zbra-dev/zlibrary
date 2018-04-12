import { BookRepository } from '../repository/book.repository';
import { Observable } from 'rxjs/Observable';
import { Book } from '../model/book';
import { Injectable } from '@angular/core';
import { User } from '../model/user';

@Injectable()
export class BookService {

    constructor(private repository: BookRepository) {
    }

    public search(keyword, orderby): Observable<Book[]> {
        return this.repository.search(keyword, orderby);
    }

    public save(book: Book, file: File): Observable<Book> {
        return this.repository.save(book, file);
    }

    public delete(book: Book): Observable<Object> {
        return this.repository.delete(book);
    }

    public findById(id: number): Observable<Book> {
        return this.repository.findById(id);
    }
}
