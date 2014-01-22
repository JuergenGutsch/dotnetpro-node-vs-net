using System;
using System.Web.Http;
using BooksCollector.Core;
using Gos.SimpleObjectStore;

namespace BooksCollector.Web.Controllers
{
    public class BooksController : ApiController
    {
        // POST api/books
        public Result Post(Book book)
        {
            using (var store = ObjectStore.GetInstance<Book>())
            {
                try
                {
                    if (BookExists(store, book))
                    {
                        // tell us if the book already exists
                        return NewDuplicateResult(book);
                    }

                    store.SaveOnSubmit(book);

                    return NewSuccessResult(book);
                }
                catch (Exception ex)
                {
                    // tell us if something unexpected happened
                    return NewFailResult(book, ex);
                }
            }
        }

        private bool BookExists(IObjectStore<Book> store, Book book)
        {
            var existing = store.LoadSingle(x => x.Equals(book));
            return existing != null;
        }

        private Result NewSuccessResult(Book book)
        {
            return new Result
            {
                Book = book,
                Success = true,
                ErrorMessage = String.Empty
            };
        }

        private Result NewDuplicateResult(Book book)
        {
            return new Result
            {
                Book = book,
                Success = false,
                ErrorCode = 1,
                ErrorMessage = String.Format("Book \"{0}\" allready exists", book.Title)
            };
        }

        private Result NewFailResult(Book book, Exception ex)
        {
            return new Result
            {
                Book = book,
                Success = false,
                ErrorCode = 2,
                ErrorMessage = ex.Message
            };
        }
    }
}
