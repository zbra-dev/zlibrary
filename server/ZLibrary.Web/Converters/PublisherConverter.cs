using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class PublisherConverter : IConverter<Publisher, PublisherDTO>
    {
        public PublisherDTO ConvertFromModel(Publisher model)
        {
            return new PublisherDTO
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        public Publisher ConvertToModel(PublisherDTO dto)
        {
            return new Publisher
            {
                Id = dto.Id ?? 0,
                Name = dto.Name
            };
        }
    }
}
