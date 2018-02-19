using System.Collections.Generic;
using System.Linq;
using ZLibrary.Model;
using ZLibrary.Web.Controllers.Items;

namespace ZLibrary.Web.Extensions
{
    public static class PublisherExtensions
    {
        public static PublisherDTO ToPublisherViewItem(this Publisher publisher)
        {
            return new PublisherDTO()
            {
                Id = publisher.Id,
                Name = publisher.Name
            };
        }

        public static IEnumerable<PublisherDTO> ToPublisherViewItems(this IEnumerable<Publisher> publishers)
        {
            return publishers.Select(p => p.ToPublisherViewItem()).ToArray();
        }
    }
}