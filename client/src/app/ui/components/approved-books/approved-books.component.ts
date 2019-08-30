import { Component, OnInit, Input } from '@angular/core';
import { ToastMediator } from '../../mediators/toast.mediator';
import { AuthService } from '../../../service/auth.service';
import { ReservationService } from '../../../service/reservation.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ReservationStatus } from '../../../model/reservation-status';
import { Order } from '../../../model/order';
import { BsModalRef } from 'ngx-bootstrap';
import { ConfirmMediator } from '../../mediators/confirm.mediator';
import { TranslateService } from '@ngx-translate/core';

@Component({
  selector: 'zli-approved-books',
  templateUrl: './approved-books.component.html',
  styleUrls: ['./approved-books.component.scss']
})
export class ApprovedBooksComponent implements OnInit {

  constructor(private reservationService: ReservationService,
    private toastMediator: ToastMediator,
    private loaderMediator: LoaderMediator,
    private confirmMediator: ConfirmMediator,
    private translate: TranslateService) {
  }

  @Input() public orders: Order[];
  public modalControl: BsModalRef;

  ngOnInit() {
    this.refreshList();
  }

  public refreshList() {
    this.reservationService.findOrdersByStatus(ReservationStatus.Approved)
      .subscribe((orders: Order[]) => {
        this.orders = orders;
      }
      )
  }

  public returnBook(reservationId: number) {
    this.confirmMediator.showDialog(this.translate.instant('BOOKS.RETURN').toUpperCase(), this.translate.instant('BOOKS.RETURN_QUESTION')).subscribe(r => {
      if (r) {
        this.loaderMediator.execute(
          this.reservationService.returnBook(reservationId)
            .subscribe(() => {
              this.refreshList();
            }, error => {
              this.toastMediator.show(error);
            }
            )
        );
      }
    });
  }

  public close(): void {
    this.modalControl.hide();
  }
}
