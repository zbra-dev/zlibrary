import { Observable } from 'rxjs/Observable';
import { Reservation } from '../model/reservation';
import { Book } from '../model/book';
import { User } from '../model/user';
import { Order } from '../model/order';
import { ReservationResquestDTO } from './dto/reservationRequestDTO';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { ReservationViewModelConverter } from './converter/reservation.view-model-converter';
import { OrderViewModelConverter } from './converter/order.view-model-converter';
import 'rxjs/add/operator/map';
import { HttpErrorResponse } from '@angular/common/http/src/response';
import { ReservationStatus } from '../model/reservation-status';

const RESERVATIONS_PATH = 'reservations';
const URL = `${environment.apiUrl}/${RESERVATIONS_PATH}`;

@Injectable()
export class ReservationRepository {

  constructor(private httpClient: HttpClient) {
  }

  public findByUserId(user: User): Observable<Reservation[]> {
    const findByUserIdUrl = `${URL}/user/${user.id}`;
    return this.httpClient.get(findByUserIdUrl).map((data: any) => data.map(r => ReservationViewModelConverter.fromDTO(r)));
  }

  public order(user: User, book: Book): Observable<Reservation> {
    const orderUrl = `${URL}/order`;
    const dto = new ReservationResquestDTO(user.id, book.id);
    const json = JSON.stringify(dto);

    return this.httpClient.post(orderUrl, json, {
      headers: new HttpHeaders().set('Content-Type', 'application/json')
    }).map((data: any) => ReservationViewModelConverter.fromDTO(data));
  }

  public findByStatus(status: ReservationStatus): Observable<Reservation[]> {
    console.log('ReservationStatus[status] ===> ' + ReservationStatus[status]);
    const url = `${URL}/status/${ReservationStatus[status]}`;
    return this.httpClient.get(url).map((data: any) => data.map(r => ReservationViewModelConverter.fromDTO(r)));
  }

  public findOrderByStatus(status: ReservationStatus): Observable<Order[]> {
    const url = `${URL}/orders/${ReservationStatus[status]}`;
    return this.httpClient.get(url).map((data: any) => data.map(r => OrderViewModelConverter.fromDTO(r)));
  }

  public approve(reservationId: number) {
    return this.httpClient.post(`${URL}/approved/${reservationId}`, ' ', {
      headers: new HttpHeaders().set('Content-Type', 'application/json'),
      responseType: 'text' 
   });
  }

  public reject(reservationId: number) {
    const orderUrl = `${URL}/rejected`;
    const dto = { id: reservationId, description: '' }
    const json = JSON.stringify(dto);
    return this.httpClient.post(orderUrl, json, { 
      headers: new HttpHeaders().set('Content-Type', 'application/json'), 
      responseType: 'text' 
   });
    
  }
}
