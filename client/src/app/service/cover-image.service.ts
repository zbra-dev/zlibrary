import { CoverImageRepository } from '../repository/cover-image.repository';
import { Observable } from 'rxjs/Observable';
import { Book } from '../model/book';
import { Injectable } from '@angular/core';

@Injectable()
export class CoverImageService {

    constructor(private repository: CoverImageRepository) {
    }

    public loadImage(coverImageKey: string): Observable<string> {
        return this.repository.loadImage(coverImageKey);
    }
}
