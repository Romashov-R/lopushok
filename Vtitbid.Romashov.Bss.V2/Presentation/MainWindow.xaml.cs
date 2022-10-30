using System.Windows;
using Vtitbid.Romashov.Bss.V2.Presentation.ViewModels;

namespace Vtitbid.Romashov.Bss.V2.Presentation
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainWindowViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            _viewModel = (MainWindowViewModel)DataContext;
        }

        private void ListView_MouseDoubleClick(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if(_viewModel.SelectedProduct != null)
            {
                var productWindow = new ProductWindow(_viewModel.SelectedProduct, _viewModel.DisplayingProducts, _viewModel.ProductTypes);
                productWindow.ShowDialog();
            }
        }
    }
}
