using System;
using static SB.Entities.Enums;

namespace SB.Entities
{
    public class User : BaseEntity
    {
        public string Name { get; set; }

        public string Address { get; set; }

        public DateTime BirthDate { get; set; }

        public DocumentType DocumentType { get; set; }

        public string DocumentNumber { get; set; }

        public Guid CountryId { get; set; }

        public Guid StateId { get; set; }

        public Guid CityId { get; set; }

        public virtual Catalog Country { get; set; }

        public virtual Catalog State { get; set; }

        public virtual Catalog City { get; set; }
    }
}
