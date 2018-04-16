import { ReservationStatus } from './../../../model/reservation-status';
import { element } from 'protractor';
import { ReactiveFormsModule } from '@angular/forms';
import { Reservation } from './../../../model/reservation';
import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { ReservationService } from '../../../service/reservation.service';
import { AuthService } from '../../../service/auth.service';
import { User } from '../../../model/user';
import index from '@angular/cli/lib/cli';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

@Component({
    selector: 'zli-reservation-history',
    templateUrl: './reservation-history.component.html',
    styleUrls: ['./reservation-history.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class ReservationHistoryComponent implements OnInit {
    constructor(private authService: AuthService, private reservationService: ReservationService) {
    }

    public modalControl: BsModalRef;
    public user: User;
    public reservations: Reservation[];
    public reservationHistoryType: ReservationHistoryType;

    public get isLoaned(): boolean {
        return this.reservationHistoryType === ReservationHistoryType.Loaned;
    }

    public get isWaiting(): boolean {
        return this.reservationHistoryType === ReservationHistoryType.Waiting;
    }

    ngOnInit() {
        this.user = this.authService.getLoggedUser();
        this.showReservations();
    }

    private convertToReservationStatus(reservationHistoryType: ReservationHistoryType): ReservationStatus {
        if (reservationHistoryType === ReservationHistoryType.Loaned) {
            return ReservationStatus.approved;
        }
        if (reservationHistoryType === ReservationHistoryType.Waiting) {
            return ReservationStatus.waiting;
        }
        throw new Error('Reservation Status is invalid');
    }

    public showReservations() {
        this.reservationService.findByUserId(this.user)
            .subscribe((reservations: Reservation[]) => {
                const reservationStatus = this.convertToReservationStatus(this.reservationHistoryType);
                this.reservations = reservations
                    .filter(r => r.reservationReason.status === reservationStatus && !r.canBorrow);
            });
    }

    public close(): void {
        this.modalControl.hide();
    }
}

export enum ReservationHistoryType {
    Loaned = 1,
    Waiting = 2
}
