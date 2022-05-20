using Common.Enums;
using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class ServiceUpcharge : BaseEntity
    {
        public string Description { get; set; } = default!;
        public ServiceUpchargeType ServiceUpchargeType { get; set;}
        public AmountType ServiceUpchargeAmountType { get; set; }
        public double Amount { get; set; }
        public int ServiceId { get; set; }

        public virtual Service Service { get; set; } = default!;

    }
}
