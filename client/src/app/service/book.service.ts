import {BookRepository} from '../repository/book.repository';
import {Observable} from 'rxjs/Observable';
import {Book} from '../model/book';
import {Injectable} from '@angular/core';
import {User} from '../model/user';

@Injectable()
export class BookService {
    constructor(private repository: BookRepository) {
    }

    public search(keyword, orderby):Observable<Book[]>{
        return this.repository.search(keyword, orderby);
    }

    public IsBookAvailable(book : Book): Observable<boolean>{
        return this.repository.IsBookAvailable(book);
    }

    public delete(book: Book):  Observable<Object>{
        return this.repository.delete(book);
    }

}
