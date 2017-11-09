import { Component, OnInit, ViewEncapsulation } from '@angular/core';
import {BookService} from '../../../service/book.service';
import {Book} from '../../../model/book';

@Component({
    selector: 'zli-book-list',
    templateUrl: './book-list.component.html',
    styleUrls: ['./book-list.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class BookListComponent implements OnInit {
    public books: Book[] = [];

    constructor(private service: BookService) {
    }

    public ngOnInit(): void {
        this.service.findAll().subscribe(
            books => {
                this.books = books;
            }, error => {
                console.error(error);
            }, () => {
                console.log('Finished loading books!');
            }
        );
    }
}
