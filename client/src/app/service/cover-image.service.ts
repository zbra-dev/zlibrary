import { CoverImageRepository } from '../repository/cover-image.repository';
import { Observable } from 'rxjs/Observable';
import { Book } from '../model/book';
import { Injectable } from '@angular/core';

@Injectable()
export class CoverImageService {

    constructor(private repository: CoverImageRepository) {
    }

    public loadImage(book: Book): Observable<string> {
        return this.repository.loadImage(book);
    }

    public uploadImage(key: string, file: File): Observable<string> {
        return this.repository.uploadImage(key, file);
    }
}
