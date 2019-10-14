import { Component, OnInit, Input, Output, EventEmitter } from '@angular/core';
import { Reservation } from '../../../model/reservation';
import { Loan } from '../../../model/loan';
import { User } from '../../../model/user';
import { UserService } from '../../../service/user.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { LoanService } from '../../../service/loan.service';

@Component({
  selector: 'loan',
  templateUrl: './loan.component.html',
  styleUrls: ['./loan.component.scss']
})
export class LoanComponent implements OnInit {

  @Input()
  public reservation: Reservation;
  public loan: Loan;
  public user: User;

  @Output()
  refreshReservations = new EventEmitter<void>();

  constructor(private loaderMediator: LoaderMediator, private toastMediator: ToastMediator, private userService: UserService, private loanService: LoanService) { }

  ngOnInit() {
    this.loan = this.reservation.loan
    this.findUser();
  }


  public findUser() {
    if (!!this.reservation) {
      this.loaderMediator.execute(
        this.userService.findById(this.reservation.userId).subscribe(
          user => {
            this.user = user;
          },
          error => {
            this.toastMediator.show(`Usuario não encontrado.`);
          }
        )
      );
    }
  }

  public returnBook(){
    this.loaderMediator.execute(
      this.loanService.updateLoanStatusToReturned(this.loan)
      .subscribe(
        respose => {
          this.refreshReservations.emit();
        },
        error => {
          this.toastMediator.show(`Erro ao devolver o livro: ${error}`);
        }
      )
    );
  }
}
