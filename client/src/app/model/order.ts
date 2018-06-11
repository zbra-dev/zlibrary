import { Reservation } from './reservation';
import { User } from './user';
import { Book } from './book';

export class Order {
    constructor(public reservation: Reservation,
                public book: Book, 
                public user: User) {
    }
}
