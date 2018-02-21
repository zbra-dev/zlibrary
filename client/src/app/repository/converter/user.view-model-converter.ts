import { User } from '../../model/user';

export class UserViewModelConverter {

    public static fromDTO(dto: any): User {
        const user = new User(dto.Id, dto.Name, dto.Email, dto.IsAdministrator, dto.AccessToken, dto.UserAvatarUrl);
        return user;
    }
}
