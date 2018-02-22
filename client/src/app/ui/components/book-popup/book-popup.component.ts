import { Component, Input, OnInit, ElementRef, keyframes } from '@angular/core';
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

@Component({
  selector: 'zli-book-popup',
  templateUrl: './book-popup.component.html',
  styleUrls: ['./book-popup.component.scss']
})
export class BookPopupComponent implements OnInit {
  @Input()
  set bookData(value: Book) {
    this.book = value;
    this.getImage();
    this.initBookToSave();
  }
  public book: Book;
  private canEdit = false;

  public user: User;
  public isBusy = false;
  public isOrder = false;
  public image = null;
  public imageLoad = false;

  //Variables to create or edit a book
  public uploadedImage: File = null;

  public searchAuthorTerm = new Subject<string>();
  public searchPublisherTerm = new Subject<string>();

  public filterAuthors: Author[];
  public selectedAuthors: Author[];
  public selectedAuthor: Author;

  public filterPublishers: Publisher[];
  public selectedPublisher: Publisher;
  public hoverPublisher: Publisher;

  public bookToSave = new Book();

  bookForm: FormGroup;

  constructor(private bookService: BookService,
    private coverImageService: CoverImageService,
    private authorService: AuthorService,
    private publisherService: PublisherService,
    private reservationService: ReservationService,
    private loaderMediator: LoaderMediator,
    private toastMediator: ToastMediator,
    private authService: AuthService,
    public authorSuggestionAdapter: AuthorSuggestionAdapter,
    private elementRef: ElementRef) {
    this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
    this.searchAuthor();
    this.searchPublisher();
    this.bookForm = new FormGroup({
      titleControl: new FormControl(this.bookToSave.title, Validators.compose([
        Validators.required,
        Validators.nullValidator,
        this.emptyStringValidator
      ])),
      isbnControl: new FormControl(this.bookToSave.isbn, Validators.compose([
        Validators.required,
        Validators.nullValidator,
        Validators.minLength(13),
        Validators.maxLength(13),
        this.isbnValidator
      ])),
        authorsControl: new FormControl(this.bookToSave.authors, validateAuthors),
      publisherControl: new FormControl(this.selectedPublisher),
      publicationYearControl: new FormControl(this.bookToSave.publicationYear, Validators.compose([
        Validators.required,
        Validators.minLength(4),
        Validators.maxLength(4),
        Validators.min(1900),
        Validators.max((new Date()).getFullYear()),
        Validators.nullValidator,
        this.greaterThanZeroValidator
      ])),
      numberOfCopiesControl: new FormControl(this.bookToSave.numberOfCopies, Validators.compose([
        Validators.required,
        Validators.maxLength(1),
        Validators.min(1),
        Validators.max(5),
        this.greaterThanZeroValidator
      ]))
    });
  }

  ngOnInit() {
    this.user = this.authService.getLoggedUser();
    this.bookForm.controls['publisherControl'].valueChanges.subscribe(value => {
      const error = this.hasSelectedPublisher();
      if (error !== null) {
        this.bookForm.controls['publisherControl'].setErrors(error);
      }
    });
  }

