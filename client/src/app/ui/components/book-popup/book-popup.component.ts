import { Component, Input, OnInit, ElementRef, keyframes, Output, EventEmitter } from '@angular/core';
import { Book } from '../../../model/book';
import { User } from '../../../model/user';
import { BookService } from '../../../service/book.service';
import { CoverImageService } from '../../../service/cover-image.service';
import { ReservationService } from '../../../service/reservation.service';
import { AuthService } from '../../../service/auth.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { KeyValueDiffers } from '@angular/core';
import { Reservation } from '../../../model/reservation';
import { ReservationStatus } from '../../../model/reservation-status';
import { AuthorService } from '../../../service/author.service';
import { Subject } from 'rxjs/Subject';
import { Author } from '../../../model/author';
import { Publisher } from '../../../model/publisher';
import { Isbn } from '../../../model/isbn';
import { Guid } from '../../../model/guid';
import { element } from 'protractor';
import { PublisherService } from '../../../service/publisher.service';
import { FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';
import { AuthorSuggestionAdapter } from './author-suggestion.adapter';
import { PublisherSuggestionAdapter } from './publisher-suggestion.adapter';
import { BookComponent } from '../book/book.component';
import { BookValidator } from '../../validators/book-validator';

@Component({
    selector: 'zli-book-popup',
    templateUrl: './book-popup.component.html',
    styleUrls: ['./book-popup.component.scss']
})
export class BookPopupComponent implements OnInit {
    @Input()
    set bookData(value: Book) {
        if (value !== this.book && this.canEdit) {
            this.canEdit = false;
        }
        this.isNew = !value;
        this.bookForm.reset();
        this.book = value;
        if (!this.book) {
            this.book = new Book();
            this.book.coverImageKey = Guid.newGUID();
            this.hasImageLoad = false;
            this.image = null;
            this.uploadedImage = null;
        } else {
            this.getImage();
        }
    }

    @Output()
    cancelEvent = new EventEmitter();

    @Output()
    updateBookListEvent = new EventEmitter();

    public book = new Book();
    public user: User;
    public uploadedImage: File = null;
    public bookForm: FormGroup;
    public image = null;
    private isNew = true;
    private canEdit = false;
    public isBusy = false;
    public isOrder = false;
    public hasImageLoad = false;

    constructor(private bookService: BookService,
        private coverImageService: CoverImageService,
        private authorService: AuthorService,
        private publisherService: PublisherService,
        private reservationService: ReservationService,
        private loaderMediator: LoaderMediator,
        private toastMediator: ToastMediator,
        private authService: AuthService,
        public authorSuggestionAdapter: AuthorSuggestionAdapter,
        public publisherSuggestionAdapter: PublisherSuggestionAdapter,
        private elementRef: ElementRef) {
        this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
        this.bookForm = new FormGroup({
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

    public getImage(): void {
        if (!!this.book) {
            this.loaderMediator.execute(
                this.coverImageService.loadImage(this.book).subscribe(
                    image => {
                        this.image = image;
                    }, error => {
                        this.image = null;
                        this.toastMediator.show(`Error loading image: ${error}`);
                    }
                )
            );
        }
    }

    public order(): void {
        if (this.book !== null) {
            this.loaderMediator.execute(
                this.reservationService.order(this.user, this.book).subscribe(
                    reservation => {
                        this.isOrder = reservation !== null;
                        this.updateBookListEvent.emit(null);
                    }, error => {
                        this.toastMediator.show(`Error when order the book: ${error}`);
                    }
                )
            );
        }
    }

    uploadImage(event) {
        const reader = new FileReader();
        this.hasImageLoad = true;
        const image = this.elementRef.nativeElement.querySelector('.book-image-uploaded');

        reader.onload = function (e: any) {
            const src = e.target.result;
            image.src = src;
        };

        reader.readAsDataURL(event.target.files[0]);
        this.uploadedImage = event.target.files[0];
    }

    public saveBook() {
        this.loaderMediator.execute(
            this.bookService.save(this.book, this.uploadedImage).subscribe(
                book => {
                    this.bookData = book;
                    this.updateBookListEvent.emit(null);
                }, error => {
                    this.toastMediator.show(`Error when saving the book: ${error}`);
                }
            )
        );
    }

    onCancel(): void {
        this.canEdit = false;
        if (this.isNew) {
            this.cancelEvent.emit(null);
        }
    }
}
