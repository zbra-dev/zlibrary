import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from '../../service/auth.service';

@Injectable()
export class AuthGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) {
    }

    public canActivate(): boolean {
        const loggedUser = this.authService.getLoggedUser();
        if (!loggedUser) {
            this.router.navigate(['/login']);
            return false;
        }
        return true;
    }
}
