import { Component, OnInit, ViewEncapsulation, forwardRef, SimpleChanges, ChangeDetectorRef, ViewChild, ElementRef } from '@angular/core';
import { NG_VALUE_ACCESSOR, NG_VALIDATORS, FormControl, ControlValueAccessor, Validator } from '@angular/forms';
import { ToastMediator } from '../../mediators/toast.mediator';

@Component({
    selector: 'load-image',
    templateUrl: './load-image.component.html',
    styleUrls: ['./load-image.component.scss'],
    providers: [
        { provide: NG_VALUE_ACCESSOR, useExisting: forwardRef(() => LoadImageComponent), multi: true },
        { provide: NG_VALIDATORS, useExisting: forwardRef(() => LoadImageComponent), multi: true },
    ],
    encapsulation: ViewEncapsulation.None
})
export class LoadImageComponent implements OnInit, ControlValueAccessor, Validator {

    private file: File;

    private propagateChangeFunction: any;
    private propagateTouchFunction: any;
    private validateFunction: any;

    constructor(toastMediator: ToastMediator, private changeDetectorReference: ChangeDetectorRef) {
        this.propagateChangeFunction = () => {
        };
        this.propagateTouchFunction = () => {
        };
        this.validateFunction = () => {
        };
    }

    ngOnInit() {
    }

    public elementngOnChanges(changes: SimpleChanges): void {
        this.propagateChangeFunction(this.file);
    }

    public writeValue(value: any): void {
        this.file = value || null;
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

    public loadImage(event) {
        this.file = event.target.files[0];
        this.propagateChangeFunction(this.file);
    }
    public onTouch() {
        this.propagateTouchFunction();
    }
}
