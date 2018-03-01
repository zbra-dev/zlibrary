import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
//import { BsModalService } from 'ngx-bootstrap';
import { ReservationService } from "../../../service/reservation.service";
import { Reservation } from "../../../model/reservation";
//import { ReservationStatus } from "../../../model/reservationStatus";
//import { LoaderMediator } from "../../mediators/loader.mediator";
//import { ToastMediator } from "../../mediators/toast.mediator";
import { AuthService } from "../../../service/auth.service";
//import { Book } from "../../../model/book";
import { User } from "../../../model/user";
//import { CoverImageService } from '../../../service/coverImage.service';
import index from "@angular/cli/lib/cli";
//import { Publisher } from "../../../model/publisher";
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';
//import { BookService } from '../../../service/book.service';
//import {forEach} from "@angular/router/src/utils/collection";

@Component({
  selector: 'zli-reservation-history',
  templateUrl: './reservation-history.component.html',
  styleUrls: ['./reservation-history.component.scss'],
  encapsulation: ViewEncapsulation.Emulated
})
export class ReservationHistoryComponent implements OnInit {
  constructor(private authService: AuthService, private reservationService: ReservationService) {
  }

  @Input() modalControl: BsModalRef;
  public user: User;
  public reservations: Reservation[];


  ngOnInit() {
      this.user = this.authService.getLoggedUser();
      this.reservationService.findByUserId(this.user)
          .subscribe((reservations: Reservation[]) => {
              this.reservations = reservations;
          });
  }

  public close(): void {
      this.modalControl.hide();
  }
}
