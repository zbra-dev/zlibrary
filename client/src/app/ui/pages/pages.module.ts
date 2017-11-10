import {NgModule} from '@angular/core';
import {CommonModule} from '@angular/common';
import {SharedModule} from '../../shared.module';
import {BookListComponent} from './book-list/book-list.component';
import {PageNotFoundComponent} from './page-not-found/page-not-found.component';
import {ComponentsModule} from '../components/components.module';
import {LoginComponent} from './login/login.component';
import {ReactiveFormsModule} from '@angular/forms';

@NgModule({
    imports: [
        CommonModule,
        ReactiveFormsModule,
        SharedModule,
        ComponentsModule
    ],
    declarations: [
        BookListComponent,
        PageNotFoundComponent,
        LoginComponent
    ]
})
export class PagesModule {
}
