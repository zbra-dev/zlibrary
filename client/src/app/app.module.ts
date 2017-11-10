import {BrowserModule} from '@angular/platform-browser';
import {NgModule} from '@angular/core';
import {AppComponent} from './app.component';
import {RouterModule, Routes} from '@angular/router';
import {PageNotFoundComponent} from './ui/pages/page-not-found/page-not-found.component';
import {BookListComponent} from './ui/pages/book-list/book-list.component';
import {PagesModule} from './ui/pages/pages.module';
import {AuthGuard} from './ui/guards/auth.guard';
import {LoginComponent} from './ui/pages/login/login.component';

const appRoutes: Routes = [
    {
        path: 'books',
        component: BookListComponent,
        canActivate: [AuthGuard]
    },
    {
        path: 'login',
        component: LoginComponent
    },
    {
        path: '',
        redirectTo: '/books',
        pathMatch: 'full'
    },
    {
        path: '**',
        component: PageNotFoundComponent
    }
];

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        RouterModule.forRoot(
            appRoutes,
            {enableTracing: true} // Debugging purposes only
        ),
        BrowserModule,
        PagesModule
    ],
    providers: [AuthGuard],
    bootstrap: [AppComponent]
})
export class AppModule {
}
