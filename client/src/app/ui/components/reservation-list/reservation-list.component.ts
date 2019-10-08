import { ReservationStatus } from './../../../model/reservation-status';
import { BookService } from './../../../service/book.service';
import { Component, Input, OnInit, ViewEncapsulation, EventEmitter, Output } from '@angular/core';
import { ReservationService } from '../../../service/reservation.service';
import { Reservation } from '../../../model/reservation';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { AuthService } from '../../../service/auth.service';
import { Book } from '../../../model/book';
import { User } from '../../../model/user';
import { TranslateService } from '@ngx-translate/core';
import { ConfirmMediator } from '../../mediators/confirm.mediator';

@Component({
    selector: 'reservation-list',
    templateUrl: './reservation-list.component.html',
    styleUrls: ['./reservation-list.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class ReservationListComponent implements OnInit {
    constructor(private reservationService: ReservationService,
                private bookService: BookService,
                private loaderMediator: LoaderMediator,
                private toastMediator: ToastMediator,
                private authService: AuthService,
                private translate: TranslateService,
                private confirmMediator: ConfirmMediator ) {
    }

    @Input() reservation: Reservation;
    public user: User;
    public reservationStatus: ReservationStatus;
    public book: Book;
    public isOrdered = false;
    public isWaiting = false;
    public isRequested = false;
    public date: string;
    public isExpired: boolean;

    @Output()
    updateBookListEvent = new EventEmitter();

    ngOnInit() {
        this.user = this.authService.getLoggedUser();
        this.isWaiting = this.reservation.reservationReason.status === ReservationStatus.Waiting;
        this.isRequested = this.reservation.reservationReason.status === ReservationStatus.Requested;
        this.isExpired = !!this.reservation.loan && this.reservation.loan.isExpired;
        this.loaderMediator.execute(
            this.bookService.findById(this.reservation.bookId).subscribe(
                book => {
                    this.book = book;
                    this.isOrdered = !this.isWaiting
                        && book.reservations.some(r => r.reservationReason.status === ReservationStatus.Requested);
                }, error => {
                    this.toastMediator.show(`Erro ao carregar os livros: ${error}`);
                }
            )
        );
        this.setDate();
    }

    public get showRenewButton(): boolean {
        return !!this.reservation.loan && this.reservation.loan.canBorrow && !this.reservation.loan.isExpired && this.reservation.reservationReason.isApproved;
    }

    public get canRenewBook(): boolean {
        return this.showRenewButton && !this.isOrdered && !this.isWaiting;
    }

    public get isRenewalRequested(): boolean {
        return this.isOrdered && !this.isRequested;
    }

    public get isBookRented(): boolean {
        return !this.showRenewButton && !this.isWaiting && !this.isRequested && !this.isExpired;
    }

    public get isReservationAccepted(): boolean {
        return !this.showRenewButton && this.isWaiting;
    }

    public setDate() {
        if (this.isWaiting || this.isRequested) {
            this.date = this.reservation.startDate;
        } else {
            this.date = this.reservation.loan.startDate;
        }
    }

    public renew() {
        this.loaderMediator.execute(
            this.reservationService.order(this.user, this.book).subscribe(
                reservation => {
                    this.isOrdered = true;
                }, error => {
                    this.toastMediator.show(`Erro ao renovar o livro: ${error}`);
                }
            )
        );
    }

     public cancelReservation() {
        this.confirmMediator.showDialog(this.translate.instant('BOOKS.QUIT').toUpperCase(), this.translate.instant('BOOKS.QUIT_WAITING_LIST_QUESTION')).subscribe(r => {
            if (r) {
                this.loaderMediator.execute(
                    this.reservationService.cancel(this.reservation.id)
                        .subscribe(() => {
                            this.updateBookListEvent.emit(null);
                        }, error => {
                            this.toastMediator.show(error);
                        }
                        )
                );
            }
        });
    }
}
