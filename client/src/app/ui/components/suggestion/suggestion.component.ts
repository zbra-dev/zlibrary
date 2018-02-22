import {Component, forwardRef, Input, OnChanges, OnInit, SimpleChanges} from '@angular/core';
import {ControlValueAccessor, FormControl, NG_VALIDATORS, NG_VALUE_ACCESSOR, Validator} from '@angular/forms';
import {Subject} from 'rxjs/Subject';
import {ToastMediator} from '../../mediators/toast.mediator';
import {SuggestionAdapter} from './suggestion.adapter';
import {Suggestion} from './suggestion';

const DEBOUNCE_TIME = 400;

@Component({
    selector: 'zli-suggestion',
    templateUrl: './suggestion.component.html',
    styleUrls: ['./suggestion.component.scss'],
    providers: [
        {provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => SuggestionComponent), multi: true},
        {provide: NG_VALIDATORS, useExisting: forwardRef(() => SuggestionComponent), multi: true}
    ]
})
export class SuggestionComponent implements OnInit, OnChanges, ControlValueAccessor, Validator {
    @Input()
    public suggestionAdapter: SuggestionAdapter;
    @Input()
    public selectionLimit?: number;

    public query: string;
    public queryChanged: Subject<string>;
    public searchResults: Suggestion[];
    public candidate: Suggestion;

    private selected: Suggestion[];
    private propagateChangeFunction: any;
    private validateFunction: any;

    constructor(private toastMediator: ToastMediator) {
        this.selected = [];
        this.queryChanged = new Subject<string>();
        this.searchResults = [];
        this.propagateChangeFunction = () => {
        };
        this.validateFunction = () => {
        };
    }

    public ngOnInit() {
        this.queryChanged.debounceTime(DEBOUNCE_TIME)
            .distinctUntilChanged()
            .subscribe(query => {
                this.query = query;
                if (!query) {
                    this.searchResults = [];
                    return;
                }

                this.suggestionAdapter.search(query).subscribe(results => {
                    this.searchResults = results;
                }, error => {
                    this.toastMediator.show(`Error loading search results: ${error}`);
                });
            });
    }

    public ngOnChanges(changes: SimpleChanges): void {
        this.propagateChangeFunction(this.selected);
    }

    public writeValue(value: any): void {
        this.selected = value || [];
    }

    public registerOnChange(fn: any): void {
        this.propagateChangeFunction = fn;
    }

    public registerOnTouched(fn: any): void {
    }

    public validate(control: FormControl): any {
        return this.validateFunction(control);
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
        event.target.scrollIntoView();
    }

    public selectElement(element: Suggestion): void {
        this.selected.push(element);
        this.searchResults = [];
        this.query = '';
        this.propagateChangeFunction(this.selected);
    }

    public deselectElement(element: Suggestion): void {
        const index = this.selected.indexOf(element);
        if (index === -1) {
            return;
        }

        this.selected.splice(index, 1);
        this.propagateChangeFunction(this.selected);
    }

    public onQueryChanged(value: string) {
        this.queryChanged.next(value);
    }
}
