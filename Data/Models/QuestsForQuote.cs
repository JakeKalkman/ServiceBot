using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Models
{
    public class QuestsForQuote
    {
        public List<Service> Quests { get; set; } = default!;
        public List<string> UnfoundQuests { get; set; } = default!;

        public QuestsForQuote(List<Service> quests, List<string> unfoundQuests)
        {
            Quests = quests;
            UnfoundQuests = unfoundQuests;
        }
    }
}
