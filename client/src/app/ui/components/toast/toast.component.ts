import {ChangeDetectorRef, Component, Input, ViewEncapsulation} from '@angular/core';
import {Observable} from 'rxjs/Observable';
import {Subject} from 'rxjs/Subject';

const TOAST_TIMEOUT = 50000;

@Component({
    selector: 'zli-toast',
    templateUrl: './toast.component.html',
    styleUrls: ['./toast.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class ToastComponent {
    private _message: string;
    private _onDismiss: Subject<any>;

    @Input()
    public set message(value: string) {
        this._message = value;
        this.changeDetectorReference.detectChanges();
    }

    public get message(): string {
        return this._message;
    }

    constructor(private changeDetectorReference: ChangeDetectorRef) {
        this._onDismiss = new Subject();
        setTimeout(() => this.dismiss(), TOAST_TIMEOUT);
    }

    public dismiss(): void {
        this._onDismiss.next();
        this._onDismiss.complete();
    }

    public get onDismiss(): Observable<any> {
        return this._onDismiss.asObservable();
    }
}
