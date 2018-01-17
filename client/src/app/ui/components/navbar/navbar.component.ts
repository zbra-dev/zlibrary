import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {AuthService} from "../../../service/auth.service";
import {User} from "../../../model/user";

@Component({
    selector: 'zli-navbar',
    templateUrl: './navbar.component.html',
    styleUrls: ['./navbar.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class NavbarComponent implements OnInit {

    public user: User;

    constructor(private service: AuthService) {
    }
    ngOnInit() {
        this.user = this.service.getLoggedUser();
    }
    public showMenu(texto) {

    }

    public onLogout() {
        this.service.logout();
    }
}
