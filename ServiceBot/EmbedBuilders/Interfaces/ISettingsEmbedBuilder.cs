using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBot.EmbedBuilders.Interfaces
{
    public interface ISettingsEmbedBuilder
    {
        Task<Embed> GetAllSettingsEmbed();
        Task<Embed> GetBTCEmbed();
        Task<Embed> GetGPEmbed();
        Task<Embed> GetETHEmbed();
        Task<Embed> GetSettingByNameEmbed(string name);
        Task<Embed> UpdateSettingByName(string name, string value);
        Task<Embed> GetLocEmbed();
        Task<Embed> GetGPLocEmbed();
    }
}
