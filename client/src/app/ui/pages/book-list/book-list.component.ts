import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import { BookService } from '../../../service/book.service';
import { Book } from '../../../model/book';
import { NavbarComponent } from '../../components/navbar/navbar.component';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { Input, EventEmitter } from '@angular/core';
import { User } from '../../../model/user';
import { AuthService } from '../../../service/auth.service';

@Component({
    selector: 'zli-book-list',
    templateUrl: './book-list.component.html',
    styleUrls: ['./book-list.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class BookListComponent implements OnInit {
    public books: Book[] = [];
    public selectedBook: Book;
    public isBusy = false;
    public user: User;

    keyword = '';

    constructor(private service: BookService,
        private loaderMediator: LoaderMediator,
        private toastMediator: ToastMediator,
        private authService: AuthService) {
        this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
    }

    public get isAdmin() { return this.user.isAdministrator; }

    public ngOnInit(): void {
        this.searchBy(this.keyword, 0);
        this.user = this.authService.getLoggedUser();
    }


    public search(): void {
        this.searchBy(this.keyword, 0);
    }

    public searchBy(keyword, orderBy) {
        this.loaderMediator.execute(
            this.service.search(this.keyword, 0).subscribe(
                books => {
                    this.books = books;
                }, error => {
                    this.toastMediator.show(`Error loading books: ${error}`);
                }
            )
        );
    }

    public toggleSidebar(): void {
        document.getElementById('background').classList.toggle('active');
        if (document.getElementById('sidebar').classList.toggle('active') === false) {
            this.isBusy = false;
            this.selectedBook = null;
        }
    }

    public onSelect(book: Book): void {
        this.selectedBook = book;
        this.toggleSidebar();
    }

    public updateBookStatus(message: string): void {
        this.search();
    }

    public onBookDeleted(message: string): void {
        this.books = null;
        this.search();
    }
    public onBookEdited(book: Book): void {
        this.onSelect(book);
    }
}
