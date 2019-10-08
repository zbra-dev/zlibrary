import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Suggestion } from '../typeahead/suggestion';

@Component({
    selector: 'list-results',
    templateUrl: './list-results.component.html',
    styleUrls: ['./list-results.component.scss'],
})
export class ListResultsComponent {
    @Input()
    public searchResults: Suggestion[];
    @Input()
    public candidate: Suggestion;

    @Output()
    selectElement = new EventEmitter();

    constructor() { }

    onMouseDown(event: any): void {
        if (event.button === 0) {
            this.selectElement.emit(this.candidate);
        }
    }
}
