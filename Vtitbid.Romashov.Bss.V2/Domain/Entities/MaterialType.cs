using System;
using System.Collections.Generic;

namespace Vtitbid.Romashov.Bss.V2.Domain.Entities
{
    public partial class MaterialType
    {
        public MaterialType()
        {
            Materials = new HashSet<Material>();
        }

        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public double DefectedPercent { get; set; }

        public virtual ICollection<Material> Materials { get; set; }
    }
}
