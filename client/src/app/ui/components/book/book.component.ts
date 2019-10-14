import { Reservation } from './../../../model/reservation';
import { Component, Input, OnInit, ViewEncapsulation, Output, EventEmitter } from '@angular/core';
import { Book } from '../../../model/book';
import { CoverImageService } from '../../../service/cover-image.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { BookService } from '../../../service/book.service';
import { ConfirmMediator } from '../../mediators/confirm.mediator';
import { User } from '../../../model/user';
import { ReservationService } from '../../../service/reservation.service';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'book',
    templateUrl: './book.component.html',
    styleUrls: ['./book.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class BookComponent implements OnInit {
    @Input() public book: Book;
    @Input() public user: User;
    public isBusy = false;
    public hasBook: boolean;
    public isExpired: boolean;
    public reservations: Reservation[];

    @Output() deleted: EventEmitter<void> = new EventEmitter<void>();
    @Output() view: EventEmitter<Book> = new EventEmitter<Book>();
    @Output() edit: EventEmitter<Book> = new EventEmitter<Book>();

    constructor(private loaderMediator: LoaderMediator,
        private bookService: BookService,
        private toastMediator: ToastMediator,
        private confirmMediator: ConfirmMediator,
        private reservationService: ReservationService,
        private translate: TranslateService) {
        this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
    }

    public ngOnInit(): void {
        this.hasBook = this.book.hasBookReservation(this.user);
        this.isExpired = this.book.calculateExpired(this.user);
    }

    public get isbookAvailable(): boolean {
        return this.book.isAvailable;
    }

    public get isAdmin(): boolean {
        return this.user.isAdministrator;
    }

    public delete() {
        if (this.book != null) {
            this.loaderMediator.execute(
                this.bookService.delete(this.book).subscribe(
                    () => {
                        this.deleted.emit();
                    },
                    error => {
                        this.toastMediator.show(error);
                    }
                )
            );
        }
    }

    public viewBookDetails() {
        this.view.emit(this.book);
    }

    public editBook() {
        this.edit.emit(this.book);
    }

    public orderBook() {
        if (this.book != null && this.user != null) {
            this.loaderMediator.execute(
                this.reservationService.order(this.user, this.book).subscribe(
                    () => {

                    },
                    error => {
                        this.toastMediator.show(error);
                    }
                )
            );
        }
    }
}


