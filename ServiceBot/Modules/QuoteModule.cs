using Common.Enums;
using Discord.Commands;
using ServiceBot.EmbedBuilders.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBot.Modules
{
    public class QuoteModule : ModuleBase<SocketCommandContext>
    {
        private readonly IQuoteEmbedBuilder _quoteEmbedBuilder;
        private string _lastAliasUsed;

        public QuoteModule(IQuoteEmbedBuilder quoteEmbedBuilder)
        {
            _quoteEmbedBuilder = quoteEmbedBuilder;
        }

        protected override void BeforeExecute(CommandInfo command)
        {
            var message = Context.Message.CleanContent;
            _lastAliasUsed = message.Split(" ")[0].Replace("!", string.Empty);
            if(_lastAliasUsed == "wc")
            {
                _lastAliasUsed = "Woodcutting";
            }
            if(_lastAliasUsed == "rc")
            {
                _lastAliasUsed = "Runecrafting";
            }
            if(_lastAliasUsed == "fm")
            {
                _lastAliasUsed = "Firemaking";
            }
        }

        [Command("Thieving")]
        [Alias(new string[24] {"Attack","Strength", "Defense", "Magic", "Ranged", "Prayer", 
            "Mining", "Fishing", "Woodcutting", "Hunter", "Farming", "Cooking", "Smithing", "Fletching", 
            "Firemaking", "Herbalore", "Crafting", "Runecrafting", "Construction", "Agility", "Slayer", "rc", "wc", "fm" })]
        public async Task GetSkillingQuote(string skillLevels)
        {
            try
            {
                SkillType skillEnumType = Enum.Parse<SkillType>(_lastAliasUsed, true);
                var skillLevelsSplit = skillLevels.Split('-').Select(x => int.Parse(x)).ToList();

                var skillQuote = await _quoteEmbedBuilder.BuildSkillingQuoteEmbed(skillEnumType, skillLevelsSplit[0], skillLevelsSplit[1]);

                await Context.Channel.SendMessageAsync(embed: skillQuote);
            }
            catch (Exception ex)
            {
                //todo: add error;
            }
        }



        [Command("quests")]
        [Alias(new string[1] { "quest" })]
        public async Task QuestQuote([Remainder] List<string> questNames)
        {
            var questQuote = await _quoteEmbedBuilder.BuildQuestQuoteEmbed(questNames.ToList());

            await Context.Channel.SendMessageAsync(embed: questQuote);

        }

        [Command("questlist")]
        public async Task QuestList()
        {
            var questList = await _quoteEmbedBuilder.BuildQuestListEmbed(ServiceType.Quest);

            foreach(var embed in questList)
            {
                await Context.Channel.SendMessageAsync(embed: embed);
            }
        }
    }
}
