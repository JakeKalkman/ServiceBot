using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Models.Quote
{
    public class ServiceQuote
    {
        public double GpTotal { get; set; }
        public double UsdTotal { get; set; }

        public List<ServiceQuoteItems> Items { get; set; }
        public List<string> UnfoundItems { get; set; }

        public ServiceQuote(List<ServiceQuoteItems> items, List<string> unfoundItems)
        {
            GpTotal = Math.Round(items.Sum(x => x.GpTotal), 2);
            UsdTotal = Math.Round(items.Sum(x => x.UsdTotal), 2);
            Items = items;
            UnfoundItems = unfoundItems;
        }

    }

    public class ServiceQuoteItems
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double GpTotal { get; set; }
        public double UsdTotal { get; set; }
    }
}
