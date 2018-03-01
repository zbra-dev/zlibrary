import { Publisher } from './publisher';
import { Author } from './author';
import { Isbn } from './isbn';
import { Reservation } from './reservation';

export class Book {

    public id: number;
    public title: string;
    public publisher: Publisher;
    public authors: Author[];
    public isbn: number;
    public synopsis: string;
    public publicationYear: number;
    public numberOfCopies: number;
    public coverImageKey: string;
    public created: Date;
    public reservations: Reservation[];

    public get isAvailable(): boolean {
        return this.numberOfCopies > 0 && this.reservations.filter(r => r.reservationReason.isApproved).length < this.numberOfCopies;
    }

    constructor() {
    }
}


//constructor(id: number, title: string, publisher: Publisher, authors: Author[],  isbn: Isbn, synopsis: string, publicationYear: number, numberOfCopies: number, coverImageKey: string,  created: Date) {
