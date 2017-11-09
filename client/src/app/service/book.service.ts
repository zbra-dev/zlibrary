import {BookRepository} from '../repository/book.repository';
import {Observable} from 'rxjs/Observable';
import {Book} from '../model/book';
import {Injectable} from '@angular/core';

@Injectable()
export class BookService {
    constructor(private repository: BookRepository) {
    }

    public findAll(): Observable<Book[]> {
        return this.repository.findAll();
    }
}
