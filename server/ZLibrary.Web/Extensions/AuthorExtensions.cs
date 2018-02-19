using System.Collections.Generic;
using System.Linq;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Extensions
{
    public static class AuthorExtensions
    {
        public static AuthorDTO ToAuthorViewItem(this Author author)
        {
            return new AuthorDTO()
            {
                Id = author.Id,
                Name = author.Name
            };
        }

        public static IEnumerable<AuthorDTO> ToAuthorViewItems(this IEnumerable<Author> authors)
        {
            return authors.Select(a => a.ToAuthorViewItem()).ToArray();
        }
    }
}