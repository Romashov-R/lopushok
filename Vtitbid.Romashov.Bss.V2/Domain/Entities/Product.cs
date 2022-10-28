using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using System.Linq;

namespace Vtitbid.Romashov.Bss.V2.Domain.Entities
{
    public partial class Product
    {
        private StringBuilder _stringBuilder;
        private string? _image;
        public Product()
        {
            _stringBuilder = new StringBuilder();  
            ProductCostHistories = new HashSet<ProductCostHistory>();
            ProductMaterials = new HashSet<ProductMaterial>();
            ProductSales = new HashSet<ProductSale>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int? ProductTypeId { get; set; }
        public string ArticleNumber { get; set; } = null!;
        public string? Description { get; set; }
        public string? Image
        {
            get => (_image == string.Empty) || (_image == null) 
                ? $"\\Resources\\picture.png"
                : $"\\Resources{_image.Replace("jpg", "jpeg")}";
            set => _image = value;
        }
        public int? ProductionPersonCount { get; set; }
        public int? ProductionWorkshopNumber { get; set; }
        public decimal MinCostForAgent { get; set; }

        public virtual ProductType? ProductType { get; set; }
        public virtual ICollection<ProductCostHistory> ProductCostHistories { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }
        public virtual ICollection<ProductSale> ProductSales { get; set; }

        [NotMapped]
        public decimal TotalCost 
        { 
            get
            {
                if (ProductMaterials.Count == 0)
                    return MinCostForAgent;

                var totalCost = 0M;

                foreach (var pm in ProductMaterials)
                {
                    totalCost += Math.Ceiling((decimal)pm.Count) * pm.Material.Cost;
                }

                return totalCost;
            }
        }

        [NotMapped]
        public string FullName 
        { 
            get
            {
                var productTypeTitle = ProductType == null ? "Тип" : ProductType.Title;
                return $"{productTypeTitle} | {Title}";
            }
        }

        [NotMapped]
        public string MaterialStringRepresentation 
        { 
            get
            {
                _stringBuilder.Clear();

                if (ProductMaterials.Count == 0)
                    return "Нет материалов";

                _stringBuilder.Append("Материалы: ");

                foreach(var pm in ProductMaterials)
                {
                    _stringBuilder.Append(pm.Material.Title);
                    _stringBuilder.Append(", ");
                }

                _stringBuilder.Remove(_stringBuilder.Length - 2, 2);

                return _stringBuilder.ToString();
            }
        }
    }
}
