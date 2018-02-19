import { Component, Input, OnInit, Output, EventEmitter } from '@angular/core';
import { Book } from '../../../model/book';
import { User } from '../../../model/user';
import { BookService } from '../../../service/book.service';
import { CoverImageService } from '../../../service/coverImage.service';
import { ReservationService } from '../../../service/reservation.service';
import { AuthService } from '../../../service/auth.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { KeyValueDiffers } from '@angular/core';
import { Reservation } from '../../../model/reservation';
import { ReservationStatus } from '../../../model/reservationStatus';

@Component({
  selector: 'zli-book-popup',
  templateUrl: './book-popup.component.html',
  styleUrls: ['./book-popup.component.scss']
})
export class BookPopupComponent implements OnInit {
  @Input()
  public book: Book;

  public user: User;
  public isBusy = false;
  public IsOrder = false;
  public image: string;
  public isAvailable: boolean;
  public differ: any;

  @Output() ordered: EventEmitter<void> = new EventEmitter<void>();

  constructor(private bookService: BookService,
    private coverImageService: CoverImageService,
    private reservationService: ReservationService,
    private loaderMediator: LoaderMediator,
    private toastMediator: ToastMediator,
    private authService: AuthService,
    differs: KeyValueDiffers) {
    this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
    this.differ = differs.find([]).create();
  }

  ngOnInit() {
    this.user = this.authService.getLoggedUser();
    console.log(this.user.name);
  }

  ngDoCheck() {
    var changes = this.differ.diff(this.book);
  
    if (changes) {
      this.IsReserationCreated();
      this.IsAvailable();
      this.getImage();
    }
  }

  public IsAvailable(): void{
    if (this.book != null) {
      this.loaderMediator.execute(
        this.bookService.IsBookAvailable(this.book).subscribe(
          (bool: boolean) => {
            console.log("isAvailable: " + bool);
              this.isAvailable = bool;
          }, error => {
            this.isAvailable = null;
            this.toastMediator.show(`Error loading books: ${error}`);
          }
        )
      );
    }
  }

  public IsReserationCreated(): void{
    if (this.book != null) {
      this.loaderMediator.execute(
        this.reservationService.findByUserId(this.user).subscribe(
          (reservations: Reservation[]) => {
              this.IsOrder = reservations.filter(reservation => reservation.reservationReason.status != ReservationStatus.rejected).map(r => r.bookId).includes(this.book.id);
              console.log("User have reservation: "+ this.IsOrder);
          }, error => {
            this.IsOrder = null;
            this.toastMediator.show(`Error loading books: ${error}`);
          }
        )
      );
    }
  }

  public getImage(): void {
    if (this.book != null) {
        this.loaderMediator.execute(
            this.coverImageService.LoadImage(this.book).subscribe(
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

  public order():void {
    if (this.book != null) {
      this.loaderMediator.execute(
          this.reservationService.order(this.user,this.book).subscribe(
              reservation => {
                console.log("Reserva Feita" + reservation.reservationReason.status);
                  this.IsOrder = reservation != null;
                  this.ordered.emit();
              }, error => {
                  this.toastMediator.show(`Error when order the book: ${error}`);
              }
          )
      );
  }
  }

}
