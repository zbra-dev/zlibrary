import {NgModule} from '@angular/core';
import {BookRepository} from './book.repository';
import {AuthRepository} from './auth.repository';
import {HttpClientModule} from '@angular/common/http';
import {AuthInterceptorProvider} from './interceptor/auth.interceptor';
import {ErrorInterceptorProvider} from './interceptor/error.interceptor';

@NgModule({
    imports: [HttpClientModule],
    providers: [
        AuthInterceptorProvider,
        ErrorInterceptorProvider,
        BookRepository,
        AuthRepository
    ]
})
export class RepositoryModule {
}
