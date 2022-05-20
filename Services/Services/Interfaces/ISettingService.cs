using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Services.Services.Interfaces
{
    public interface ISettingService
    {
        Task<List<Settings>> GetAllSettings();
        Task<Settings?> GetSettingByName(string name);
        Task<bool> UpdateGoldPrice(string sellValue, string? buyValue);
        Task<Settings?> UpdateSettingValueByName(string name, string value);
    }
}
