using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Converters
{
    public class PublisherConverter : AbstractConverter<Publisher, PublisherDto>
    {
        protected override PublisherDto NullSafeConvertFromModel(Publisher model)
        {
            return new PublisherDto
            {
                Id = model.Id,
                Name = model.Name
            };
        }

        protected override Publisher NullSafeConvertToModel(PublisherDto viewItem)
        {
            return new Publisher
            {
                Id = viewItem.Id ?? 0,
                Name = viewItem.Name
            };
        }
    }
}
