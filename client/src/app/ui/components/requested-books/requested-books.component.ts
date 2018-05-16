import { ReservationStatus } from './../../../model/reservation-status';
import { Reservation } from './../../../model/reservation';
import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { ReservationService } from '../../../service/reservation.service';
import { AuthService } from '../../../service/auth.service';
import { User } from '../../../model/user';
import { Book } from '../../../model/book';
import index from '@angular/cli/lib/cli';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { BookService } from './../../../service/book.service';

@Component({
    selector: 'zli-requested-books',
    templateUrl: './requested-books.component.html',
    styleUrls: ['./requested-books.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class RequestedBooksComponent implements OnInit {
    constructor(private authService: AuthService, 
                private reservationService: ReservationService) {
    }

    public modalControl: BsModalRef;
    public reservations: Reservation[];
    public reservationStatus : ReservationStatus;
    public loaderMediator : LoaderMediator;
    public bookService : BookService;
    public book : Book;

    ngOnInit() {
        this.reservationService.findByStatus(ReservationStatus.Requested)
            .subscribe((reservations: Reservation[]) => {
                this.reservations = reservations;
            });
    }

    /*public getBook(reservations : Reservation[])
    {
        this.book = this.reservations.bookId;
                return
    }*/



    public close(): void {
        this.modalControl.hide();
    }
}
