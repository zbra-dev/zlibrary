import {NgModule} from '@angular/core';
import {BookService} from './book.service';
import {CoverImageService} from './coverImage.service';
import {AuthService} from './auth.service';

@NgModule({
    providers: [
        BookService,
        CoverImageService,
        AuthService
    ]
})
export class ServiceModule {
}
