import { Publisher } from '../../model/publisher';


export class PublisherViewModelConverter {

    public static fromDTO(dto: any): Publisher {
        const publisher = new Publisher(dto.id, dto.name);
        return publisher;
    }
}
