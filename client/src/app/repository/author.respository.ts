import { Observable } from 'rxjs/Observable';
import { Author } from '../model/author';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { AuthorViewModelConverter } from './converter/author.view-model-converter';
import 'rxjs/add/operator/map';

const AUTHORS_PATH = 'authors';
const URL = `${environment.apiUrl}/${AUTHORS_PATH}`;

@Injectable()
export class AuthorRepository {

  constructor(private httpClient: HttpClient) {
  }

  public search(name: string): Observable<Author[]> {
    return this.httpClient.get(`${URL}/${name}`).map((data: any) => data.map(b => AuthorViewModelConverter.fromDTO(b)));
  }

  public save(author: Author): Observable<Author> {
    return this.httpClient.post(URL, author ).map((data: any) => AuthorViewModelConverter.fromDTO(data));
  }
}
