import {CoverImageRepository} from '../repository/coverImage.repository';
import {Observable} from 'rxjs/Observable';
import {Book} from '../model/book';
import {Injectable} from '@angular/core';

@Injectable()
export class CoverImageService {
    constructor(private repository: CoverImageRepository) {
    }

    public LoadImage(book: Book): Observable<string> {
        return this.repository.LoadImage(book);
    }
}
