using System.Collections.Generic;
using System.Linq;
using Vtitbid.Romashov.Bss.V2.Domain.Entities;
using Vtitbid.Romashov.Bss.V2.Infrastructure.Persistence;

namespace Vtitbid.Romashov.Bss.V2.Presentation.ViewModels
{
    public class ProductWindowViewModel : ViewModelBase
    {
        private Product _selectedProduct;
        private List<Product> _products;
        private List<ProductType> _productTypes;
        private List<ProductMaterial> _productMaterials;
        private ProductType _selectedProductType;

        public Product SelectedProduct
        {
            get => _selectedProduct;
            set
            {
                Set(ref _selectedProduct, value, nameof(SelectedProduct));
                ProductMaterials = SelectedProduct.ProductMaterials.ToList();
            }
        }
        public ProductType SelectedProductType
        {
            get => _selectedProductType;
            set => Set(ref _selectedProductType, value, nameof(SelectedProductType));
        }
        public List<Product> Products
        {
            get => _products;
            set => Set(ref _products, value, nameof(Products));
        }
        public List<ProductType> ProductTypes
        {
            get => _productTypes;
            set => Set(ref _productTypes, value, nameof(ProductTypes));
        }
        public List<ProductMaterial> ProductMaterials
        {
            get => _productMaterials;
            set => Set(ref _productMaterials, value, nameof(ProductMaterials));
        }

        public ProductWindowViewModel()
        {

        }

        public void SaveChanges()
        {
            using (var context = new ApplicationDbContext())
            {
                // Переделать

                context.Products.Update(SelectedProduct);

                SelectedProduct.ProductTypeId = SelectedProductType.Id;

                context.SaveChanges();
            }
        }
    }
}
