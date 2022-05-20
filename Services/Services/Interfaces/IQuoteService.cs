using Common.Enums;
using Data.Entities;
using Services.Services.Models.Quote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Interfaces
{
    public interface IQuoteService
    {
        Task<List<Service>> GetAllServicesByType(ServiceType serviceType);
        Task<ServiceQuote> QuoteQuests(List<string> questNames);
        Task<SkillingQuote> QuoteSkill(SkillType skillType, int startLevel, int endLevel);
    }
}
