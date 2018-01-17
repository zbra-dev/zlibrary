import { Observable } from 'rxjs/Observable';
import { Reservation } from '../model/reservation';
import { Book } from '../model/book';
import { User } from '../model/user';
import { ReservationResquestDTO } from './dto/reservationRequestDTO';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ReservationViewModelConverter } from './converter/reservation.view-model-converter';
import 'rxjs/add/operator/map';
import { HttpErrorResponse } from '@angular/common/http/src/response';

const RESERVATIONS_PATH = 'reservations/';
const url = `${environment.apiUrl}/${RESERVATIONS_PATH}`;

@Injectable()
export class ReservationRepository {
  constructor(private httpClient: HttpClient) {
  }

  public findByUserId(user: User): Observable<Reservation[]> {
    var findByUserIdUrl = url + `user/${user.id}`;
    console.log("Find By User Id " + findByUserIdUrl);
    return this.httpClient.get(findByUserIdUrl).map((data: any) => data.map(r => ReservationViewModelConverter.fromDTO(r)));
  }

  public order(user: User, book: Book): Observable<Reservation> {
    var orderUrl = url + `order`;
    console.log("Order URL " + orderUrl);
    var dto = new ReservationResquestDTO(user.id, book.id);
    var json = JSON.stringify(dto);
    var headers = new Headers({ 'Content-Type': 'application/json' });

    return this.httpClient.post(orderUrl, json, {
      headers: new HttpHeaders().set('Content-Type', 'application/json')
    }).map((data: any) => ReservationViewModelConverter.fromDTO(data));
  }
}
