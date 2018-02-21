import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { AuthService } from '../../../service/auth.service';

@Component({
    selector: 'zli-login',
    templateUrl: './login.component.html',
    styleUrls: ['./login.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class LoginComponent implements OnInit {

    constructor(private service: AuthService) {
    }

    public ngOnInit(): void {
    }

    public showError(): string {
        return this.service.getLoginError();
    }

    public deleteLoginError() {
        this.service.deleteLoginError();
    }
}
