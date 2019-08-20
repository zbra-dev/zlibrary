import { FeatureSettings } from './../model/feature-settings';
import { FeatureSettingsViewModelConverter } from './converter/feature-settings.view-model-converter.converter';
import { FeatureSettingsResources } from './resources/feature-settings.resources';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/observable/of';
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import 'rxjs/add/operator/map';

const IMAGE_PATH = 'image';
const URL = `${environment.apiUrl}`;

@Injectable()
export class FeatureSettingsRepository {

    constructor(private httpClient: HttpClient) {
    }

    public getFeatureSettings(): Observable<FeatureSettings> {
        const url = `${URL}/settings/features`;
            return this.httpClient.get(url).map((data:FeatureSettingsResources) => FeatureSettingsViewModelConverter.fromDto(data));;
    }
}