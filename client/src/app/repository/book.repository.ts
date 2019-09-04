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
import { AuthRepository } from './auth.repository';

const BOOKS_PATH = 'books';
const URL = `${environment.apiUrl}/${BOOKS_PATH}`;

@Injectable()
export class BookRepository {

    constructor(private httpClient: HttpClient,
        private authRepository: AuthRepository) {
    }

    public search(keyword, orderby) {
        const dto = new SearchParametersDTO(keyword, orderby);
        const json = JSON.stringify(dto);

        var URL_COMPLETE = URL + '/search/';
        var user = this.authRepository.getLoggedUser();
        if(user.isAdministrator){
            URL_COMPLETE += 'admin/'
        }

        return this.httpClient.post(URL_COMPLETE, json, {
            headers: new HttpHeaders().set('Content-Type', 'application/json')
        }).map((data: any) => data.map(b => BookViewModelConverter.fromDTO(b)));
    }

    public save(book: Book, file: File): Observable<Book> {
        const json = JSON.stringify(book);
        const formData = new FormData();
        formData.append('file', file);
        formData.append('value', json);

        return this.httpClient.post(URL, formData)
            .map((data: any) => BookViewModelConverter.fromDTO(data));
    }

    public delete(book: Book): Observable<Object> {
        const deleteURL = `${URL}/${book.id}`;
        return this.httpClient.delete(deleteURL);
    }

    public findById(id: number): Observable<Book> {
        const findByIdURL = `${URL}/${id}`;
        return this.httpClient.get(findByIdURL).map((data: any) => BookViewModelConverter.fromDTO(data));
    }

}
