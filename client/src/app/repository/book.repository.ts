import { Observable } from 'rxjs/Observable';
import { Book } from '../model/book';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BookViewModelConverter } from './converter/book.view-model-converter';
import 'rxjs/add/operator/map';
import { User } from '../model/user';
import { SearchParametersDTO } from './dto/searchParametersDTO';

const BOOKS_PATH = 'books';

@Injectable()
export class BookRepository {
    constructor(private httpClient: HttpClient) {
    }
    url = `${environment.apiUrl}/${BOOKS_PATH}`;

    public search(keyword, orderby) {
        var dto = new SearchParametersDTO(keyword, orderby);
        var json = JSON.stringify(dto);
        var headers = new Headers({ 'Content-Type': 'application/json' });

        //search/{keyword}/{orderByValue:int}
        return this.httpClient.post(this.url + '/search/', json, {
            headers: new HttpHeaders().set('Content-Type', 'application/json')
        }).map((data: any) => data.map(b => BookViewModelConverter.fromDTO(b)));
    }

    public save(book: Book): Observable<Book> {
        var json = JSON.stringify(book);
        console.log("Book to Create JSON: " + json);
        return this.httpClient.post(this.url, json, {
            headers: new HttpHeaders().set('Content-Type', 'application/json')
        }).map((data: any) => BookViewModelConverter.fromDTO(data));
    }

    public delete(book: Book): Observable<Object> {
        const deleteURL = this.url + `/${book.id}`;
        return this.httpClient.delete(deleteURL);
    }

}
