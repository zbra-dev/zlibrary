import {Observable} from 'rxjs/Observable';
import {Book} from '../model/book';
import 'rxjs/add/observable/of';
import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {BookViewModelConverter} from './converter/book.view-model-converter';
import 'rxjs/add/operator/map';

const BOOKS_PATH = 'books';

@Injectable()
export class BookRepository {
    constructor(private httpClient: HttpClient) {
    }

    public findAll(): Observable<Book[]> {
        const url = `${environment.apiUrl}/${BOOKS_PATH}`;
        return this.httpClient.get(url).map((data: any) => data.map(b => BookViewModelConverter.fromDTO(b)));
    }
}
