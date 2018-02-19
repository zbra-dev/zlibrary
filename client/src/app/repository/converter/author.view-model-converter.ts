import {Author} from '../../model/author';

export class AuthorViewModelConverter {
    // TODO: Read missing fields
    public static fromDTO(dto: any): Author {
        const author = new Author(dto.id, dto.name);
        return author;
    }
}
