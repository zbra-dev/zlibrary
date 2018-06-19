import { ReservationStatus } from './../../../model/reservation-status';
import { Reservation } from './../../../model/reservation';
import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { ReservationService } from '../../../service/reservation.service';
import { AuthService } from '../../../service/auth.service';
import { User } from '../../../model/user';
import { Book } from '../../../model/book';
import index from '@angular/cli/lib/cli';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { ToastMediator } from '../../mediators/toast.mediator';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { Order } from '../../../model/order';

@Component({
    selector: 'zli-requested-books',
    templateUrl: './requested-books.component.html',
    styleUrls: ['./requested-books.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class RequestedBooksComponent implements OnInit {
    constructor(private authService: AuthService,
        private reservationService: ReservationService,
        private toastMediator: ToastMediator,
        private loaderMediator: LoaderMediator) {
    }

    @Input() public orders: Order[];
    public modalControl: BsModalRef;
    public reservationStatus: ReservationStatus;

    ngOnInit() {
        this.showRequestedReservations();
    }

    public showRequestedReservations() {
        this.reservationService.findOrdersByStatus(ReservationStatus.Requested)
            .subscribe((orders: Order[]) => {
                this.orders = orders;
            });
    }


    public acceptReservation(order: Order) {
        if (confirm("Deseja aprovar está reserva?")) {
            this.loaderMediator.execute(
                this.reservationService.approve(order.reservation.id)
                    .subscribe(() => {
                        console.log('Reserva Aprovada!');
                        this.showRequestedReservations();
                    }
                        , error => {
                            this.toastMediator.show(`Erro ao pedir o livro: ${error}`);
                        }
                    )
            );
        }
    }

    public rejectReservation(order: Order) {
        if (confirm("Deseja recusar está reserva?")) {
            this.loaderMediator.execute(
                this.reservationService.reject(order.reservation.id)
                    .subscribe(() => {
                        console.log('Reserva Recusada!');
                        this.showRequestedReservations();
                    }
                        , error => {
                            this.toastMediator.show(`Erro ao pedir o livro: ${error}`);
                        }
                    )
            );
        }
    }


    public close(): void {
        this.modalControl.hide();
    }
}