import { NgModule } from '@angular/core';
import { BookService } from './book.service';
import { CoverImageService } from './coverImage.service';
import { ReservationService } from './reservation.service';
import { AuthService } from './auth.service';
import { AuthorService } from './author.service';
import { PublisherService } from './publisher.service';

@NgModule({
    providers: [
        BookService,
        CoverImageService,
        ReservationService,
        AuthorService,
        PublisherService,
        AuthService
    ]
})
export class ServiceModule {
}
