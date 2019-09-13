import { User } from './../../../model/user';
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
import { Book } from '../../../model/book';
import { Reservation } from '../../../model/reservation';

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

  public orders: GroupedOrder[];
  public modalControl: BsModalRef;

  ngOnInit() {
    this.refreshList();
  }

  public refreshList() {
    this.reservationService.findOrdersByStatus(ReservationStatus.Approved)
      .subscribe((orders: Order[]) => {
        this.orders = this.convertToGroupedOrders(orders);
      }
      )
  }

  public get hasOrders(): boolean {
    return this.orders && this.orders.length > 0;
}

  private convertToGroupedOrders(orders: Order[]): GroupedOrder[] {

    let groups = orders.reduce((g: Array<Order[]>, order: Order) => {
      g[order.book.id] = g[order.book.id] || [];
      g[order.book.id].push(order);
      return g;
    }, []).filter(g => g.length > 0);

    let groupedOrders: GroupedOrder[] = new Array<GroupedOrder>();

    for (let i = 0; i < groups.length; i++) {
      let book: Book;
      let reservations = new Array<Reservation>();
      let users = new Array<User>();
      let groupedOrder: GroupedOrder;
      for (let j = 0; j < groups[i].length; j++) {
        book = groups[i][j].book;
        users.push(groups[i][j].user);
        reservations.push(groups[i][j].reservation);
      }
      groupedOrder = new GroupedOrder(reservations, book, users);
      groupedOrders.push(groupedOrder);
    }

    return groupedOrders;
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
