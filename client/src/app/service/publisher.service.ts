import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import 'rxjs/add/operator/switchMap';
import { Publisher } from '../model/publisher';
import { Injectable } from '@angular/core';
import { PublisherRepository } from '../repository/publisher.repository';

@Injectable()
export class PublisherService {

    constructor(private repository: PublisherRepository) {
    }

    public search(keyword: string): Observable<Publisher[]> {
        return this.repository.search(keyword);
    }
}
