using System;

namespace BooksCollector.Client
{
    public class ClearFormCommand : IAppCommand
    {
        private readonly MainViewModel _mainViewModel;

        public ClearFormCommand(MainViewModel mainViewModel)
        {
            _mainViewModel = mainViewModel;
        }

        public bool CanExecute(object parameter)
        {
            return !String.IsNullOrWhiteSpace(_mainViewModel.Author)
                   || !String.IsNullOrWhiteSpace(_mainViewModel.Title)
                   || !String.IsNullOrWhiteSpace(_mainViewModel.Isbn);
        }

        public void Execute(object parameter)
        {
            _mainViewModel.Author = string.Empty;
            _mainViewModel.Title = string.Empty;
            _mainViewModel.Isbn = string.Empty;
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
}