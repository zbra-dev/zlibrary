import { Component, OnInit, Input } from '@angular/core';
import { ToastMediator } from '../../mediators/toast.mediator';
import { ReservationService } from '../../../service/reservation.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ReservationStatus } from '../../../model/reservation-status';
import { Order } from '../../../model/order';
import { BsModalRef } from 'ngx-bootstrap';
import { ConfirmMediator } from '../../mediators/confirm.mediator';
import { TranslateService } from '@ngx-translate/core';
import { GroupedOrder } from '../../../model/grouped-order';
import { GroupedOrdersConverter } from '../../../repository/converter/grouped-orders.converter';

@Component({
  selector: 'approved-books',
  templateUrl: './approved-books.component.html',
  styleUrls: ['./approved-books.component.scss']
})
export class ApprovedBooksComponent implements OnInit {

  constructor(private reservationService: ReservationService,
    private toastMediator: ToastMediator,
    private loaderMediator: LoaderMediator,
    private confirmMediator: ConfirmMediator,
    private translate: TranslateService,
    private abstractGroupedOrdersConverter: GroupedOrdersConverter) {
  }

  public orders: GroupedOrder[];
  public modalControl: BsModalRef;

  ngOnInit() {
    this.refreshList();
  }

  public refreshList() {
    this.reservationService.findOrdersByStatus(ReservationStatus.Approved)
      .subscribe((orders: Order[]) => {
        this.orders = this.abstractGroupedOrdersConverter.convertToGroupedOrders(orders);
      }
      )
  }

  public get hasOrders(): boolean {
    return this.orders && this.orders.length > 0;
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
