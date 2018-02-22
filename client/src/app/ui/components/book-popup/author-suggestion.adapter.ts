import {Injectable} from '@angular/core';
import {SuggestionAdapter} from '../suggestion/suggestion.adapter';
import {Author} from '../../../model/author';
import {Observable} from 'rxjs/Observable';
import {AuthorService} from '../../../service/author.service';

@Injectable()
export class AuthorSuggestionAdapter implements SuggestionAdapter {
    constructor(private service: AuthorService) {
    }

    public search(query: string): Observable<Author[]> {
        return this.service.search(query);
    }
}
