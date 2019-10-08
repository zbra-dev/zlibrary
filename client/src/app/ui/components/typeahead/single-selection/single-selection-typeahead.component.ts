import { Component, OnInit, ViewEncapsulation, Input, OnChanges, SimpleChanges, forwardRef, ChangeDetectorRef } from '@angular/core';
import { ToastMediator } from './../../../mediators/toast.mediator';
import { AbstractTypeahead } from './../abstract-typeahead';
import { NG_VALUE_ACCESSOR, NG_VALIDATORS, ControlValueAccessor, Validator, FormControl } from '@angular/forms';
import { Suggestion } from './../suggestion';

@Component({
    selector: 'single-selection-typeahead',
    templateUrl: './single-selection-typeahead.component.html',
    styleUrls: ['./../typeahead.component.scss'],
    providers: [
        { provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => TypeaheadComponent), multi: true },
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => TypeaheadComponent), multi: true }
    ],
    encapsulation: ViewEncapsulation.None
})
export class TypeaheadComponent extends AbstractTypeahead implements OnInit, ControlValueAccessor, Validator {

    private selected: Suggestion;

    private propagateChangeFunction: any;
    private propagateTouchFunction: any;
    private validateFunction: any;

    constructor(toastMediator: ToastMediator, private changeDetectorReference: ChangeDetectorRef) {
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

    public elementngOnChanges(changes: SimpleChanges): void {
        this.propagateChangeFunction(this.selected);
    }

    public writeValue(value: any): void {
        this.selected = value || null;
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
        if (!this.selected && !!element) {
            setTimeout(() => {
                this.selected = element;
                this.changeDetectorReference.detectChanges();
                this.candidate = null;
                this.cleanResultLists();
                this.query = '';
                this.onQueryChanged(this.query);
                this.propagateChangeFunction(this.selected);
            });
        }
    }

    public deselectElement(): void {
        this.selected = null;
        this.propagateChangeFunction(this.selected);
    }

    public onBlur() {
        this.propagateTouchFunction();
        this.searchResults = [];
    }
}
