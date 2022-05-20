using Data.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Entities
{
    public class Wallet : BaseEntity
    {
        public string WalletUserId { get; set; } = default!;
        public double WalletUsdAmount { get; set; }
        public double WalletGpAmount { get; set; }

        public virtual ICollection<WalletTransaction>? SentTransactions { get; set; }
        public virtual ICollection<WalletTransaction>? RecievedTransactions { get; set; }
    }
}
