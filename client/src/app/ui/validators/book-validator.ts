import { AbstractControl } from '@angular/forms';
import { Utils } from '../components/utils/utils';
import { Book } from '../../model/book';

const INTEGER_REGEX = new RegExp(`^\\d+$`);
const IMAGE_TYPE = 'image/png';
const isbnModernLength = 13;
const isbnOldLength = 10;

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

    public static validateImageExtension(book: Book) {
        return (c: AbstractControl) => {
            const value = c.value;
            if ((!value || value.length === 0) && !book.id) {
                return { required: true };
            }
            if (!!value && value.type !== IMAGE_TYPE) {
                return {
                    extensionInvalid: {
                        'actual': value.type, expected: IMAGE_TYPE
                    }
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

            if (value.length !== isbnModernLength && value.length !== isbnOldLength) {
                return {
                    invalidIsbn: { 'minLength': isbnOldLength, 'actualLength': value.length }
                };
            }
            if (value.length == isbnModernLength) {
                return this.ValidateModernIsbn(value);
            }
            else if (value.length == isbnOldLength) {
                return this.ValidateOldIsbn(value);
            }
            return null;
        };
    }

    private static ValidateModernIsbn(value) {
        const evenWeight = 1;
        const oddWeight = 3;
        let sum = 0;
        for (let i = 0; i < isbnModernLength; i++) {
            if (i % 2 === 0) {
                sum += value[i] * evenWeight;
            } else {
                sum += value[i] * oddWeight;
            }
        }
        if (sum % 10 != 0) {
            return {
                invalidIsbn: { inputValue: value }
            };
        }
    }

    private static ValidateOldIsbn(value) {
        const maxWeight = 10;
        let factor = maxWeight;
        let sum = 0;
        for (let i = 0; i < isbnOldLength; i++, factor--) {
            sum += value[i] * factor;
        }
        if (sum % 11 != 0) {
            return {
                invalidIsbn: { inputValue: value }
            };
        }
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
