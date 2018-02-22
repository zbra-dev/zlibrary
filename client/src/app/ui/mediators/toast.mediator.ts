import { Toast } from '../components/toast/toast';
import { Injectable } from '@angular/core';

@Injectable()
export class ToastMediator {

    constructor(private toast: Toast) {
    }

    public show(message: string) {
        this.toast.show(message);
    }
}
