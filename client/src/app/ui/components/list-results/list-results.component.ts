import { Component, forwardRef, Input, OnChanges, OnInit, SimpleChanges, Output, EventEmitter } from '@angular/core';
import { ControlValueAccessor, FormControl, NG_VALIDATORS, NG_VALUE_ACCESSOR, Validator } from '@angular/forms';
import { Subject } from 'rxjs/Subject';
import { ToastMediator } from '../../mediators/toast.mediator';
import { Suggestion } from '../typeahead/suggestion';

@Component({
    selector: 'zli-list-results',
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
