using System;
using System.Collections.Generic;

namespace Vtitbid.Romashov.Bss.V2.Domain.Entities
{
    public partial class Shop
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string? Address { get; set; }
        public int AgentId { get; set; }

        public virtual Agent Agent { get; set; } = null!;
    }
}
