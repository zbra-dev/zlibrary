import { NgModule } from '@angular/core';
import { BookRepository } from './book.repository';
import { CoverImageRepository } from './cover-image.repository';
import { AuthRepository } from './auth.repository';
import { HttpClientModule } from '@angular/common/http';
import { AuthInterceptorProvider } from './interceptor/auth.interceptor';
import { ErrorInterceptorProvider } from './interceptor/error.interceptor';
import { ReservationRepository } from './reservation.repository';
import { AuthorRepository } from './author.respository';
import { PublisherRepository } from './publisher.repository';

@NgModule({
    imports: [HttpClientModule],
    providers: [
        AuthInterceptorProvider,
        ErrorInterceptorProvider,
        BookRepository,
        CoverImageRepository,
        ReservationRepository,
        AuthRepository,
        AuthorRepository,
        PublisherRepository
    ]
})
export class RepositoryModule {
}
