using System.ComponentModel;
using System.Runtime.CompilerServices;
using BooksCollector.Client.Annotations;

namespace BooksCollector.Client
{
    public class MainViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        private string _author;
        public string Author
        {
            get { return _author; }
            set
            {
                if (value != _author)
                {
                    _author = value;
                    RaisePropertyChanged();
                    ClearForm.RaiseCanExecuteChanged();
                    InsertNewBook.RaiseCanExecuteChanged();
                }
            }
        }

        private string _title;
        public string Title
        {
            get { return _title; }
            set
            {
                if (value != _title)
                {
                    _title = value;
                    RaisePropertyChanged();
                    ClearForm.RaiseCanExecuteChanged();
                    InsertNewBook.RaiseCanExecuteChanged();
                }
            }
        }

        private string _isbn;
        public string Isbn
        {
            get { return _isbn; }
            set
            {
                if (value != _isbn)
                {
                    _isbn = value;
                    RaisePropertyChanged();
                    ClearForm.RaiseCanExecuteChanged();
                    InsertNewBook.RaiseCanExecuteChanged();
                }
            }
        }

        public MainViewModel()
        {
            ClearForm = new ClearFormCommand(this);
            InsertNewBook = new InsertNewBookCommand(this);
            RetryService = new RetryService();
        }

        public IAppCommand ClearForm { get; set; }
        public IAppCommand InsertNewBook { get; set; }
        public RetryService RetryService { get; set; }

        [NotifyPropertyChangedInvocator]
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null)
            {
                handler(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
