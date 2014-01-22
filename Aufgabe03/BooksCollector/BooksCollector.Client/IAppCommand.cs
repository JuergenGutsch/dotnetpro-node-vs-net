using System.Windows.Input;

namespace BooksCollector.Client
{
    public interface IAppCommand : ICommand
    {
        void RaiseCanExecuteChanged();
    }
}