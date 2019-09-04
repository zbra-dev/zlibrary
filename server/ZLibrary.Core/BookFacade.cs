using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZLibrary.API;
using ZLibrary.Model;

namespace ZLibrary.Core
{
    public class BookFacade : IBookFacade
    {
        private readonly IBookService bookService;
        private readonly IImageService imageService;

        public BookFacade(IBookService bookService, IImageService imageService)
        {
            this.bookService = bookService;
            this.imageService = imageService;
        }

        public async Task<IList<Book>> FindAll()
        {
            return await bookService.FindAll();
        }
        public async Task<Book> FindById(long id)
        {
            return await bookService.FindById(id);
        }

        public async Task<Book> FindByCoverImageKey(Guid key)
        {
            return await bookService.FindByCoverImageKey(key);
        }

        public async Task Delete(long id)
        {
            await bookService.Delete(id);
        }

        public async Task<Book> Save(Book book, string imagePath)
        {
            imageService.SaveImage(book.CoverImageKey, imagePath);
            var bookSaved = await bookService.Save(book);
            return bookSaved;
        }

        public async Task<IList<Book>> FindBy(BookFilter bookSearchParameter)
        {
            return await bookService.FindBy(bookSearchParameter);
        }
    }
}