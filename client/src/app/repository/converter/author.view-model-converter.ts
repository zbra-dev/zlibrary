import { Author } from '../../model/author';

export class AuthorViewModelConverter {

    public static fromDTO(dto: any): Author {
        const author = new Author(dto.id, dto.name);
        return author;
    }
}
