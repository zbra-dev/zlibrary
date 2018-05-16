import { Loan } from "../../model/loan";

export class LoanViewModelConverter {
    public static fromDTO(dto: any): Loan {
        let loan: Loan;
        if (!!dto) {
            loan = new Loan(dto.id,
                dto.reservationId,
                dto.startDate,
                dto.endDate,
                dto.expirationDate,
                dto.isExpired,
                dto.isReturned,
                dto.canBorrow);
        }
        
        return loan;
    }
}
