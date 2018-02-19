import { Publisher } from "../../model/publisher";


export class PublisherViewModelConverter {
    // TODO: Read missing fields
    public static fromDTO(dto: any): Publisher {
        const publisher = new Publisher(dto.id, dto.name);
        return publisher;
    }
}
