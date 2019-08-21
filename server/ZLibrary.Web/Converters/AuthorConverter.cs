using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class AuthorConverter : AbstractConverter<Author, AuthorDto>
    {
        protected override AuthorDto NullSafeConvertFromModel(Author model)
        {
            return new AuthorDto
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        protected override Author NullSafeConvertToModel(AuthorDto viewItem)
        {
            return new Author
            {
                Id = viewItem.Id ?? 0,
                Name = viewItem.Name
            };
        }
    }
}
