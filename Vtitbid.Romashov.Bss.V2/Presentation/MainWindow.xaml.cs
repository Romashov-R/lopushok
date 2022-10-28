using System.Windows;
using System.Windows.Controls;
using Vtitbid.Romashov.Bss.V2.Domain.Entities;
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
            ListView lw = (ListView)sender;
            if(lw.SelectedItem != null)
            {
                ProductWindow productWindow = new ProductWindow((Product)lw.SelectedItem);
                productWindow.ShowDialog();
            }
        }
    }
}
