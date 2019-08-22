import {Observable} from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/switchMap';
import {Author} from '../model/author';
import {Injectable} from '@angular/core';
import {AuthorRepository} from '../repository/author.respository';

@Injectable()
export class AuthorService {

    constructor(private repository: AuthorRepository) {
    }

    public search(keyword: string): Observable<Author[]> {
        return this.repository.search(keyword);
    }

    public save(author: Author): Observable<Author> {
        return this.repository.save(author);
    }
}
