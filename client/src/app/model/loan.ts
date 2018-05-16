export class Loan {
    constructor(public id: number,
        public reservationId: number,
        public startDate: string,
        public endDate: string,
        public expirationDate: string,
        public isExpired: boolean,
        public isReturned: boolean,
        public canBorrow: boolean) {
    }
}
