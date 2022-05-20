using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories.Interfaces
{
    public interface ISettingsRepository
    {
        Task<List<Settings>> GetAllSettings();
        Task<double> GetCurrentGoldPrice();
        Task<Settings?> GetSettingByName(string name);
        Task<Settings?> UpdateSettingValueByName(string name, string value);
    }
}
