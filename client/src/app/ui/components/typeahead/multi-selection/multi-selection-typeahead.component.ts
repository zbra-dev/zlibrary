import { Component, OnInit, ViewEncapsulation, Input, OnChanges, SimpleChanges, forwardRef } from '@angular/core';
import { ToastMediator } from '../../../mediators/toast.mediator';
import { AbstractTypeahead } from '../abstract-typeahead';
import { NG_VALUE_ACCESSOR, NG_VALIDATORS, ControlValueAccessor, Validator, FormControl } from '@angular/forms';
import { Suggestion } from '../suggestion';

@Component({
    selector: 'zli-multi-selection-typeahead',
    templateUrl: './multi-selection-typeahead.component.html',
    styleUrls: ['./../typeahead.component.scss'],
    providers: [
        { provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => TypeaheadMultiSelectionComponent), multi: true },
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => TypeaheadMultiSelectionComponent), multi: true }
    ],
    encapsulation: ViewEncapsulation.None
})
export class TypeaheadMultiSelectionComponent extends AbstractTypeahead implements OnInit, OnChanges, ControlValueAccessor, Validator {

    @Input()
    public selectionLimit?: number;

    private selected: Suggestion[];

    private propagateChangeFunction: any;
    private propagateTouchFunction: any;
    private validateFunction: any;

    constructor(toastMediator: ToastMediator) {
        super(toastMediator);
        this.propagateChangeFunction = () => {
        };
        this.propagateTouchFunction = () => {
        };
        this.validateFunction = () => {
        };
    }

    ngOnInit() {
        super.ngOnInit();
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
        this.propagateTouchFunction = fn;
    }

    public validate(control: FormControl): any {
        return this.validateFunction(control);
    }

    public selectElement(element: Suggestion): void {
        if (!(this.selected.some(e => e.id === element.id))) {
            setTimeout(() => {
             this.selected.push(element);
             this.candidate = null;
             this.cleanResultLists();
             this.query = '';
             this.onQueryChanged(this.query);
             this.propagateChangeFunction(this.selected);
            });
        }
    }

    public deselectElement(element: Suggestion): void {
        const index = this.selected.indexOf(element);
        if (index === -1) {
            return;
        }

        this.selected.splice(index, 1);
        this.propagateChangeFunction(this.selected);
    }

    public onBlur() {
        this.propagateTouchFunction();
        this.searchResults = [];
    }
}
