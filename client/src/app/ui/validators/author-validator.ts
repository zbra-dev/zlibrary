import { AbstractControl } from '@angular/forms';
import { Utils } from '../components/utils/utils';

export class AuthorValidator {

    public static validateEmptyString() {
        return (c: AbstractControl) => {
            const value = String(c.value);
            if (Utils.isNullOrWhiteSpace(value)) {
                return {
                    emptyString: { 'value': value }
                };
            }
            return null;
        };
    }

    public static validateTypeahead() {
        return (c: AbstractControl) => {
            const value = c.value;
            if (!value || Array.isArray(value) && !value.length) {
                return {
                    emptySuggestions: { 'value:': value }
                };
            }
            return null;
        };
    }

}
