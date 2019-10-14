import { Book } from './../../../model/book';
import { ReservationStatus } from './../../../model/reservation-status';
import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { ReservationService } from '../../../service/reservation.service';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ToastMediator } from '../../mediators/toast.mediator';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { Order } from '../../../model/order';
import { ConfirmMediator } from '../../mediators/confirm.mediator';
import { TranslateService } from '@ngx-translate/core';
import { GroupedOrder } from '../../../model/grouped-order';
import { Reservation } from '../../../model/reservation';
import { GroupedOrdersConverter } from '../../../repository/converter/grouped-orders.converter';

@Component({
    selector: 'requested-books',
    templateUrl: './requested-books.component.html',
    styleUrls: ['./requested-books.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class RequestedBooksComponent implements OnInit {
    constructor(private reservationService: ReservationService,
        private toastMediator: ToastMediator,
        private confirmMediator: ConfirmMediator,
        private loaderMediator: LoaderMediator,
        private translate: TranslateService,
        private abstractGroupedOrdersConverter: GroupedOrdersConverter) {
    }

    public orders: GroupedOrder[];
    public modalControl: BsModalRef;
    public reservationStatus: ReservationStatus;

    ngOnInit() {
        this.showRequestedReservations();
    }

    public get hasOrders(): boolean {
        return this.orders && this.orders.length > 0;
    }

    public canLoanBook(book: Book): boolean {
        return book.isAvailable;
    }

    public showEnterWaitingList(reservation: Reservation, book: Book): boolean {
        return !reservation.reservationReason.isWaiting
            && !book.isAvailable;
    }

    public showRequestedReservations() {
        this.reservationService.findRequestedOrders()
            .subscribe((orders: Order[]) => {
                this.orders = this.abstractGroupedOrdersConverter.convertToGroupedOrders(orders);
            });
    }

    public acceptReservation(reservation: Reservation) {
        this.confirmMediator.showDialog(this.translate.instant('BOOKS.APPROVE').toUpperCase(), this.translate.instant('BOOKS.APPROVE_QUESTION')).subscribe(r => {
            if (r) {
                this.loaderMediator.execute(
                    this.reservationService.approve(reservation.id)
                        .subscribe(() => {
                            this.showRequestedReservations();
                        }, error => {
                            this.toastMediator.show(error);
                        }
                        )
                );
            }
        });
    }

    public holdReservation(reservation: Reservation) {

        this.confirmMediator.showDialog(this.translate.instant('BOOKS.WAITING_LIST').toUpperCase(), this.translate.instant('BOOKS.WAITING_LIST_QUESTION')).subscribe(r => {
            if (r) {
                this.loaderMediator.execute(
                    this.reservationService.wait(reservation.id)
                        .subscribe(() => {
                            this.showRequestedReservations();
                        }, error => {
                            this.toastMediator.show(error);
                        }
                        )
                );
            }
        });
    }

    public cancelReservation(reservation: Reservation) {
        this.confirmMediator.showDialog(this.translate.instant('BOOKS.REJECT').toUpperCase(), this.translate.instant('BOOKS.REJECT_QUESTION')).subscribe(r => {
            if (r) {
                this.loaderMediator.execute(
                    this.reservationService.cancel(reservation.id)
                        .subscribe(() => {
                            this.showRequestedReservations();
                        }, error => {
                            this.toastMediator.show(error);
                        }
                        )
                );
            }
        });
    }


    public close(): void {
        this.modalControl.hide();
    }
}