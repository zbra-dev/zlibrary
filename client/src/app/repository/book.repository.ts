import {Observable} from 'rxjs/Observable';
import {Book} from '../model/book';
import 'rxjs/add/observable/of';
import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {BookViewModelConverter} from './converter/book.view-model-converter';
import 'rxjs/add/operator/map';

const BOOKS_PATH = 'books';
const IMAGE_PATH = 'image/LoadImage';

@Injectable()
export class BookRepository {
    constructor(private httpClient: HttpClient) {
    }
    url = `${environment.apiUrl}/${BOOKS_PATH}`;
    public findAll(): Observable<Book[]> {
        
        const imageUrl = `${environment.apiUrl}/${IMAGE_PATH}/`;
        return this.httpClient.get(this.url).map((data: any) => data.map(b => {
            var book =  BookViewModelConverter.fromDTO(b);
        
            return book;
        }));
    }

    public search(keyword, orderby)
    {
        
        //search/{keyword}/{orderByValue:int}
        return this.httpClient.get(this.url + '/search/'+keyword+'/'+orderby).map((data: any) => data.map(b => {
            var book =  BookViewModelConverter.fromDTO(b);
        
            return book;
        }));
    }

    public IsBookAvailable(book : Book): Observable<boolean>{
        const isBookAvailableURL = this.url+`/isBookAvailable/${book.id}`;
        return this.httpClient.get(isBookAvailableURL).map((res: boolean) => res);
    }

    public delete(book : Book){
        const deleteURL = this.url+`/${book.id}`;
        return this.httpClient.delete(deleteURL).map((res: boolean) => res);
    }
}
