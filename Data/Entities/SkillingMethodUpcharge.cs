using Common.Enums;
using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class SkillingMethodUpcharge : BaseEntity
    {
        public string Description { get; set; } = default!;
        public SkillingMethodUpchargeType SkillingMethodUpchargeType { get; set; }
        public AmountType SkillingMethodUpchargeAmountType { get; set; }
        public double Amount { get; set; }
        public int SkillingMethodId { get; set; }

        public virtual SkillingMethod? SkillingMethod { get; set; }
    }
}
