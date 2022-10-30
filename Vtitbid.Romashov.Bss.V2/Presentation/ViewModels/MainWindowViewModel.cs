using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Vtitbid.Romashov.Bss.V2.Domain.Entities;
using Vtitbid.Romashov.Bss.V2.Infrastructure.Persistence;

namespace Vtitbid.Romashov.Bss.V2.Presentation.ViewModels
{
    public class MainWindowViewModel : ViewModelBase
    {
        // Поле списка продуктов, где будут храниться все продукты
        private List<Product> _products;
        // Поле списка продуктов, которые мы будем выводить в ListView
        private List<Product> _displayingProducts;
        private List<ProductType> _productTypes;

        private List<string> _valuesToFilter;
        
        private Product _selectedProduct;

        private string _searchValue;
        private string _filterValue;
        private string _sortValue;

        public List<Product> DisplayingProducts
        {
            get => _displayingProducts;
            set => Set(ref _displayingProducts, value, nameof(DisplayingProducts));
        }
        public List<ProductType> ProductTypes
        {
            get => _productTypes;
            set => Set(ref _productTypes, value, nameof(ProductType));
        }

        // Значения для фильтрации
        public List<string> ValuesToFilter
        {
            get => _valuesToFilter;
            set => Set(ref _valuesToFilter, value, nameof(ValuesToFilter));
        }
        // Значения для сортировки
        public List<string> ValuesToSort => new List<string>
        {
            "Без сортировки", "По названию (возр)", "По названию (уб)", "По цене (возр)", "По цене (уб)"
        };
        
        public Product SelectedProduct
        {
            get => _selectedProduct;
            set => Set(ref _selectedProduct, value, nameof(SelectedProduct));
        }

        public string SearchValue
        {
            get => _searchValue;
            set
            {
                Set(ref _searchValue, value, nameof(SearchValue));
                DisplayProducts();
            }

            }
        public string FilterValue
        {
            get => _filterValue;
            set
            {
                Set(ref _filterValue, value, nameof(FilterValue));
                DisplayProducts();
            }
        }
        public string SortValue
        {
            get => _sortValue;
            set
            {
                Set(ref _sortValue, value, nameof(SortValue));
                DisplayProducts();
            }
        }

        public MainWindowViewModel()
        {
            // Заполняем список для фильтрации
            ValuesToFilter = new List<string>();
            ValuesToFilter.Add("Без фильтра");

            // Задаем начальные значения ComboBox
            FilterValue = ValuesToFilter[0];
            SortValue = ValuesToSort[0];

            // Получаем данные из базы данных
            using (ApplicationDbContext context = new ApplicationDbContext())
            {
                // Получаем данные о типах продуктов и сохраняем их в свойство ProductTypes
                ProductTypes = context.ProductTypes.ToList();
                
                // Для ComboBox необходимо привязать строковые значения.
                // Берем значения каждого элемента из свойства ProductTypes 
                // и добавляем их в свойство ValuesToFilter
                ProductTypes.ForEach(pt => ValuesToFilter.Add(pt.Title));

                // Получаем данные о всех продуктах вместе с данными о материалах
                // этих продуктов
                _products = context.Products
                    .Include(p => p.ProductMaterials)
                    .ThenInclude(pm => pm.Material)
                    .Include(p => p.ProductType)
                    .ToList();
            }

            _displayingProducts = new List<Product>(_products);
        }

        // Сначала фильтруем, потом по отфильтрованному списку
        // производим поиск, далее сортируем по условию
        private void DisplayProducts()
        {
            DisplayingProducts = Sort(Search(Filter(_products)));
        }
        
        // Производит поиск по списку продуктов и возвращает список найденных элементов
        private List<Product> Search(List<Product> products)
        {
            if ((SearchValue == string.Empty) || (SearchValue == null))
                return products;

            var value = SearchValue.ToLower();

            return products.Where(p => p.FullName.ToLower().Contains(value)).ToList();
        }

        // Фильтрует список и возвращает уже отфильтрованный список
        private List<Product> Filter(List<Product> products)
        {
            if (FilterValue == ValuesToFilter[0])
                return products;
            else
                return products.Where(p => p.ProductType.Title == FilterValue).ToList();
        }

        // Сортирует список и возвращает уже отсортированный список
        private List<Product> Sort(List<Product> products)
        {
            // "Без сортировки", "По названию (возр)", "По названию (уб)", "По цене (возр)", "По цене (уб)"
            if (SortValue == ValuesToSort[1])
                return products.OrderBy(p => p.Title).ToList();
            else if (SortValue == ValuesToSort[2])
                return products.OrderByDescending(p => p.Title).ToList();
            else if (SortValue == ValuesToSort[3])
                return products.OrderBy(p => p.TotalCost).ToList();
            else if(SortValue == ValuesToSort[4])
                return products.OrderByDescending(p => p.TotalCost).ToList();
            else
                return products;
        }
    }
}