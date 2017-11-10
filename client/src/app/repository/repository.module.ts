import {NgModule} from '@angular/core';
import {BookRepository} from './book.repository';
import {AuthRepository} from './auth.repository';
import {HTTP_INTERCEPTORS, HttpClientModule} from '@angular/common/http';
import {AuthInterceptor} from './interceptor/auth.interceptor';

@NgModule({
    imports: [HttpClientModule],
    providers: [
        {
            provide: HTTP_INTERCEPTORS,
            useClass: AuthInterceptor,
            multi: true,
        },
        BookRepository,
        AuthRepository
    ]
})
export class RepositoryModule {
}
