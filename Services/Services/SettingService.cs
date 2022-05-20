using Data.Entities;
using Data.Repositories.Interfaces;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services
{
    public class SettingService : ISettingService
    {
        private readonly ISettingsRepository _settingsRepository;

        public SettingService(ISettingsRepository settingsRepository)
        {
            _settingsRepository = settingsRepository;
        }

        public async Task<List<Settings>> GetAllSettings() => await _settingsRepository.GetAllSettings();

        public async Task<Settings?> UpdateSettingValueByName(string name, string value) => await _settingsRepository.UpdateSettingValueByName(name, value);

        public async Task<Settings?> GetSettingByName(string name) => await _settingsRepository.GetSettingByName(name);

        public async Task<bool> UpdateGoldPrice(string sellValue, string? buyValue)
        {
            try
            {
                await _settingsRepository.UpdateSettingValueByName("GoldPrice", sellValue);

                if (buyValue != null)
                {
                    await _settingsRepository.UpdateSettingValueByName("BuyPrice", buyValue);
                }
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }           
        }
    }
}
