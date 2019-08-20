import { FeatureSettingsResources } from "../resources/feature-settings.resources";
import { FeatureSettings } from "../../model/feature-settings";

export class FeatureSettingsViewModelConverter {
    public static fromDto(dto: FeatureSettingsResources): FeatureSettings {
        let featureSettings = new FeatureSettings();
        featureSettings.allowCoverImage = dto.allowCoverImage;
        return featureSettings;
    }
}
