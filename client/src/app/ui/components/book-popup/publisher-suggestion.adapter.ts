import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/Observable';
import { PublisherService } from '../../../service/publisher.service';
import { SuggestionAdapter } from '../typeahead/suggestion.adapter';
import { Publisher } from '../../../model/publisher';

@Injectable()
export class PublisherSuggestionAdapter implements SuggestionAdapter {
    constructor(private service: PublisherService) {
    }

    public search(query: string): Observable<Publisher[]> {
        return this.service.search(query);
    }
}
