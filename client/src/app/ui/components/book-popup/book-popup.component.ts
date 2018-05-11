import { ReservationStatus } from './../../../model/reservation-status';
import { User } from './../../../model/user';
import { Book } from './../../../model/book';
import { Reservation } from './../../../model/reservation';
import { LoanStatus } from './../../../model/loan-status';
import { Component, OnInit, ElementRef, keyframes, Output, EventEmitter, ViewChild } from '@angular/core';
import { BookService } from '../../../service/book.service';
import { CoverImageService } from '../../../service/cover-image.service';
import { ReservationService } from '../../../service/reservation.service';
import { AuthService } from '../../../service/auth.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { AuthorService } from '../../../service/author.service';
import { Author } from '../../../model/author';
import { Publisher } from '../../../model/publisher';
import { Isbn } from '../../../model/isbn';
import { Guid } from '../../../model/guid';
import { PublisherService } from '../../../service/publisher.service';
import { FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';
import { AuthorSuggestionAdapter } from './author-suggestion.adapter';
import { PublisherSuggestionAdapter } from './publisher-suggestion.adapter';
import { BookComponent } from '../book/book.component';
import { BookValidator } from '../../validators/book-validator';

const WAITINGMESSAGE = 'Aguardando aprovação';
const EXPIREDMESSAGE = 'Reserva expirada';
const APPROVEDMESSAGE = 'Reserva aprovada';
const WAITINGLISTMESSAGE = 'Reserva na lista de espera';
const REJECTEDMESSAGE = 'Reserva rejeitada';
const RENEWMESSAGE = 'Vá para sua lista de livros para renovar';

@Component({
    selector: 'zli-book-popup',
    templateUrl: './book-popup.component.html',
    styleUrls: ['./book-popup.component.scss']
})

export class BookPopupComponent implements OnInit {

    public book = new Book();
    public originalBook: Book;
    public user: User;
    public bookForm: FormGroup;
    public newCoverImage: File = null;
    public coverImageURL = null;
    private isNew = true;
    private canEdit = false;
    public isBusy = false;
    public isOrder = false;
    public reservations: Reservation[];
    public message: string;
    public isExpired = false;

    @ViewChild('bookImage')
    imageElement: ElementRef;

    @ViewChild('loadImage')
    loadImage: ElementRef;

    @Output()
    cancelEvent = new EventEmitter();

    @Output()
    updateBookListEvent = new EventEmitter();

    constructor(
        private bookService: BookService,
        private coverImageService: CoverImageService,
        private authorService: AuthorService,
        private publisherService: PublisherService,
        private reservationService: ReservationService,
        private loaderMediator: LoaderMediator,
        private toastMediator: ToastMediator,
        private authService: AuthService,
        public authorSuggestionAdapter: AuthorSuggestionAdapter,
        public publisherSuggestionAdapter: PublisherSuggestionAdapter) {
        this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
        this.bookForm = new FormGroup({
            imageControl: new FormControl(this.newCoverImage, Validators.compose([
                Validators.required,
                BookValidator.validateImageExtension(this.book)
            ])),
            titleControl: new FormControl(this.book.title, Validators.compose([
                Validators.required,
                BookValidator.validateEmptyString()
            ])),
            isbnControl: new FormControl(this.book.isbn, Validators.compose([
                Validators.required,
                BookValidator.validateIsbn()
            ])),
            authorsControl: new FormControl(this.book.authors, BookValidator.validateTypeahead()),
            publisherControl: new FormControl(this.book.publisher, BookValidator.validateTypeahead()),
            publicationYearControl: new FormControl(this.book.publicationYear, Validators.compose([
                Validators.required,
                BookValidator.validatePublicationYear()
            ])),
            numberOfCopiesControl: new FormControl(this.book.numberOfCopies, Validators.compose([
                Validators.required,
                Validators.maxLength(1),
                BookValidator.validateNumberOfCopies(1, 5)
            ]))
        });
    }

    ngOnInit() {
        this.user = this.authService.getLoggedUser();
    }

    public initWith(book: Book) {
        if (!book) {
            throw new Error('Livro não pode ser nulo.');
        }
        this.canEdit = false;
        //Ensures clean validation errors
        this.bookForm.reset();
        this.book = book;
        this.originalBook = Object.assign(new Book(), book);
        this.isNew = !book.id;
        this.isOrder = book.hasBookReservation(this.user);
        //Set Image validate again because book reference has changed
        this.bookForm.get('imageControl').setValidators(BookValidator.validateImageExtension(this.book));
        if (!this.isNew) {
            this.refreshReservationStatus();
        }
        this.isExpired = false;
    }

    public initNewBook() {
        const book = new Book();
        book.coverImageKey = Guid.newGUID();
        this.coverImageURL = null;
        this.newCoverImage = null;
        this.initWith(book);
    }

    public refreshReservationStatus() {

        const currentReservations = this.book.reservations
        .filter(r => r.userId === this.user.id && !r.isReturned);

        if (currentReservations.length === 0) {
            return;
        }

        const currentReservation = currentReservations[0];

        this.reservationService.findByUserId(this.user)
            .subscribe((reservations: Reservation[]) => {

                const userReservations = reservations
                    .filter(r => r.id === currentReservation.id);

                if (userReservations.length !== 0) {
                    this.printReservationState(userReservations[0]);
                }
            });
    }

    private printReservationState(reservation: Reservation | null) {
        if (!reservation) {
            return;
        } else if (reservation.isLoanExpired) {
            this.message = EXPIREDMESSAGE;
            this.isExpired = true;
        } else if (reservation.reservationReason.status === ReservationStatus.Approved) {
                    if (!reservation.canBorrow) {
                        this.message = APPROVEDMESSAGE;
                    } else {
                        this.message = RENEWMESSAGE;
                    }
        } else if (reservation.reservationReason.status === ReservationStatus.Waiting) {
            this.message = WAITINGLISTMESSAGE;
        } else if (reservation.reservationReason.status === ReservationStatus.Rejected) {
            this.message = REJECTEDMESSAGE;
        } else {
            this.message = WAITINGMESSAGE;
        }
    }

    get imageControl() {
        return this.bookForm.get('imageControl');
    }

    get titleControl() {
        return this.bookForm.get('titleControl');
    }

    get isbnControl() {
        return this.bookForm.get('isbnControl');
    }

    get authorsControl() {
        return this.bookForm.get('authorsControl');
    }

    get publisherControl() {
        return this.bookForm.get('publisherControl');
    }

    get publicationYearControl() {
        return this.bookForm.get('publicationYearControl');
    }

    get numberOfCopiesControl() {
        return this.bookForm.get('numberOfCopiesControl');
    }

    public order(): void {
        if (this.book !== null) {
            this.loaderMediator.execute(
                this.reservationService.order(this.user, this.book).subscribe(
                    reservation => {
                        this.isOrder = reservation !== null;
                        this.updateBookListEvent.emit(null);
                        this.printReservationState(reservation);
                    }, error => {
                        this.toastMediator.show(`Error when ordering the book: ${error}`);
                    }
                )
            );
        }
    }

    setCoverImage(image) {
        this.newCoverImage = image;
        if (!!image) {
            const reader = new FileReader();
            reader.readAsDataURL(this.newCoverImage);
            reader.onload = (e: any) => {
                this.coverImageURL = e.target.result;
            };
        } else {
            this.coverImageURL = null;
        }
    }

    public saveBook() {
        this.loaderMediator.execute(
            this.bookService.save(this.book, this.newCoverImage).subscribe(
                book => {
                    this.initWith(book);
                    this.updateBookListEvent.emit(null);
                }, error => {
                    this.toastMediator.show(`Erro ao salvar o livro: ${error}`);
                }
            )
        );
    }

    public onCancel(): void {
        this.canEdit = false;
        this.book = this.originalBook;
        if (this.isNew) {
            this.cancelEvent.emit(null);
        }
    }
}
