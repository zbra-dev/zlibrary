import {NgModule} from '@angular/core';
import {BookService} from './book.service';
import {AuthService} from './auth.service';

@NgModule({
    providers: [
        BookService,
        AuthService
    ]
})
export class ServiceModule {
}
