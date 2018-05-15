import { Observable } from 'rxjs/Observable';
import { User } from '../model/user';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { UserViewModelConverter } from './converter/user.view-model-converter';
import 'rxjs/add/operator/map';

const USERS_PATH = 'users';
const URL = `${environment.apiUrl}/${USERS_PATH}`;

@Injectable()
export class UserRepository {

  constructor(private httpClient: HttpClient) {
  }

  public findById(userId: number): Observable<User> {
    return this.httpClient.get(`${URL}/${userId}`).map((user: any) => UserViewModelConverter.fromDTO(user));
  }
}
