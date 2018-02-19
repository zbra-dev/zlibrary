import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {AuthService} from "../../../service/auth.service";
import {User} from "../../../model/user";
import { BsModalService } from 'ngx-bootstrap';
import { ReservationHistoryComponent } from '../reservation-history/reservation-history.component';

@Component({
    selector: 'zli-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class NavbarComponent implements OnInit {

    public user: User;

    constructor(private modalService: BsModalService, private service: AuthService) {
    }
    
    ngOnInit() {
        this.user = this.service.getLoggedUser();
    }
    public showMenu(texto) {

    }

    public onLogout() {
        this.service.logout();
        window.location.reload();
    }

	public viewReservationHistory(): void {
        this.modalService.show(ReservationHistoryComponent);
    }
}
