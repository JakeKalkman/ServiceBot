using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Models.Quote
{
    public class SkillingQuote
    {
        public List<SkillingMethodQuote> SkillingMethods { get; set; } = default!;
        public double GpPrice { get; set; }
        public double UsdPrice { get; set; }
        public int TotalXp { get; set; }

    }

    public class SkillingMethodQuote
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public int StartLevel { get; set; }
        public int EndLevel { get; set; }
        public int TotalXp { get; set; }
        public double GpPrice { get; set; }
        public double UsdPrice { get; set; }

        public List<SkillingMethodUpchargeQuote>? SkillingMethodUpchargeQuotes { get; set; }

        public SkillingMethodQuote()
        {

        }

        public SkillingMethodQuote(SkillingMethod method, int startLevel, int endLevel, double goldPrice)
        {
            Name = method.Name;
            Description = method.Description;

            var start = startLevel > method.StartLevel ? startLevel : method.StartLevel;
            var end = endLevel > method.EndLevel ? method.EndLevel : endLevel;

            var xp = UtilityService.GetExperienceDifferenceBetweenLevels(start!.Value, end!.Value);

            StartLevel = start!.Value;
            EndLevel = end!.Value;
            TotalXp = xp;
            GpPrice = xp * method.Amount;
            UsdPrice = (GpPrice / 1000000) * goldPrice;
        }
    }

    public class SkillingMethodUpchargeQuote
    {
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public double Price { get; set; }
    }
}
