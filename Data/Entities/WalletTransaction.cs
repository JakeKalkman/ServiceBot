using Common.Enums;
using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class WalletTransaction : BaseEntity
    {
        public int SendersWalletId { get; set; }
        public int? RecieversWalletId { get; set; }
        public string CommandUsed { get; set; } = default!;
        public string CommandUser { get; set; } = default!;
        public double OldValue { get; set; }
        public double NewValue { get; set; }
        public double Amount { get; set; }
        public AmountType Type { get; set; }

        public virtual Wallet SenderWallet { get; set; } = default!;
        public virtual Wallet? RecieversWallet { get; set; }
    }
}
