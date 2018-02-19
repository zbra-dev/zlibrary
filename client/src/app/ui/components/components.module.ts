import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { BookComponent } from './book/book.component';
import { BookPopupComponent } from './book-popup/book-popup.component';
import { SharedModule } from '../../shared.module';
import { NavbarComponent } from './navbar/navbar.component';
import { LoadingOverlayComponent } from './loading-overlay/loading-overlay.component';
import { ToastComponent } from './toast/toast.component';
import { ReservationHistoryComponent } from './reservation-history/reservation-history.component';
import { ConfirmComponent } from './confirm/confirm.component';
import { FormsModule, ReactiveFormsModule  } from '@angular/forms';
import { DirectivesModule } from '../directives/directives.module';

@NgModule({
    declarations: [
        BookComponent,
        BookPopupComponent,
        NavbarComponent,
        LoadingOverlayComponent,
        ToastComponent,
        ReservationHistoryComponent,
        ConfirmComponent
    ],
    exports: [
        BookComponent,
        BookPopupComponent,
        NavbarComponent,
        LoadingOverlayComponent,
        ToastComponent,
        ReservationHistoryComponent,
        ConfirmComponent,
		ReactiveFormsModule
    ],
    entryComponents: [
        ToastComponent,
        ReservationHistoryComponent,
        ConfirmComponent
    ],
    imports: [
        BrowserModule,
        SharedModule,
 		FormsModule,
        ReactiveFormsModule,
        DirectivesModule
    ],
    providers: []
})
export class ComponentsModule {
}
