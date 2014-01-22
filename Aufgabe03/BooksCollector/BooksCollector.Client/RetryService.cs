using System;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using BooksCollector.Core;
using Gos.SimpleObjectStore;
using Gos.SimpleObjectStore.Providers;

namespace BooksCollector.Client
{
    public class RetryService
    {
        public RetryService()
        {
            var path = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            path = Path.Combine(path, "Data");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }
            ObjectStore.DataProvider = new DataProvider(path);
            ObjectStore.FormatOutput = true;
        }

        public void Add(Book book)
        {
            using (var store = ObjectStore.GetInstance<Book>())
            {
                store.SaveOnSubmit(book);
            }
        }

        public async Task RetryPublishAsync()
        {
            using (var store = ObjectStore.GetInstance<Book>())
            {
                var books = store.LoadAll().ToList();
                foreach (var book in books)
                {
                    try
                    {
                        var result = await SubmitService.SubmitAsync(book);
                        // ErrorCode 1 is a duplicate entry
                        if (result.ErrorCode <= 1)
                        {
                            // delete item locally if it is successfuly saved on the server
                            store.DeleteOnSubmit(x => x.Equals(book));

                            ShowSuccessMessage(book);
                        }
                    }
                    finally
                    {
                    }
                }

                if (store.LoadAll().Any())
                {
                    var t = new Thread(() =>
                    {
                        Thread.Sleep(1000 * 30);
                        RetryPublishAsync();
                    });
                }
            }
        }

        private static void ShowSuccessMessage(Book book)
        {
            var message = String.Format("\"{0}\" successfully saved.",
                book.Title);
            Toaster.ShowToast("New Book saved",
                message);
        }

        public bool Exists(Book book)
        {
            using (var store = ObjectStore.GetInstance<Book>())
            {
                return store.LoadSingle(x => x.Equals(book)) != null;
            }
        }
    }
}