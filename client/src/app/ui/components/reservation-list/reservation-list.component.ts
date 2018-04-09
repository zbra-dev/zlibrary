import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { ReservationService } from "../../../service/reservation.service";
import { Reservation } from "../../../model/reservation";
import { LoaderMediator } from "../../mediators/loader.mediator";
import { ToastMediator } from "../../mediators/toast.mediator";
import { AuthService } from "../../../service/auth.service";
import { Book } from "../../../model/book";
import { User } from "../../../model/user";
import { CoverImageService } from '../../../service/cover-image.service';
import { ReservationStatus } from '../../../model/reservation-status';

@Component({
    selector: 'zli-reservation-list',
    templateUrl: './reservation-list.component.html',
    styleUrls: ['./reservation-list.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class ReservationListComponent implements OnInit {
    constructor(private reservationService: ReservationService,
                private loaderMediator: LoaderMediator,
                private toastMediator: ToastMediator,
                private authService: AuthService ) {
    }


    @Input() reservation: Reservation;
    public user: User;
    reservationStatus: ReservationStatus;

    ngOnInit() {
        this.user = this.authService.getLoggedUser();
    
    }

}
