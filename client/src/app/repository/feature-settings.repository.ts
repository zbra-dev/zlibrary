import { FeatureSettings } from './../model/feature-settings';
import { FeatureSettingsResources } from './resources/feature-settings.resources';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import 'rxjs/add/operator/map';
import { AbstractRepository } from './abstract.respository';

const URL = `${environment.apiUrl}`;

@Injectable()
export class FeatureSettingsRepository extends AbstractRepository {

    constructor(private httpClient: HttpClient) {
        super();
    }

    public getFeatureSettings(): Observable<FeatureSettings> {
        const url = `${URL}/settings/features`;
            return this.httpClient.get(url).map((data:FeatureSettingsResources) => this.fromDto(data));;
    }

    private fromDto(dto: FeatureSettingsResources): FeatureSettings {
        return this.copy(dto, new FeatureSettings());
    }
}