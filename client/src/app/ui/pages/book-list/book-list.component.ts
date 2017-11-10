import {Component, OnInit, ViewEncapsulation} from '@angular/core';
import {BookService} from '../../../service/book.service';
import {Book} from '../../../model/book';
import {LoaderMediator} from '../../mediators/loader.mediator';
import {ToastMediator} from '../../mediators/toast.mediator';

@Component({
    selector: 'zli-book-list',
    templateUrl: './book-list.component.html',
    styleUrls: ['./book-list.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class BookListComponent implements OnInit {
    public books: Book[] = [];
    public isBusy = false;

    constructor(private service: BookService,
                private loaderMediator: LoaderMediator,
                private toastMediator: ToastMediator) {
        this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
    }

    public ngOnInit(): void {
        this.loaderMediator.execute(
            this.service.findAll().subscribe(
                books => {
                    this.books = books;
                }, error => {
                    this.toastMediator.show(`Error loading books: ${error}`);
                }
            )
        );
    }
}
