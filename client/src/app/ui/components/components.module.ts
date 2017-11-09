import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BookComponent } from './book/book.component';
import {SharedModule} from '../../shared.module';

@NgModule({
    declarations: [
        BookComponent
    ],
    exports: [
        BookComponent
    ],
    imports: [
        BrowserModule,
        SharedModule
    ],
    providers: []
})
export class ComponentsModule {
}
