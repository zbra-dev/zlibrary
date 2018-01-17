import { Observable } from 'rxjs/Observable';
import { Book } from '../model/book';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BookViewModelConverter } from './converter/book.view-model-converter';
import 'rxjs/add/operator/map';
import { HttpErrorResponse } from '@angular/common/http/src/response';

const IMAGE_PATH = 'image/LoadImage';

@Injectable()
export class CoverImageRepository {

    constructor(private httpClient: HttpClient) {
    }

    public LoadImage(book: Book): Observable<string> {
        const url = `${environment.apiUrl}/${IMAGE_PATH}/`;
        console.log("book key:  " + book.coverImageKey);
        if(book != null){
            return this.httpClient.get(url + book.coverImageKey).map((res: string) => res);
        }
    }
}
