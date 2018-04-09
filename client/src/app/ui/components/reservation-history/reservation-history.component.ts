import { Component, OnInit, Input, ViewEncapsulation } from '@angular/core';
import { ReservationService } from "../../../service/reservation.service";
import { Reservation } from "../../../model/reservation";
import { AuthService } from "../../../service/auth.service";
import { User } from "../../../model/user";
import index from "@angular/cli/lib/cli";
import { BsModalRef } from 'ngx-bootstrap/modal/bs-modal-ref.service';

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
  public reservationHistoryType: ReservationHistoryType;


  ngOnInit() {
      this.user = this.authService.getLoggedUser();
      this.reservationService.findByUserId(this.user)
          .subscribe((reservations: Reservation[]) => {
              this.reservations = reservations;
          });
  }

  public isLoaned(): boolean{
      return this.reservationHistoryType == ReservationHistoryType.Loaned;
  }

  public isWaiting(): boolean{
     return this.reservationHistoryType == ReservationHistoryType.Waiting;
  }

  public close(): void {
      this.modalControl.hide();
  }
}

export enum ReservationHistoryType{
    Loaned,
    Waiting
}
