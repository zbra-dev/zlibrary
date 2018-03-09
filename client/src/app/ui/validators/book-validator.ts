import { AbstractControl } from '@angular/forms';
import { Utils } from '../components/utils/utils';

const INTEGER_REGEX = new RegExp(`^\\d+$`);

export class BookValidator {

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

    public static validateIsbn() {
        return (c: AbstractControl) => {
            if (!c.value) {
                return {
                    invalidIsbn: { 'noValue': c.value }
                };
            }
            const value = c.value.toString();
            const isbnLength = 13;

            if (value.length !== isbnLength) {
                return {
                    invalidIsbn: { 'minLength': isbnLength, 'actualLenght': value.length }
                };
            }
            const weightType1 = 1;
            const weightType2 = 3;
            const productor = new Array(isbnLength);
            for (let i = 0; i < isbnLength; i++) {
                if (i % 2 === 0) {
                    productor[i] = value[i] * weightType1;
                } else {
                    productor[i] = value[i] * weightType2;
                }
            }
            if (!(productor.reduce((a, b) => a + b) % 10 === 0)) {
                return {
                    invalidIsbn: { inputValue: value }
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

    public static validatePublicationYear() {
        return (c: AbstractControl) => {
            const value = c.value;
            if (!value && value !== 0) {
                return {
                    emptyField: { 'actualValue': value }
                };
            }
            if (!INTEGER_REGEX.test(value) || value < 0) {
                return {
                    notValide: { 'value': value }
                };
            }
            return null;
        };
    }

    public static validateNumberOfCopies(minValue: number, maxValue: number) {
        return (c: AbstractControl) => {
            const value = c.value;
            if (!value && value !== 0) {
                return {
                    emptyField: { 'actualValue': value }
                };
            }
            if (value === 0 || !INTEGER_REGEX.test(value) || !!value && (minValue > value || maxValue < value)) {
                return {
                    outOfRange: { 'min': minValue, 'max': maxValue, 'actual': value }
                };
            }
            return null;
        };
    }
}
