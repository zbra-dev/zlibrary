using ZLibrary.API;

namespace ZLibrary.Web.Validators
{
    public class ValidationContext
    {
        public IBookFacade BookFacade { get; private set; }
        public IAuthorService AuthorService { get; private set; }
        public IPublisherService PublisherService { get; private set; }

        public ValidationContext(IBookFacade bookFacade, IAuthorService authorService, IPublisherService publisherService){
            BookFacade = bookFacade;
            AuthorService = authorService;
            PublisherService = publisherService;
        }
    }
}