  get titleControl() {
    // console.log(this.bookForm.get('titleControl').errors);
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
            console.log('Reserva Feita' + reservation.reservationReason.status);
            this.isOrder = reservation !== null;
          }, error => {
            this.toastMediator.show(`Error when order the book: ${error}`);
          }
        )
      );
    }
  }

  uploadImage(event) {
    const reader = new FileReader();
    this.imageLoad = true;
    const image = this.elementRef.nativeElement.querySelector('.book_image_uploaded');

    reader.onload = function (e: any) {
      const src = e.target.result;
      image.src = src;
    };

    reader.readAsDataURL(event.target.files[0]);
    this.uploadedImage = event.target.files[0];
  }


  //Todo: transform into component
  public searchAuthor(): void {
    this.searchAuthorTerm.debounceTime(400)
      .distinctUntilChanged()
      .subscribe(term => {
        if (term !== '') {
          this.authorService.search(term).subscribe(
            authors => {
              this.filterAuthors = authors;
            }, error => {
              this.toastMediator.show(`Error loading authors: ${error}`);
            });
        } else {
          this.filterAuthors = [];
        }
      });
  }

  public searchPublisher(): void {
    this.searchPublisherTerm.debounceTime(400)
      .distinctUntilChanged()
      .subscribe(term => {
        if (term !== null && term !== '') {
          this.publisherService.search(term).subscribe(
            publishers => {
              this.filterPublishers = publishers;
            }, error => {
              this.toastMediator.show(`Error loading authors: ${error}`);
            }
          );
        } else {
          this.filterPublishers = [];
        }
      });
  }

  searchTerm(event) {
    if (event.target.name === 'authorSearch') {
      this.searchAuthorTerm.next(event.target.value);
    } else if (event.target.name === 'publisherSearch') {
      this.searchPublisherTerm.next(event.target.value);
    }
  }

  onKeyPressed(event) {
    if (this.filterAuthors !== null && this.filterAuthors.length > 0) {
      const index = this.filterAuthors.indexOf(this.selectedAuthor);
      if (event.key === 'Enter') {
        this.selectElement(this.selectedAuthor);
        this.bookForm.controls['authorsControl'].patchValue(null);
      } else if (event.key === 'ArrowUp') {
        if (index > 0) {
          this.selectedAuthor = this.filterAuthors[index - 1];
        } else {
          const lastIndex = this.filterAuthors.length - 1;
          this.selectedAuthor = this.filterAuthors[lastIndex];
        }
        event.preventDefault();
      } else if (event.key === 'ArrowDown') {
        if (index < this.filterAuthors.length - 1) {
          this.selectedAuthor = this.filterAuthors[index + 1];
        } else {
          this.selectedAuthor = this.filterAuthors[0];
        }
        event.target.scrollIntoView();
      }
    }
    if (this.filterPublishers !== null && this.filterPublishers.length > 0) {
      const index = this.filterPublishers.indexOf(this.hoverPublisher);
      if (event.key === 'Enter') {
        this.selectElement(this.hoverPublisher);
        event.target.value = '';
      } else if (event.key === 'ArrowUp') {
        if (index > 0) {
          this.hoverPublisher = this.filterPublishers[index - 1];
        } else {
          const lastIndex = this.filterPublishers.length - 1;
          this.hoverPublisher = this.filterPublishers[lastIndex];
        }
        event.preventDefault();
      } else if (event.key === 'ArrowDown') {
        if (index < this.filterPublishers.length - 1) {
          this.hoverPublisher = this.filterPublishers[index + 1];
        } else {
          this.hoverPublisher = this.filterPublishers[0];
        }
      }
    }
    console.log('key Pressed:' + event.key);
  }

  public selectElement(object: Object) {
    if (object instanceof Author && !(this.selectedAuthors.some(a => a.id === object.id))) {
      this.selectedAuthors.push(object);
      this.bookForm.controls['authorsControl'].patchValue(null);
      this.filterAuthors = [];
    } else if (object instanceof Publisher && this.selectedPublisher === null) {
      this.bookForm.controls['publisherControl'].patchValue(null);
      this.selectedPublisher = object;
      this.filterPublishers = [];
    }
  }

  public unSelectElement(object: Object) {
    if (object instanceof Author) {
      const index = this.selectedAuthors.indexOf(object);
      if (index > -1) {
        this.selectedAuthors.splice(index, 1);
        this.bookForm.controls['authorsControl'].patchValue(null);
      }
    } else if (object instanceof Publisher) {
      this.selectedPublisher = null;
      this.bookForm.controls['publisherControl'].patchValue(null);
    }
  }

  public saveBook() {
    if (this.book === null) {
      this.bookToSave.coverImageKey = Guid.newGUID();
    }
    this.bookToSave.authors = this.selectedAuthors;
    this.bookToSave.publisher = this.selectedPublisher;
    this.loaderMediator.execute(
      this.bookService.save(this.bookToSave).subscribe(
        book => {
          console.log('Livro Criado' + book);
          if (this.book === null || (this.image !== null && this.uploadedImage !== null)) {
            this.coverImageService.uploadImage(book.coverImageKey, this.uploadedImage).subscribe(
              key => {
                console.log('Image Uploaded' + key);
                this.book = book;
                this.canEdit = false;
              }, error => {
                this.toastMediator.show(`Error when upload Image: ${error}`);
                if (this.image == null) {
                  this.bookService.delete(book).subscribe(
                    deleteError => {
                      this.toastMediator.show(`Error deleting book: ${deleteError}`);
                    });
                }
                this.canEdit = true;
              });
          } else {
            this.book = book;
            this.canEdit = false;
          }
        }, error => {
          this.toastMediator.show(`Error when saving the book: ${error}`);
        }
      )
    );
  }

  public Validate(event) {
    if (event.target.value.length >= event.target.maxLength) {
      switch (event.key) {
        case '0':
        case '1':
        case '2':
        case '3':
        case '4':
        case '5':
        case '6':
        case '7':
        case '8':
        case '9':
          event.preventDefault();
          break;
      }
      event.target.value = event.target.value.slice(0, event.target.maxLength);
    }
    if (event.key === ',' || event.key === '.' || event.key === '-' || event.key === '+' || event.key === 'E' || event.key === 'e') {
      event.preventDefault();
    }
    //Prevent Up Arrow and Down Arrow
    if (event.which === 38 || event.which === 40) {
      event.preventDefault();
    }

    console.log('key Pressed:' + event.which);
  }

  private initBookToSave() {
    if (!!this.book) {
      this.bookToSave = this.book;
      this.selectedPublisher = this.book.publisher;
      this.selectedAuthors = this.book.authors;
    } else {
      this.bookToSave = new Book();
      this.bookToSave.numberOfCopies = 1;
      this.selectedAuthors = [];
      this.selectedPublisher = null;
      this.imageLoad = false;
      this.image = null;
    }
  }

  private greaterThanZeroValidator(control) {
    if (!!!control.value || control.value > 0) {
      return null;
    }

    return {
      greaterThanZero: { inputValue: control.value }
    };
  }

  public emptyStringValidator(control) {
    if (!!control.value && control.value.match(/^ *$/) !== null) {
      return {
        emptyString: { inputValue: '' }
      };
    }
    return null;
  }

  private isbnValidator(control) {
    if (!!!control.value) {
      return null;
    }
    const isbn = control.value.toString();
    const length = isbn.length;
    if (length !== 13) {
      return {
        invalidIsbn: { inputValue: control.value }
      };
    }

    const weightType1 = 1;
    const weightType2 = 3;
    const productor = new Array(length);
    for (let i = 0; i < length; i++) {
      if (i % 2 === 0) {
        productor[i] = isbn[i] * weightType1;
      } else {
        productor[i] = isbn[i] * weightType2;
      }
    }
    if (!(productor.reduce((a, b) => a + b) % 10 === 0)) {
      return {
        invalidIsbn: { inputValue: control.value }
      };
    }
    return null;
  }

  private hasSelectedPublisher() {
    if (this.selectedPublisher === null) {
      return {
        hasSelectedPublisher: { filterPublihserNull: 'select a publisher' }
      };
    }
    return null;
  }
}

// TODO: Move to a separate file
function validateAuthors(control: AbstractControl): { [key: string]: any } {
    const value = control.value;
    if (!value || !value.length) {
        return {
            emptyAuthors: {value: control.value}
        };
    }
    return null;
}
