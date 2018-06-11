import { BookViewModelConverter } from './book.view-model-converter';
import { Order } from '../../model/order';
import { ReservationViewModelConverter } from './reservation.view-model-converter';
import { UserViewModelConverter } from './user.view-model-converter';

export class OrderViewModelConverter {
    public static fromDTO(dto: any): Order {
        const order = new Order(
            ReservationViewModelConverter.fromDTO(dto.reservation), 
            BookViewModelConverter.fromDTO(dto.book), 
            UserViewModelConverter.fromDTO(dto.user)
        );
        return order;
    }
}
