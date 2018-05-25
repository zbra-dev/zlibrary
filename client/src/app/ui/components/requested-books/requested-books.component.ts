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
                private toastMediator: ToastMediator) {
    }
    @Input()  public orders: Order[];
    public modalControl: BsModalRef;
    public reservationStatus : ReservationStatus;

    ngOnInit() {
        this.reservationService.findOrdersByStatus(ReservationStatus.Requested)
            .subscribe((orders: Order[]) => {
                this.orders = orders;
            });
    }

    public acceptReservation() {
        var order : Order;
        return this.reservationService.approve(order.reservation.id);
    }

    public rejectReservation() {

    }
    
    public close(): void {
        this.modalControl.hide();
    }
}


/*var user = this.reservations[0].userId;
        this.loaderMediator.execute(
            this.reservationService.order(user, this.book).subscribe(
                reservation => {
                    
                }, error => {
                    this.toastMediator.show(`Erro ao aceitar a reserva: ${error}`);
                }
            )
        );*/