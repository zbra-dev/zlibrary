import { Component, Input, OnInit, ViewEncapsulation, Output, EventEmitter } from '@angular/core';
import { Book } from '../../../model/book';
import { CoverImageService } from '../../../service/coverImage.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { BookService } from '../../../service/book.service';
import { ConfirmComponent } from '../confirm/confirm.component';
import { BsModalService } from 'ngx-bootstrap';
import { modalConfigDefaults } from 'ngx-bootstrap/modal/modal-options.class';
import { ConfirmMediator } from '../../mediators/confirm.mediator';
import { User } from '../../../model/user';
import { ReservationService } from '../../../service/reservation.service';
import { ReservationStatus } from '../../../model/reservationStatus';
import { LoanStatus } from '../../../model/loanStatus';


@Component({
    selector: 'zli-book',
    templateUrl: './book.component.html',
    styleUrls: ['./book.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class BookComponent implements OnInit {
    @Input() public book: Book;
    @Input() public user: User;
    public image: string;
    public isBusy = false;
    public hasBook: boolean;

    @Output() deleted: EventEmitter<void> = new EventEmitter<void>();
    @Output() view: EventEmitter<Book> = new EventEmitter<Book>();

    constructor(private coverImageService: CoverImageService,
        private loaderMediator: LoaderMediator,
        private bookService: BookService,
        private toastMediator: ToastMediator,
        private confirmMediator: ConfirmMediator,
        private reservationService: ReservationService) {
        this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
    }

    public ngOnInit(): void {
        this.getImage();
        this.hasBook = this.getUserReservations();
    }

    public getImage(): void {
        if (this.book != null) {
            this.loaderMediator.execute(
                this.coverImageService.LoadImage(this.book).subscribe(
                    image => {
                        this.image = image;
                    }, error => {
                        this.image = null;
                        this.toastMediator.show(`Error loading books: ${error}`);
                    }
                )
            );
        }
    }

    public delete() {
        if (this.book != null) {
            this.loaderMediator.execute(
                this.bookService.delete(this.book).subscribe(
                    res => {
                        this.deleted.emit();
                    },
                    error => {
                        this.toastMediator.show(`Error loading books: ${error}`);
                    }
                )
            );
        }
    }
    public deleteModal() {
        this.confirmMediator.showDialog('Você tem certeza que gostaria de deletar esse livro?').subscribe(b => {
            if (b) {
                this.delete();
            }
        });
    }

    public viewBookDetails() {
        this.view.emit(this.book);
    }

    public orderBook() {
        if (this.book != null && this.user != null) {
            this.loaderMediator.execute(
                this.reservationService.order(this.user, this.book).subscribe(
                    res => {

                    },
                    error => {
                        this.toastMediator.show(`Error loading books: ${error}`);
                    }
                )
            );
        }
    }

    public getUserReservations(): boolean {
        if (this.book.reservations.length > 0) {
            let userReservations = this.book.reservations.filter(r => r.userId == this.user.id);
            return userReservations.length > 0 && userReservations.some(r => r.reservationReason.status != ReservationStatus.rejected || r.loanStatus != LoanStatus.returned)
        }
        return false;
    }
}


