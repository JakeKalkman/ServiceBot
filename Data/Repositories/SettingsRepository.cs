using Data.Entities;
using Data.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class SettingsRepository : ISettingsRepository
    {
        private readonly ServiceBotContext _db;

        public SettingsRepository(ServiceBotContext db)
        {
            _db = db;
        }

        public async Task<Settings?> GetSettingByName(string name)
        {
            var setting = await _db.Settings.FirstOrDefaultAsync(x => x.SettingName.ToLower() == name.ToLower());

            return setting;
        }

        public async Task<List<Settings>> GetAllSettings()
        {
            var settings = await _db.Settings.ToListAsync();

            return settings;
        }

        public async Task<Settings?> UpdateSettingValueByName(string name, string value)
        {
            var setting = await GetSettingByName(name);

            if(setting != null)
            {
                setting.SettingValue = value;

                await _db.SaveChangesAsync();
            }

            return setting;
        }

        public async Task<double> GetCurrentGoldPrice()
        {
            var goldSetting = await _db.Settings.FirstOrDefaultAsync(x => x.SettingName == "GoldPrice");

            if(goldSetting == null)
            {
                throw new Exception();
            }

            return double.Parse(goldSetting.SettingValue);
        }
    }
}
