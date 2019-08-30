import { FeatureSettings } from './../../../model/feature-settings';
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
import { BsModalService } from 'ngx-bootstrap';
import { ReturnBookListComponent } from '../return-book-list/return-book-list.component';
import { FeatureSettingsService } from '../../../service/feature-settings.service';
import { TranslateService } from '@ngx-translate/core';

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
    public featureSettings: FeatureSettings;

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
        public publisherSuggestionAdapter: PublisherSuggestionAdapter,
        private modalService: BsModalService,
        private featureSettingsService: FeatureSettingsService,
        private translate: TranslateService) {
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
            ])),
            editionControl: new FormControl(this.book.edition, Validators.compose([
                Validators.required,
                BookValidator.validateEmptyString()
            ]))
        });
    }

    ngOnInit() {
        this.user = this.authService.getLoggedUser();
        this.featureSettingsService.getFeatureSettings().subscribe(
            featureSettings => {
                this.featureSettings = featureSettings;
            }, error => {
                this.toastMediator.show(`${error}`);
            });
    }

    public initWith(book: Book) {
        if (!book) {
            throw new Error('Livro não pode ser nulo.');
        }
        this.canEdit = false;
        //Set Image validate again because book reference has changed
        if (this.allowCoverImage) {
            this.bookForm.get('imageControl').setValidators(BookValidator.validateImageExtension(this.book));
        } else {
            this.bookForm.get('imageControl').setValidators(BookValidator.noImageValidation());
        }
        //Ensures clean validation errors
        this.bookForm.reset();
        this.book = book;
        this.originalBook = Object.assign(new Book(), book);
        this.isNew = !book.id;
        this.isOrder = book.hasBookReservation(this.user);

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

    public get imageControl() {
        return this.bookForm.get('imageControl');
    }

    public get titleControl() {
        return this.bookForm.get('titleControl');
    }

    public get isbnControl() {
        return this.bookForm.get('isbnControl');
    }

    public get authorsControl() {
        return this.bookForm.get('authorsControl');
    }

    public get publisherControl() {
        return this.bookForm.get('publisherControl');
    }

    public get publicationYearControl() {
        return this.bookForm.get('publicationYearControl');
    }

    public get numberOfCopiesControl() {
        return this.bookForm.get('numberOfCopiesControl');
    }

    public get editionControl() {
        return this.bookForm.get('editionControl');
    }

    public get canRequestReservation(): boolean {
        return (!this.isOrder && !this.isAdmin)
            || (this.isAdmin && !this.book.isAvailable);
    }

    public get isBookAvailable(): boolean {
        return this.book.isAvailable;
    }

    public get isAdmin(): boolean {
        return this.user.isAdministrator;
    }

    public get canRequestBookRental(): boolean {
        return this.isBookAvailable
            && !this.isAdmin;
    }

    public get hasCoverImageUrl(): boolean {
        return !!this.coverImageURL;
    }

    public get requestedForReservation(): boolean {
        return this.isOrder
            && !this.isExpired
            && !this.isAdmin
    }

    public get isUploadedImageInvalid(): boolean {
        return this.allowCoverImage
            && this.isImageInvalid
            && !!this.imageControl.errors;
    }

    public get isImageInvalid(): boolean {
        return this.imageControl.invalid
            && (this.imageControl.dirty || this.imageControl.touched);
    }

    public get isImageExtensionInvalid(): boolean {
        return this.imageControl.errors ? this.imageControl.errors.extensionInvalid : false;
    }

    public get canAddToWaitingList(): boolean {
        return !this.book.isAvailable
            && !this.isAdmin;
    }

    public get canAdminProvideBookToUsers(): boolean {
        return !this.book.isAvailable
            && this.isAdmin;
    }

    public get wasOrderSuccesfull(): boolean {
        return this.isExpired
            && !this.isAdmin;
    }

    public get canManageBook(): boolean {
        return this.isNew
            || this.canEdit;
    }

    public get isBookTitleEmpty(): boolean {
        return this.isBookTitleInvalid
            && !this.titleControl.value;
    }

    public get isBookTitleInvalid(): boolean {
        return this.titleControl.invalid
            && (this.titleControl.dirty || this.titleControl.touched);
    }

    public get isBookEditionInvalid(): boolean {
        return this.editionControl.invalid
            && (this.editionControl.dirty || this.editionControl.touched);
    }

    public get isIsbnEmpty(): boolean {
        return this.isIsbnInvalid
            && !this.isbnControl.value;
    }

    public get isIsbnInvalid(): boolean {
        return this.isbnControl.invalid
            && (this.isbnControl.dirty || this.isbnControl.touched);
    }

    public get isIsbnIncorrect(): boolean {
        return this.isbnControl.dirty
            && !!this.isbnControl.errors
            && !!this.isbnControl.value;
    }

    public get isAuthorEmpty(): boolean {
        return this.isAuthorInvalid
            && !this.authorsControl.value;
    }

    public get isAuthorInvalid(): boolean {
        return this.authorsControl.invalid
            && (this.authorsControl.touched || this.authorsControl.dirty);
    }

    public get isPublisherEmpty(): boolean {
        return this.isPublisherInvalid
            && !this.publisherControl.value;
    }

    public get isPublisherInvalid(): boolean {
        return this.publisherControl.invalid
            && (this.publisherControl.touched || this.publisherControl.dirty);
    }

    public get isPublicationYearEmpty(): boolean {
        return this.isPublicationYearInvalid
            && !this.publicationYearControl.value;
    }

    public get isPublicationYearInvalid(): boolean {
        return this.publicationYearControl.invalid
            && (this.publicationYearControl.dirty || this.publicationYearControl.touched);
    }

    public get isPublicationYearIncorrect(): boolean {
        return this.publicationYearControl.dirty
            && !!this.publicationYearControl.errors
            && !!this.publicationYearControl.value;
    }

    public get isNumberOfCopiesEmpty(): boolean {
        return this.isNumberOfCopiesInvalid
            && !this.numberOfCopiesControl.value;
    }

    public get isNumberOfCopiesInvalid(): boolean {
        return this.numberOfCopiesControl.invalid
            && (this.numberOfCopiesControl.dirty || this.numberOfCopiesControl.touched);
    }

    public get isNumberOfCopiesIncorrect(): boolean {
        return this.numberOfCopiesControl.dirty
            && this.numberOfCopiesControl.errors
            && !!this.numberOfCopiesControl.value;
    }

    public get existingBook(): boolean {
        return this.canEdit && !this.isNew;
    }

    public get isImageSelected(): boolean {
        return !this.isNew || !!this.newCoverImage;
    }

    public get isImageLoaded(): boolean {
        return !this.coverImageURL && !this.isNew;
    }

    public get allowCoverImage(): boolean {
        if (this.featureSettings) {
            return this.featureSettings.allowCoverImage;
        }
    }

    public refreshReservationStatus() {

        const currentReservations = this.book.reservations
            .filter(r => r.userId === this.user.id && (!r.reservationReason.isRejected || r.reservationReason.isApproved && !!r.loan && !r.loan.isReturned));

        if (currentReservations.length === 0) {
            return;
        }

        const currentReservation = currentReservations[0];

        this.reservationService.findByUserId(this.user)
            .subscribe((reservations: Reservation[]) => {

                const userReservations = reservations
                    .filter(r => r.id === currentReservation.id && !r.reservationReason.isReturned);

                if (userReservations.length !== 0) {
                    this.printReservationState(userReservations[0]);
                }
            });
    }

    private printReservationState(reservation: Reservation | null) {
        if (!reservation) {
            return;
        } else if (!!reservation.loan && reservation.loan.isExpired) {
            this.message = this.translate.instant('MESSAGE.EXPIRED');
            this.isExpired = true;
        } else if (reservation.reservationReason.isApproved) {
            if (!!reservation.loan && !reservation.loan.canBorrow) {
                this.message = this.translate.instant('MESSAGE.APPROVED');
            } else {
                this.message = this.translate.instant('MESSAGE.RENEW');
            }
        } else if (reservation.reservationReason.status === ReservationStatus.Waiting) {
            this.message = this.translate.instant('MESSAGE.WAITING_LIST');
        } else if (reservation.reservationReason.isRejected) {
            this.message = this.translate.instant('MESSAGE.REJECTED');
        } else {
            this.message = this.translate.instant('MESSAGE.WAITING');
        }
    }

    public order(): void {
        if (!!this.book) {
            this.loaderMediator.execute(
                this.reservationService.order(this.user, this.book).subscribe(
                    reservation => {
                        this.isOrder = reservation !== null;
                        this.updateBookListEvent.emit(null);
                        this.printReservationState(reservation);
                    }, error => {
                        this.toastMediator.show(`Erro ao pedir o livro: ${error}`);
                    }
                )
            );
        }
    }

    public setCoverImage(image) {
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

    public openReturnBookList() {
        const returnBookListModalControl = this.modalService.show(ReturnBookListComponent);
        const returnBookListComponent = returnBookListModalControl.content as ReturnBookListComponent;
        returnBookListComponent.initWith(this.book);
        returnBookListComponent.modalControl = returnBookListModalControl;
        this.modalService.onHide.subscribe(() => {
            if (returnBookListComponent.needRefreshBook) {
                returnBookListModalControl.hide();
                this.refreshBook();
            }
        });
    }

    private refreshBook() {
        this.loaderMediator.execute(
            this.bookService.findById(this.book.id).subscribe(
                book => {
                    this.initWith(book);
                    this.updateBookListEvent.emit(null);
                }, error => {
                    this.toastMediator.show(`Livro não encontrado: ${error}`);
                }
            )
        );
    }

}
