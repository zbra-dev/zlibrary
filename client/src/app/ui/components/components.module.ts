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
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { DirectivesModule } from '../directives/directives.module';
import { AuthorSuggestionAdapter } from './book-popup/author-suggestion.adapter';
import { MenuComponent } from './menu/menu.component';
import { ReservationListComponent } from './reservation-list/reservation-list.component';
import { PublisherSuggestionAdapter } from './book-popup/publisher-suggestion.adapter';
import { TypeaheadComponent } from './typeahead/single-selection/single-selection-typeahead.component';
import { TypeaheadMultiSelectionComponent } from './typeahead/multi-selection/multi-selection-typeahead.component';
import { ListResultsComponent } from './list-results/list-results.component';
import { LoadImageComponent } from './load-image/load-image.component';
import { BookImageComponent } from './book-image/book-image.component';

@NgModule({
    declarations: [
        BookComponent,
        BookPopupComponent,
        NavbarComponent,
        LoadingOverlayComponent,
        ToastComponent,
        ReservationHistoryComponent,
        ConfirmComponent,
        MenuComponent,
        ReservationListComponent,
        ListResultsComponent,
        TypeaheadComponent,
        TypeaheadMultiSelectionComponent,
        LoadImageComponent,
        BookImageComponent,
    ],
    exports: [
        BookComponent,
        BookPopupComponent,
        NavbarComponent,
        LoadingOverlayComponent,
        ToastComponent,
        ReservationHistoryComponent,
        ConfirmComponent,
        ReactiveFormsModule,
        MenuComponent,
        ReservationListComponent
    ],
    entryComponents: [
        ToastComponent,
        ReservationHistoryComponent,
        ConfirmComponent,
        ReservationListComponent
    ],
    imports: [
        BrowserModule,
        SharedModule,
        FormsModule,
        ReactiveFormsModule,
        DirectivesModule
    ],
    providers: [
        AuthorSuggestionAdapter,
        PublisherSuggestionAdapter
    ]
})
export class ComponentsModule {
}
