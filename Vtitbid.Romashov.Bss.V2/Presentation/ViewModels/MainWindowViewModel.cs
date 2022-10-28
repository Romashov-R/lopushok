using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Vtitbid.Romashov.Bss.V2.Application.Commands;
using Vtitbid.Romashov.Bss.V2.Domain.Entities;
using Vtitbid.Romashov.Bss.V2.Infrastructure.Persistence;

namespace Vtitbid.Romashov.Bss.V2.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        private List<Product> _products;
        public List<Product> Products
        { 
            get => _products; 
            set => Set(ref _products, value, nameof(Products)); 
        }

        public MainWindowViewModel()
        {
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                Products = context.Products
                    .Include(p => p.ProductMaterials)
                    .ThenInclude(pm => pm.Material)
                    .Include(p => p.ProductType)
                    .ToList();
            }
        }
    }
}
