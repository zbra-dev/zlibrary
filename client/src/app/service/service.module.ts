import { NgModule } from '@angular/core';
import { BookService } from './book.service';
import { CoverImageService } from './cover-image.service';
import { ReservationService } from './reservation.service';
import { AuthService } from './auth.service';
import { AuthorService } from './author.service';
import { PublisherService } from './publisher.service';
import { UserService } from './user.service';
import { LoanService } from './loan.service';

@NgModule({
    providers: [
        BookService,
        CoverImageService,
        ReservationService,
        AuthorService,
        PublisherService,
        AuthService,
        UserService,
        LoanService
    ]
})
export class ServiceModule {
}
