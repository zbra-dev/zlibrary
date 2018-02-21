import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

@Component({
    selector: 'zli-reservation-history',
    templateUrl: './reservation-history.component.html',
    styleUrls: ['./reservation-history.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class ReservationHistoryComponent implements OnInit {

    constructor(private modalRef: BsModalRef) {
    }

    ngOnInit() {
    }

    public close(): void {
        this.modalRef.hide();
    }
}
