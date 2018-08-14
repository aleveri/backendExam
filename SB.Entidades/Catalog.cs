using System;
using System.Collections.Generic;
using static SB.Entities.Enums;

namespace SB.Entities
{
    public class Catalog : BaseEntity
    {
        public string Name { get; set; }
        
        public string Code { get; set; }
        
        public CatalogType Type { get; set; }

        public Guid? ParentId { get; set; }

        public virtual Catalog Parent { get; set; }

        public virtual ICollection<Catalog> Childs { get; set; }
    }
}
