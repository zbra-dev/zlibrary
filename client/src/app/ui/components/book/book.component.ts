import {Component, Input, OnInit, ViewEncapsulation} from '@angular/core';
import {Book} from '../../../model/book';

@Component({
    selector: 'zli-book',
    templateUrl: './book.component.html',
    styleUrls: ['./book.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class BookComponent implements OnInit {
    @Input()
    public book: Book;

    constructor() { }

    public ngOnInit(): void {
    }
}
