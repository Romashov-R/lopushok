using System;
using System.Collections.Generic;

namespace Vtitbid.Romashov.Bss.V2.Domain.Entities
{
    public partial class Material
    {
        public Material()
        {
            MaterialCountHistories = new HashSet<MaterialCountHistory>();
            ProductMaterials = new HashSet<ProductMaterial>();
            Suppliers = new HashSet<Supplier>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public int CountInPack { get; set; }
        public string Unit { get; set; } = null!;
        public double? CountInStock { get; set; }
        public double MinCount { get; set; }
        public string? Description { get; set; }
        public decimal Cost { get; set; }
        public string? Image { get; set; }
        public int MaterialTypeId { get; set; }

        public virtual MaterialType MaterialType { get; set; } = null!;
        public virtual ICollection<MaterialCountHistory> MaterialCountHistories { get; set; }
        public virtual ICollection<ProductMaterial> ProductMaterials { get; set; }

        public virtual ICollection<Supplier> Suppliers { get; set; }
    }
}
