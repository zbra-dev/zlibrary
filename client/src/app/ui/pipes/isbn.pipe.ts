import { Pipe, PipeTransform } from '@angular/core';

@Pipe({
    name: 'isbn'
})
export class IsbnPipe implements PipeTransform {

    transform(value: string, args?: any): String {
        if (value.length === 13) {
            let formattedString = '';
            for (let i = 0; i < value.length; i++) {
                formattedString += value[i];
                if (i === 2 || i === 3 || i === 8 || i === 11) {
                    formattedString += '-';
                }
            }
            return formattedString;
        }
        return value;
    }
}
