using Common.Enums;
using Data.Entities;
using Data.Repositories.Interfaces;
using Services.Interfaces;
using Services.Services.Models.Quote;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class QuoteService : IQuoteService
    {
        private readonly ISkillingRepository _skillingRepository;
        private readonly IServiceRepository _serviceRepository;
        private readonly ISettingsRepository _settingsRepository;
        public QuoteService(ISkillingRepository skillingRepository, IServiceRepository serviceRepository, ISettingsRepository settingsRepository)
        {
            _skillingRepository = skillingRepository;
            _serviceRepository = serviceRepository;
            _settingsRepository = settingsRepository;
        }

        public async Task<SkillingQuote> QuoteSkill(SkillType skillType, int startLevel, int endLevel)
        {
            var goldPrice = await _settingsRepository.GetCurrentGoldPrice();
            var skillingMethods = await _skillingRepository.GetSkillingMethodBySkillType(skillType);

            var viableMethods = skillingMethods
                .Where(x => x.StartLevel < endLevel && (x.EndLevel == null || x.EndLevel > startLevel))
                .ToList();

            var viableResults = viableMethods
                .Select(x => new SkillingMethodQuote(x, startLevel, endLevel, goldPrice))
                .OrderBy(x => x.StartLevel)
                .ToList();

            return new SkillingQuote() { SkillingMethods = viableResults };
        }

        public async Task<ServiceQuote> QuoteQuests(List<string> questNames)
        {
            var quests = await  _serviceRepository.GetServiceByNamesAndType(questNames, ServiceType.Quest);

            var serviceQuoteItems = new List<ServiceQuoteItems>();

            foreach(var quest in quests.Quests)
            {
                var item = new ServiceQuoteItems()
                {
                    Description = quest.Description,
                    Name = quest.Name
                };

                await SetPriceOnQuestItem(item, quest);

                serviceQuoteItems.Add(item);
            }

            var quote = new ServiceQuote(serviceQuoteItems, quests.UnfoundQuests);

            return quote;
        }

        public async Task<List<Service>> GetAllServicesByType(ServiceType serviceType)
        {
            var services = await _serviceRepository.GetAllServiceOfType(serviceType);

            return services;
        }

        private async Task SetPriceOnQuestItem(ServiceQuoteItems item, Service quest)
        {
            var goldPrice = await _settingsRepository.GetCurrentGoldPrice();

            if(quest.AmountType == AmountType.FlatGp)
            {
                item.GpTotal = Math.Round(quest.Amount, 2);

                item.UsdTotal = Math.Round(item.GpTotal * goldPrice, 2);

            }
            else if(quest.AmountType == AmountType.USD)
            {
                item.UsdTotal = quest.Amount;

                item.GpTotal = Math.Round(quest.Amount / goldPrice, 2);
            }
        }
    }
}
