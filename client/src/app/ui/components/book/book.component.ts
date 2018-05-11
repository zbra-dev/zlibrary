import { Reservation } from './../../../model/reservation';
import { Component, Input, OnInit, ViewEncapsulation, Output, EventEmitter } from '@angular/core';
import { Book } from '../../../model/book';
import { CoverImageService } from '../../../service/cover-image.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { BookService } from '../../../service/book.service';
import { ConfirmComponent } from '../confirm/confirm.component';
import { BsModalService } from 'ngx-bootstrap';
import { modalConfigDefaults } from 'ngx-bootstrap/modal/modal-options.class';
import { ConfirmMediator } from '../../mediators/confirm.mediator';
import { User } from '../../../model/user';
import { ReservationService } from '../../../service/reservation.service';
import { ReservationStatus } from '../../../model/reservation-status';

@Component({
    selector: 'zli-book',
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

    constructor(private coverImageService: CoverImageService,
        private loaderMediator: LoaderMediator,
        private bookService: BookService,
        private toastMediator: ToastMediator,
        private confirmMediator: ConfirmMediator,
        private reservationService: ReservationService) {
        this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
    }

    public ngOnInit(): void {
        this.hasBook = this.book.hasBookReservation(this.user);
        this.isExpired = this.book.calculateExpired(this.user);
    }


    public delete() {
        if (this.book != null) {
            this.loaderMediator.execute(
                this.bookService.delete(this.book).subscribe(
                    res => {
                        this.deleted.emit();
                    },
                    error => {
                        this.toastMediator.show(`O Livro não pode ser deletado pois possui copias emprestadas.`);
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
                        this.toastMediator.show(`Erro ao pedir o livro: ${error}`);
                    }
                )
            );
        }
    }
}


