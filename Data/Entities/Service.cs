using Common.Enums;
using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Service : BaseEntity
    {
        public string Name { get; set; } = default!;

        public string? Description { get; set; }

        public ServiceType ServiceType { get; set; }
        
        public double Amount { get; set; }

        public AmountType AmountType { get; set; }
        public AdditionalFlags Flags { get; set; }
        public virtual ICollection<ServiceUpcharge>? ServiceUpcharges { get; set; }
    }
}
