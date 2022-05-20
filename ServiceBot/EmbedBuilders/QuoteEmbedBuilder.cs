using Common.Enums;
using Data.Entities;
using Discord;
using ServiceBot.EmbedBuilders.Interfaces;
using Services.Interfaces;
using Services.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBot.EmbedBuilders
{
    public class QuoteEmbedBuilder : IQuoteEmbedBuilder
    {
        private readonly IQuoteService _quoteService;
        public QuoteEmbedBuilder(IQuoteService quoteService)
        {
            _quoteService = quoteService;
        }

        public EmbedBuilder BuildBaseEmbed()
        {
            var logo = "https://cdn.discordapp.com/attachments/964019014336671785/967840910505304164/Cidla_128.gif";
            var embed = new EmbedBuilder();

            embed.ThumbnailUrl = logo;
            embed.Color = new Color(82, 235, 52);

            embed.Footer = new EmbedFooterBuilder() { IconUrl = logo, Text = "CidlaGold - Buy OSRS Services & Gold with the most competive pricing on the market!" };
            return embed;
        }

        public async Task<Embed> BuildSkillingQuoteEmbed(SkillType skillType, int startLevel, int endLevel)
        {
            var embed = BuildBaseEmbed();

            var skillingQuote = await _quoteService.QuoteSkill(skillType, startLevel, endLevel);

            embed.Title = skillType.ToString();
            embed.Author = new EmbedAuthorBuilder().WithName("Skilling Calculator").WithIconUrl(UtilityService.GetSkillIcon(skillType));

            foreach(var skillingMethod in skillingQuote.SkillingMethods)
            {
                embed.AddField(new EmbedFieldBuilder()
                {
                    Name = $"**__{skillingMethod.Name}__**",
                    Value = $"{skillingMethod.Description}\nLevel {skillingMethod.StartLevel} to {skillingMethod.EndLevel}\n{skillingMethod.TotalXp.ToString("#,##0")} Xp Required\n\nTotal: **${Math.Round(skillingMethod.UsdPrice, 2)} or {UtilityService.GetReadableGPPrice(Convert.ToInt64(skillingMethod.GpPrice))} GP**"

                });
            }

            embed.AddField(new EmbedFieldBuilder()
            {
                IsInline = false,
                Name = "Payment Methods",
                Value = "<:BTC:960457793045934090> <:ETH:960457895802187826> <:GP:974561123607117824>"
            });

            return embed.Build();
        }

        public async Task<List<Embed>> BuildQuestListEmbed(ServiceType serviceType)
        {
            var embedList = new List<Embed>();
            var embedP2p = new EmbedBuilder().WithColor(new Color(82, 235, 52));
            var embedF2p = BuildBaseEmbed();
            var embedMini = new EmbedBuilder().WithColor(new Color(82, 235, 52));

            var questList = await _quoteService.GetAllServicesByType(serviceType);

            BuildF2pFields(embedF2p, questList.Where(x => x.Flags == AdditionalFlags.F2P).ToList());
            BuildP2pFields(embedP2p, questList.Where(x => x.Flags == AdditionalFlags.P2P).ToList());

            embedF2p.Title = "__**Free to Play Quests**__";
            embedP2p.Title = "__**Members Quests**__";

            embedList.Add(embedF2p.Build());
            embedList.Add(embedP2p.Build());

            //embed.Author = new EmbedAuthorBuilder().WithName("Quest List").WithIconUrl("https://cdn.discordapp.com/attachments/971492312309985340/971838481615687770/Quests.png");

            //var description = "";

            //foreach (var quest in  questList)
            //{
            //    description += $"\n{quest}";
            //}

            //embed.Description = description;

            return embedList;
        }

        public void BuildF2pFields(EmbedBuilder embed, List<Service> quests)
        {
            var splitQuests = quests.ChunkBy(10);

            foreach(var half in splitQuests)
            {
                var f2pValue = "";
                foreach (var quest in half)
                {
                    f2pValue += $"```fix\n{quest.Name} = {Math.Round(quest.Amount, 1)}m \n```";
                }

                embed.AddField(new EmbedFieldBuilder()
                {
                    Name = $"\u200b",
                    Value = f2pValue,
                    IsInline = true
                });
            }
        }

        public void BuildP2pFields(EmbedBuilder embed, List<Service> quests)
        {
            var splitQuests = quests.ChunkBy(20);

            if(splitQuests.FirstOrDefault(x => x.Count() < 20) != null)
            {
                splitQuests.Last().AddRange(splitQuests.First(x => x.Count() < 20));
            }

            foreach (var half in splitQuests.Where(x => x.Count() == 20))
            {
                var f2pValue = "";
                foreach (var quest in half)
                {
                    f2pValue += $"```fix\n{quest.Name} = {Math.Round(quest.Amount, 1)}m \n```";
                }

                embed.AddField(new EmbedFieldBuilder()
                {
                    Name = $"\u200b",
                    Value = f2pValue,
                    IsInline = true
                });
            }
        }

        public async Task<Embed> BuildQuestQuoteEmbed(List<string> questNames)
        {
            var embed = BuildBaseEmbed();

            var questQuote = await _quoteService.QuoteQuests(questNames);

            embed.Author = new EmbedAuthorBuilder().WithName("Questing Calculator").WithIconUrl("https://cdn.discordapp.com/attachments/971492312309985340/971838481615687770/Quests.png");

            var description = $"**USD Total:** ${questQuote.UsdTotal} \n **GP Total:** {questQuote.GpTotal}m \n **Quests:** {questQuote.Items.Count()}\n";

            foreach (var questItem in questQuote.Items)
            {
                description += $"\n {questItem.Name}: ${questItem.UsdTotal} or {questItem.GpTotal}m";
            }

            var unfoundQuests = "";

            foreach(var unfound in questQuote.UnfoundItems)
            {
                unfoundQuests += $"{unfound}\n";
            }

            if(questQuote.UnfoundItems.Count > 0)
            {
                embed.AddField(new EmbedFieldBuilder()
                {
                    IsInline = false,
                    Name = "Unrecognized Quests",
                    Value = unfoundQuests
                });
            }

            embed.AddField(new EmbedFieldBuilder()
            {
                IsInline = false,
                Name = "Payment Methods",
                Value = "<:BTC:960457793045934090> <:ETH:960457895802187826> <:GP:974561123607117824>"
            });

            embed.Description = description;


            return embed.Build();
        }
    }
}
