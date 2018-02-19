import { Observable } from 'rxjs/Observable';
import { Book } from '../model/book';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { BookViewModelConverter } from './converter/book.view-model-converter';
import 'rxjs/add/operator/map';
import { HttpErrorResponse } from '@angular/common/http/src/response';

const IMAGE_PATH = 'image/';

@Injectable()
export class CoverImageRepository {

    constructor(private httpClient: HttpClient) {
    }

    public loadImage(book: Book): Observable<string> {
        const url = `${environment.apiUrl}/${IMAGE_PATH}LoadImage/`;
        console.log("book key:  " + book.coverImageKey);
        if (book != null) {
            return this.httpClient.get(url + book.coverImageKey).map((res: string) => res);
        }
    }


    public uploadImage(key: string, file:File): Observable<string> {
        const url = `${environment.apiUrl}/${IMAGE_PATH}upload/` + key;
        console.log("book key:  " + key);
        const formData = new FormData();
        formData.append("file", file);
        console.log("Form Data: " + formData.getAll);
        return this.httpClient.post(url,formData).map((res: string) => res);
    }
}
