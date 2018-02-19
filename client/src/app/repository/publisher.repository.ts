import { Observable } from 'rxjs/Observable';
import { Publisher } from '../model/publisher';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import 'rxjs/add/operator/map';
import { PublisherViewModelConverter } from './converter/publisher.view-model-converter';

const PUBLISHERS_PATH = 'publishers/';

@Injectable()
export class PublisherRepository {
    constructor(private httpClient: HttpClient) {
    }
    url = `${environment.apiUrl}/${PUBLISHERS_PATH}`;

    public search(name: string): Observable<Publisher[]> {
        return this.httpClient.get(this.url + name).map((data: any) => data.map(p => PublisherViewModelConverter.fromDTO(p)));
    }
}
