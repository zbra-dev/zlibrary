import { ReservationStatus } from './../../../model/reservation-status';
import { BookService } from './../../../service/book.service';
import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { ReservationService } from '../../../service/reservation.service';
import { Reservation } from '../../../model/reservation';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { AuthService } from '../../../service/auth.service';
import { Book } from '../../../model/book';
import { User } from '../../../model/user';
import { CoverImageService } from '../../../service/cover-image.service';

@Component({
    selector: 'zli-reservation-list',
    templateUrl: './reservation-list.component.html',
    styleUrls: ['./reservation-list.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class ReservationListComponent implements OnInit {
    constructor(private reservationService: ReservationService,
                private bookService: BookService,
                private loaderMediator: LoaderMediator,
                private toastMediator: ToastMediator,
                private authService: AuthService ) {
    }

    @Input() reservation: Reservation;
    public user: User;
    public reservationStatus: ReservationStatus;
    public book: Book;
    public isRequested = false;
    public isWaiting = false;
    public date: string;

    public get showRenewButton(): boolean {
        return this.reservation.canBorrow && !this.reservation.isLoanExpired && this.reservation.reservationReason.isApproved;
    }

    ngOnInit() {
        this.user = this.authService.getLoggedUser();
        this.isWaiting = this.reservation.reservationReason.status === ReservationStatus.waiting;
        this.loaderMediator.execute(
            this.bookService.findById(this.reservation.bookId).subscribe(
                book => {
                    this.book = book;
                    this.isRequested = !this.isWaiting
                        && book.reservations.some(r => r.reservationReason.status === ReservationStatus.requested);
                }, error => {
                    this.toastMediator.show(`Error loading books: ${error}`);
                }
            )
        );
        this.setDate();
    }

    public setDate() {
        if (this.isWaiting) {
            this.date = this.reservation.startDate;
        } else {
            this.date = this.reservation.loanStart;
        }
    }

    public renew() {
        this.loaderMediator.execute(
            this.reservationService.order(this.user, this.book).subscribe(
                reservation => {
                    this.isRequested = true;
                }, error => {
                    this.toastMediator.show(`Error ordering book: ${error}`);
                }
            )
        );
    }
}
