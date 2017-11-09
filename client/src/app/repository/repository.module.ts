import {NgModule} from '@angular/core';
import {BookRepository} from './book.repository';

@NgModule({
    providers: [BookRepository]
})
export class RepositoryModule {
}
