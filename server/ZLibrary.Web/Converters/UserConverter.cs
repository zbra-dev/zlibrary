using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class UserConverter : AbstractFromModelConverter<User, UserDto>
    {
        protected override UserDto NullSafeConvertFromModel(User model)
        {
            return new UserDto()
            {
                Id = model.Id,
                Name = model.Name,
                UserAvatarUrl = model.UserAvatarUrl,
                Email = model.Email,
                IsAdministrator = model.IsAdministrator,
                AccessToken = model.AccessToken
            };
        }
    }
}
