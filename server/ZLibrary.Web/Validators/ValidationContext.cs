using ZLibrary.API;

namespace ZLibrary.Web.Validators
{
    public class ValidationContext
    {
        public IBookService BookService { get; private set; }
        public IAuthorService AuthorService { get; private set; }
        public IPublisherService PublisherService { get; private set; }
    }
}