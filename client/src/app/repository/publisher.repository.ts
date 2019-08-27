import { Observable } from 'rxjs/Observable';
import { Publisher } from '../model/publisher';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import 'rxjs/add/operator/map';
import { PublisherViewModelConverter } from './converter/publisher.view-model-converter';

const PUBLISHERS_PATH = 'publishers';
const URL = `${environment.apiUrl}/${PUBLISHERS_PATH}`;

@Injectable()
export class PublisherRepository {

    constructor(private httpClient: HttpClient) {
    }

    public search(name: string): Observable<Publisher[]> {
        return this.httpClient.get(`${URL}/${name}`).map((data: any) => data.map(p => PublisherViewModelConverter.fromDTO(p)));
    }

    public save(publisher: Publisher): Observable<Publisher> {
        return this.httpClient.post(URL, publisher).map((data: any) => data);
    }
}
