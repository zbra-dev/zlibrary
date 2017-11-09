import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {BookComponent} from './book/book.component';
import {SharedModule} from '../../shared.module';
import {NavbarComponent} from './navbar/navbar.component';

@NgModule({
    declarations: [
        BookComponent,
        NavbarComponent
    ],
    exports: [
        BookComponent,
        NavbarComponent
    ],
    imports: [
        BrowserModule,
        SharedModule
    ],
    providers: []
})
export class ComponentsModule {
}
