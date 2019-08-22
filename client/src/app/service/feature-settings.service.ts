import { FeatureSettingsRepository } from './../repository/feature-settings.repository';
import { Observable } from 'rxjs/Observable';
import { Injectable } from '@angular/core';
import { FeatureSettings } from '../model/feature-settings';

@Injectable()
export class FeatureSettingsService {

    constructor(private repository: FeatureSettingsRepository) {
    }

    public getFeatureSettings(): Observable<FeatureSettings> {
        return this.repository.getFeatureSettings();
    }
}


