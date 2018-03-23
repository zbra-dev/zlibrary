import { Injectable } from '@angular/core';
import { Author } from '../../../model/author';
import { Observable } from 'rxjs/Observable';
import { PublisherService } from '../../../service/publisher.service';
import { SuggestionAdapter } from '../typeahead/suggestion.adapter';

@Injectable()
export class PublisherSuggestionAdapter implements SuggestionAdapter {
    constructor(private service: PublisherService) {
    }

    public search(query: string): Observable<Author[]> {
        return this.service.search(query);
    }
}
