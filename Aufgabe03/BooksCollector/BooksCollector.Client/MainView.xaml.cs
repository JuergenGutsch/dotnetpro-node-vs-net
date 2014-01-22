using System.Windows;

namespace BooksCollector.Client
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainView : Window
    {
        public MainView()
        {
            InitializeComponent();
        }

        private void CloseButtonClick(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        public void Connect(int connectionId, object target)
        {
            throw new System.NotImplementedException();
        }
    }
}
