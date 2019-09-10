import { Reservation } from './reservation';
import { User } from './user';
import { Book } from './book';

export class GroupedOrder {
    constructor(public reservations: Reservation[],
                public book: Book, 
                public users: User[]) {
    }
}
