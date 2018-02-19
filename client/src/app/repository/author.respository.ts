import {Observable} from 'rxjs/Observable';
import {Author} from '../model/author';
import 'rxjs/add/observable/of';
import {Injectable} from '@angular/core';
import {HttpClient} from '@angular/common/http';
import {environment} from '../../environments/environment';
import {AuthorViewModelConverter} from './converter/author.view-model-converter';
import 'rxjs/add/operator/map';

const AUTHORS_PATH = 'authors/';

@Injectable()
export class AuthorRepository {
    constructor(private httpClient: HttpClient) {
    }
    url = `${environment.apiUrl}/${AUTHORS_PATH}`;

    public search(name:string): Observable<Author[]> {
		return this.httpClient.get(this.url+name).map((data: any) => data.map(b => AuthorViewModelConverter.fromDTO(b)));
    }
}
