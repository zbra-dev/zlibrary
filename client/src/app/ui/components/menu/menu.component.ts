import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import { ReservationHistoryComponent, ReservationHistoryType } from '../reservation-history/reservation-history.component';
import { BsModalService } from 'ngx-bootstrap';
import { AuthService } from '../../../service/auth.service';
import { User } from '../../../model/user';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

@Component({
    selector: 'zli-menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./menu.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class MenuComponent implements OnInit {

    public user: User;
    public showUserMenu: boolean;

    constructor(private modalService: BsModalService,
        private service: AuthService) {
    }

    ngOnInit() {
        this.user = this.service.getLoggedUser();
    }

    public viewReservationHistory(): void {
        this.modalService.show(ReservationHistoryComponent);
    }

    public onLogout() {
        this.service.logout();
        window.location.reload();
    }

    public toggleMenu() {
        this.showUserMenu = !this.showUserMenu;
    }

    public showRentedBooks() {
        const reservationHistoryModalControl = this.modalService.show(ReservationHistoryComponent);
        const reservationHistoryComponent = reservationHistoryModalControl.content as ReservationHistoryComponent;
        reservationHistoryComponent.reservationHistoryType = ReservationHistoryType.Loaned;
        reservationHistoryComponent.modalControl = reservationHistoryModalControl;
    }

    public showWaitingList() {
        const reservationHistoryModalControl = this.modalService.show(ReservationHistoryComponent);
        const reservationHistoryComponent = reservationHistoryModalControl.content as ReservationHistoryComponent;
        reservationHistoryComponent.reservationHistoryType = ReservationHistoryType.Waiting;
        reservationHistoryComponent.modalControl = reservationHistoryModalControl;
    }
}
