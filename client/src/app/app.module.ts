import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { AppComponent } from './app.component';
import { RouterModule, Routes } from '@angular/router';
import { ModalModule } from 'ngx-bootstrap';
import { PageNotFoundComponent } from './ui/pages/page-not-found/page-not-found.component';
import { BookListComponent } from './ui/pages/book-list/book-list.component';
import { PagesModule } from './ui/pages/pages.module';
import { AuthGuard } from './ui/guards/auth.guard';
import { LoginComponent } from './ui/pages/login/login.component';
import { Toast } from './ui/components/toast/toast';
import { CookieService } from 'ngx-cookie-service';
import { TranslateModule, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient } from '@angular/common/http';

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

export function HttpLoaderFactory(http: HttpClient){
    return new TranslateHttpLoader(http);
}

@NgModule({
    declarations: [
        AppComponent
    ],
    imports: [
        RouterModule.forRoot(
            appRoutes,
            { enableTracing: true } // Debugging purposes only
        ),
        ModalModule.forRoot(),
        BrowserModule,
        PagesModule,
        TranslateModule.forRoot({
            loader: {
                provide: TranslateLoader,
                useFactory: HttpLoaderFactory,
                deps: [HttpClient]
            }
        })
    ],
    providers: [
        AuthGuard,
        Toast,
        CookieService
    ],
    bootstrap: [AppComponent]
})
export class AppModule {
}
