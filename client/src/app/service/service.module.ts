import {NgModule} from '@angular/core';
import {BookService} from './book.service';
import {CoverImageService} from './coverImage.service';
import {ReservationService} from './reservation.service';
import {AuthService} from './auth.service';

@NgModule({
    providers: [
        BookService,
        CoverImageService,
        ReservationService,
        AuthService
    ]
})
export class ServiceModule {
}
