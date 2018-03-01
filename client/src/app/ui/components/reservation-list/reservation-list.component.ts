import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { ReservationService } from "../../../service/reservation.service";
import { Reservation } from "../../../model/reservation";
import { LoaderMediator } from "../../mediators/loader.mediator";
import { ToastMediator } from "../../mediators/toast.mediator";
import { AuthService } from "../../../service/auth.service";
import { Book } from "../../../model/book";
import { User } from "../../../model/user";
import { CoverImageService } from '../../../service/cover-image.service';

@Component({
    selector: 'zli-reservation-list',
    templateUrl: './reservation-list.component.html',
    styleUrls: ['./reservation-list.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class ReservationListComponent implements OnInit {
    constructor(private coverImageService: CoverImageService,
                private reservationService: ReservationService,
                private loaderMediator: LoaderMediator,
                private toastMediator: ToastMediator,
                private authService: AuthService ) {
    }


    @Input() reservation: Reservation;
    public image: string;
    public user: User;

    ngOnInit() {
        this.user = this.authService.getLoggedUser();
        this.getImage(this.reservation);

    }


    public getImage(reservation: Reservation): void {
        if (reservation.book != null) {
            console.log('Finding book image..');
            this.loaderMediator.execute(
                this.coverImageService.loadImage(reservation.book).subscribe(
                    image => {
                        console.log('Book image found');
                        this.image = image;
                    }, error => {
                        console.log(`Error find book image: ${error}`);
                        this.image = null;
                        this.toastMediator.show(`Error loading image: ${error}`);
                    }
                )
            );
        } else {
            console.log('Book was not referenced on reservation object');
        }
    }
}
