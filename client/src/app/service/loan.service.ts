import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/switchMap';
import { Injectable } from '@angular/core';
import { LoanRepository } from '../repository/loan.repository';
import { Loan } from '../model/loan';

@Injectable()
export class LoanService {

    constructor(private repository: LoanRepository) {
    }

    public updateLoanStatusToReturned(loan: Loan) {
       return this.repository.updateLoanStatusToReturned(loan);
    }
}