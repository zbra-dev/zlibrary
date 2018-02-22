import {Observable} from 'rxjs/Observable';
import {Suggestion} from './suggestion';

export interface SuggestionAdapter {
    search(query: string): Observable<Suggestion[]>;
}
