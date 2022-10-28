using System.Collections.Generic;
using System.Linq;
using Vtitbid.Romashov.Bss.V2.Domain.Entities;
using Vtitbid.Romashov.Bss.V2.Infrastructure.Persistence;

namespace Vtitbid.Romashov.Bss.V2.Presentation.ViewModels
{
    public class ProductWindowViewModel : ViewModelBase
    {
        private Product _product;

        public Product Product 
        {
            get => _product;
            set => Set(ref _product, value, nameof(Product));
        }

        public List<ProductType> ProductTypes { get; set; }

        public ProductWindowViewModel()
        {
            using(ApplicationDbContext context = new ApplicationDbContext())
            {
                ProductTypes = context.ProductTypes.ToList();
            }
        }
    }
}
