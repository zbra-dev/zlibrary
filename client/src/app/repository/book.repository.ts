import {Observable} from 'rxjs/Observable';
import {Book} from '../model/book';
import 'rxjs/add/observable/of';
import {Injectable} from '@angular/core';

@Injectable()
export class BookRepository {
    private static generateDummyBooks() {
        const books = [];
        for (let i = 0; i < 5; i++) {
            const book = new Book(i, `Book ${i}`);
            book.author = `Author ${i}`;
            books.push(book);
        }
        return books;
    }

    public findAll(): Observable<Book[]> {
        const dummyBooks = BookRepository.generateDummyBooks();
        return Observable.of(dummyBooks);
    }
}
