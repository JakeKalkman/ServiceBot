using Common.Enums;
using Discord;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBot.EmbedBuilders.Interfaces
{
    public interface IQuoteEmbedBuilder
    {
        Task<List<Embed>> BuildQuestListEmbed(ServiceType serviceType);
        Task<Embed> BuildQuestQuoteEmbed(List<string> questNames);
        Task<Embed> BuildSkillingQuoteEmbed(SkillType skillType, int startLevel, int endLevel);
    }
}
