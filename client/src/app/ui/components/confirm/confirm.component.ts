import { Component, OnInit, ViewEncapsulation, Output, EventEmitter } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Book } from '../../../model/book';


@Component({
    selector: 'zli-confirm',
    templateUrl: './confirm.component.html',
    styleUrls: ['./confirm.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class ConfirmComponent implements OnInit {
    @Output() confirmed: EventEmitter<void> = new EventEmitter<void>();
    @Output() cancelled: EventEmitter<void> = new EventEmitter<void>();
    public message: string;
    public title: string;

    constructor(private modalRef: BsModalRef) {
    }

    ngOnInit() {
    }

    public cancel(): void {
        this.modalRef.hide();
        this.cancelled.emit();
    }

    public confirm(): void {
        this.modalRef.hide();
        this.confirmed.emit();
    }

    public close(): void {
        this.modalRef.hide();
    }
}
