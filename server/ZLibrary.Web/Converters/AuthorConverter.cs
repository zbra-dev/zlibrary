using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class AuthorConverter : IConverter<Author, AuthorDTO>
    {
        public AuthorDTO ConvertFromModel(Author model)
        {
            return new AuthorDTO
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public Author ConvertToModel(AuthorDTO dto)
        {
            return new Author
            {
                Id = dto.Id ?? 0,
                Name = dto.Name
            };
        }
    }
}
