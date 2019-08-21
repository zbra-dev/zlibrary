import { Author } from '../../model/author';

export class AuthorViewModelConverter {

    public static fromDTO(dto: any): Author {
        return new Author(dto.id, dto.name);
    }
}
