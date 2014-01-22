using System;
using BooksCollector.Core;

namespace BooksCollector.Client
{
    public class InsertNewBookCommand : IAppCommand
    {
        private readonly MainViewModel _mainViewModel;

        public InsertNewBookCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return !String.IsNullOrWhiteSpace(_mainViewModel.Author)
                   && !String.IsNullOrWhiteSpace(_mainViewModel.Title)
                   && !String.IsNullOrWhiteSpace(_mainViewModel.Isbn);
        }

        public async void Execute(object parameter)
        {
            var book = new Book
            {
                Author = _mainViewModel.Author.Trim(),
                Title = _mainViewModel.Title.Trim(),
                Isbn = _mainViewModel.Isbn.Trim()
            };

            try
            {
                var result = await SubmitService.SubmitAsync(book);
                if (result.Success)
                {
                    ShowSuccessMessage(book);
                    await _mainViewModel.RetryService.RetryPublishAsync();
                }
                else
                {
                    // an exception occurred on the server; 
                    // ErrorCode 1 means duplicate
                    // anithing else means we have to retry
                    if (result.ErrorCode > 1)
                    {
                        throw new Exception(
                            string.Format("Server Error: {0}({1})",
                                result.ErrorMessage,
                                result.ErrorCode));
                    }

                    throw new BookExistsException();
                }
            }
            catch (BookExistsException)
            {
                ShowBookExistsMessage(book);
            }
            catch (Exception)
            {
                // catch all exceptions to add the book to 
                // the retry service in case of an error
                if (_mainViewModel.RetryService.Exists(book))
                {
                    ShowBookExistsMessage(book);
                }
                else
                {
                    _mainViewModel.RetryService.Add(book);

                    ShowBooksSavedLocallyMessage(book);
                }
            }

            _mainViewModel.ClearForm.Execute(null);


        }

        private static void ShowSuccessMessage(Book book)
        {
            var message = String.Format("\"{0}\" successfully saved.",
                book.Title);
            Toaster.ShowToast("New Book saved",
                message);
        }           

        private static void ShowBooksSavedLocallyMessage(Book book)
        {
            var message = String.Format("\"{0}\" saved locally.",
                book.Title);
            Toaster.ShowToast("New Book locally saved",
                message);
        }

        private static void ShowBookExistsMessage(Book book)
        {
            var message = String.Format("\"{0}\" allready exists.",
                book.Title);
            Toaster.ShowToast("New Book exists!",
                message);
        }

        public event EventHandler CanExecuteChanged;
        public void RaiseCanExecuteChanged()
        {
            var handler = CanExecuteChanged;
            if (handler != null)
            {
                handler(this, EventArgs.Empty);
            }
        }
    }

    public class BookExistsException : Exception
    {
    }
}