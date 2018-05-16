import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import 'rxjs/add/operator/map';
import { Loan } from '../model/loan';

const LOANS_PATH = 'loans';
const URL = `${environment.apiUrl}/${LOANS_PATH}`;

@Injectable()
export class LoanRepository {

  constructor(private httpClient: HttpClient) {
  }

  public updateLoanStatusToReturned(loan: Loan){
      console.log(`${URL}/returned/${loan.id}`);
    return this.httpClient.post(`${URL}/returned/${loan.id}`, ' ');
  }
}
