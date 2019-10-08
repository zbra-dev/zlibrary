import { Order } from "../../model/order";
import { GroupedOrder } from "../../model/grouped-order";
import { Reservation } from "../../model/reservation";
import { Book } from "../../model/book";
import { User } from "../../model/user";
import { Dictionary } from "../../model/dictionary";

export class GroupedOrdersConverter {
    public convertToGroupedOrders(orders: Order[]): GroupedOrder[] {

        const dictionary: Dictionary<Array<Order>> = {};

        for (const order of orders) {
            const bookId = order.book.id;
            let value = dictionary[bookId];
            if (value == null) {
                value = new Array<Order>();
                dictionary[bookId] = value;
            }
            value.push(order);
        }

        let groupedOrders: Array<GroupedOrder> = new Array<GroupedOrder>();

        for (let key in dictionary) {
            let book: Book;
            let reservations = new Array<Reservation>();
            let users = new Array<User>();
            let groupedOrder: GroupedOrder;

            let orders = dictionary[key];

            for (let order of orders) {
                book = order.book;
                users.push(order.user);
                reservations.push(order.reservation);
            }

            groupedOrder = new GroupedOrder(reservations, book, users);
            groupedOrders.push(groupedOrder);
        }

        return groupedOrders;
    }
}