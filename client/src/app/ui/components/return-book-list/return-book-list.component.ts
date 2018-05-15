import { Component, OnInit, EventEmitter, Output } from '@angular/core';
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
import { Reservation } from '../../../model/reservation';
import { Book } from '../../../model/book';
import { ReservationService } from '../../../service/reservation.service';
import { BookService } from '../../../service/book.service';
import { ReservationStatus } from '../../../model/reservation-status';

@Component({
  selector: 'zli-return-book-list',
  templateUrl: './return-book-list.component.html',
  styleUrls: ['./return-book-list.component.scss']
})
export class ReturnBookListComponent implements OnInit {

  public modalControl: BsModalRef;
  public reservations : Reservation[];
  private book : Book;
  public needRefreshBook = false;
  
  constructor(private bookService: BookService) {
  }

  ngOnInit() {
  }

  public initWith(book: Book){
    this.book = book;
    this.findBook();
  }
  public close(): void {
    this.modalControl.hide();
  }

  public refreshReservations(){
    this.findBook();
  }

  private findBook(){
    if(!!this.book){
      this.bookService.findById(this.book.id)
      .subscribe((book: Book) => {
          this.reservations = book.reservations.filter(r => r.reservationReason.status === ReservationStatus.Approved && !!r.loan && !r.loan.isReturned);
          this.needRefreshBook = this.reservations.length === 0;
          console.log(this.reservations.length);
      });
    }
  }
}
