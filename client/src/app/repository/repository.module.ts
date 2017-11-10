import {NgModule} from '@angular/core';
import {BookRepository} from './book.repository';
import {AuthRepository} from './auth.repository';
import {HttpClientModule} from '@angular/common/http';

@NgModule({
    imports: [HttpClientModule],
    providers: [
        BookRepository,
        AuthRepository
    ]
})
export class RepositoryModule {
}
