import { Component, OnInit, Input } from '@angular/core';
import { ToastMediator } from '../../mediators/toast.mediator';
import { AuthService } from '../../../service/auth.service';
import { ReservationService } from '../../../service/reservation.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ReservationStatus } from '../../../model/reservation-status';
import { Order } from '../../../model/order';
import { BsModalRef } from 'ngx-bootstrap';

@Component({
  selector: 'zli-approved-books',
  templateUrl: './approved-books.component.html',
  styleUrls: ['./approved-books.component.scss']
})
export class ApprovedBooksComponent implements OnInit {

  constructor(private reservationService: ReservationService,
              private toastMediator: ToastMediator,
              private loaderMediator: LoaderMediator) {
  }

  @Input() public orders: Order[];
  public modalControl: BsModalRef;

  ngOnInit() {
    this.loaderMediator.execute(
      this.reservationService.findOrdersByStatus(ReservationStatus.Approved)
      .subscribe((orders: Order[]) => {
            this.orders = orders;
        }, error => {
            this.toastMediator.show(`Erro ao carregar os livros: ${error}`);
        }
      )
    );
  }

  public close(): void {
    this.modalControl.hide();
  }
}
