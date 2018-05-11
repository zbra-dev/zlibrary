import { Input, OnInit } from '@angular/core';
import { Subject } from 'rxjs/Subject';
import { ToastMediator } from '../../mediators/toast.mediator';
import { FormControl, ControlValueAccessor, Validator } from '@angular/forms';
import { SuggestionAdapter } from './suggestion.adapter';
import { Suggestion } from './suggestion';

const DEBOUNCE_TIME = 400;

export abstract class AbstractTypeahead implements OnInit {
    @Input()
    public suggestionAdapter: SuggestionAdapter;

    public query: string;
    public queryChanged: Subject<string>;
    public searchResults: Suggestion[];
    public searchResultsCache: Suggestion[];
    public candidate: Suggestion;

    constructor(private toastMediator: ToastMediator) {
        this.queryChanged = new Subject<string>();
        this.cleanResultLists();
    }

    abstract selectElement(element: Suggestion): void;

    public ngOnInit() {
        this.queryChanged.debounceTime(DEBOUNCE_TIME)
            .distinctUntilChanged()
            .subscribe(query => {
                this.query = query;
                if (!query) {
                    this.cleanResultLists();
                    return;
                }

                this.suggestionAdapter.search(query).subscribe(results => {
                    this.searchResults = results;
                    this.searchResultsCache = this.searchResults;
                }, error => {
                    this.toastMediator.show(`Erro ao carregar os resultados da pesquisa: ${error}`);
                });
            });
    }

    public onKeyDown(event: any): void {
        const candidateIndex = this.searchResults.indexOf(this.candidate);
        if (event.key === 'Enter') {
            this.selectElement(this.candidate);
        } else if (event.key === 'ArrowUp') {
            if (candidateIndex > 0) {
                this.candidate = this.searchResults[candidateIndex - 1];
            } else {
                this.candidate = this.searchResults[this.searchResults.length - 1];
            }
        } else if (event.key === 'ArrowDown') {
            if (candidateIndex < this.searchResults.length - 1) {
                this.candidate = this.searchResults[candidateIndex + 1];
            } else {
                this.candidate = this.searchResults[0];
            }
        }
        //Prevent Up Arrow and Down Arrow
        if (event.which === 38 || event.which === 40) {
            event.preventDefault();
        }
        event.target.scrollIntoView();
    }

    public setSuggestionResults() {
        this.searchResults = this.searchResultsCache;
    }

    public onQueryChanged(value: string) {
        this.queryChanged.next(value);
    }

    protected cleanResultLists() {
        this.searchResults = [];
        this.searchResultsCache = [];
    }
}
