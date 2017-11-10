import {NgModule} from '@angular/core';
import {BookRepository} from './book.repository';
import {AuthRepository} from './auth.repository';

@NgModule({
    providers: [
        BookRepository,
        AuthRepository
    ]
})
export class RepositoryModule {
}
