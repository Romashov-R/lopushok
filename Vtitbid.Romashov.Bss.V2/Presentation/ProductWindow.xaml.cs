using System.Collections.Generic;
using System.Windows;
using Vtitbid.Romashov.Bss.V2.Domain.Entities;
using Vtitbid.Romashov.Bss.V2.Presentation.ViewModels;

namespace Vtitbid.Romashov.Bss.V2.Presentation
{
    /// <summary>
    /// Логика взаимодействия для ProductWindow.xaml
    /// </summary>
    public partial class ProductWindow : Window
    {
        private ProductWindowViewModel _viewModel;

        public ProductWindow(Product selectedProduct, List<Product> products, List<ProductType> productTypes)
        {
            InitializeComponent();
            _viewModel = (ProductWindowViewModel)DataContext;
            
            _viewModel.SelectedProduct = selectedProduct;
            _viewModel.Products = products;
            _viewModel.ProductTypes = productTypes;
            _viewModel.SelectedProductType = selectedProduct.ProductType;
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.SaveChanges();
            Close();
        }
    }
}
