import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {BookComponent} from './book/book.component';
import {SharedModule} from '../../shared.module';
import {NavbarComponent} from './navbar/navbar.component';
import {LoadingOverlayComponent} from './loading-overlay/loading-overlay.component';
import {ToastComponent} from './toast/toast.component';


@NgModule({
    declarations: [
        BookComponent,
        NavbarComponent,
        LoadingOverlayComponent,
        ToastComponent
        
    ],
    exports: [
        BookComponent,
        NavbarComponent,
        LoadingOverlayComponent,
        ToastComponent
    ],
    entryComponents: [
        ToastComponent
    ],
    imports: [
        BrowserModule,
        SharedModule
    ],
    providers: []
})
export class ComponentsModule {
}
