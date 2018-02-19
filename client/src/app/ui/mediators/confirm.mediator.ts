import { BsModalService } from 'ngx-bootstrap/modal/bs-modal.service';
import { ConfirmComponent } from '../components/confirm/confirm.component';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';

@Injectable()
export class ConfirmMediator {
    constructor(private modalService: BsModalService) {
    }

    public showDialog(message: string): Observable<boolean> {
        return new Observable<boolean>(o => {
            const modalRef = this.modalService.show(ConfirmComponent);
            const confirmComponent = <ConfirmComponent>modalRef.content;
            confirmComponent.message = message;
            confirmComponent.confirmed.subscribe(() => o.next(true));
            confirmComponent.cancelled.subscribe(() => o.next(false));
        });
    }
}