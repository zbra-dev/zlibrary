import { Publisher } from './publisher';
import { Author } from './author';
import { Isbn } from './isbn';
import { Reservation } from './reservation';
import { LoanStatus } from './loan-status';
import { User } from './user';

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

    public hasBookReservation(user: User): boolean {
        if (this.reservations.length > 0) {
            const userReservations = this.reservations.filter(r => r.userId === user.id);
            return userReservations.length > 0
                && userReservations.some(r => r.reservationReason.isApproved || r.loanStatus !== LoanStatus.returned);
        }
        return false;
    }
}
