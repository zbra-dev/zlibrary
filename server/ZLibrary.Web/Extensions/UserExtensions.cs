using System.Collections.Generic;
using System.Linq;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Extensions
{
    public static class UserExtensions
    {
        public static UserDTO ToUserViewItem(this User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                UserAvatarUrl = user.UserAvatarUrl,
                Email = user.Email,
                IsAdministrator = user.IsAdministrator,
                AccessToken = user.AccessToken
            };
        }

        public static UserDTO ToLiteUserViewItem(this User user)
        {
            return new UserDTO()
            {
                Id = user.Id,
                Name = user.Name,
                UserAvatarUrl = user.UserAvatarUrl,
                Email = user.Email
            };
        }

        public static IEnumerable<UserDTO> ToUserViewItems(this IEnumerable<User> users)
        {
            return users.Select(a => a.ToUserViewItem()).ToArray();
        }
    }
}