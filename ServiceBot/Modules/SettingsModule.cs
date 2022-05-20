using Common.Enums;
using Discord;
using Discord.Commands;
using ServiceBot.EmbedBuilders.Interfaces;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBot.Modules
{
    public class SettingsModule : ModuleBase<SocketCommandContext>
    {
        private readonly ISettingsEmbedBuilder _settingsEmbedBuilder;
        private readonly ISettingService _settingService;
        private bool _isAdmin = false;
        private bool _isManager = false;
        public SettingsModule(ISettingsEmbedBuilder settingsEmbedBuilder, ISettingService settingService)
        {
            _settingsEmbedBuilder = settingsEmbedBuilder;
            _settingService = settingService;
        }

        protected override void BeforeExecute(CommandInfo command)
        {
            var guildUser = Context.Guild.Users.FirstOrDefault(x => x.Id == Context.User.Id);
            var roleList = guildUser.Roles.Select(x => x.Id).ToList();
            if (guildUser.Id == 149976413041065984 || roleList.Contains(877195952941834310))
            {
                _isAdmin = true;
            }

            if (guildUser.Id == 149976413041065984 || roleList.Contains(952682924845711360))
            {
                _isManager = true;
            }
        }

        [Command("add")]
        public async Task WalletAdd(IGuildUser guildUser, double amount, string type)
        {
            var test = 1;
        }

        [Command("tip")]
        [Alias(new string[1] { "send" })]
        public async Task WalletTip(IGuildUser recievingUser, double amount, string type)
        {

        }

        [Command("w")]
        [Alias(new string[3] { "wallet", "balance", "bal" })]
        public async Task WalletBalance(IGuildUser User)
        {

        }

        [Command("BTC")]
        public async Task GetBTC()
        {
            if (_isManager)
            {
                var embed = await _settingsEmbedBuilder.GetBTCEmbed();

                await Context.Channel.SendFileAsync("EmbedBuilders/Images/BTCQR.png", embed: embed);
            }
        }

        [Command("ETH")]
        public async Task GetETH()
        {
            if (_isManager)
            {
                var embed = await _settingsEmbedBuilder.GetETHEmbed();

                await Context.Channel.SendFileAsync("EmbedBuilders/Images/ETHQR.png", embed: embed);
            }
        }

        [Command("gp")]
        public async Task GetGoldPrice()
        {
            var embed = await _settingsEmbedBuilder.GetGPEmbed();

            await SendEmbed(embed);
        }

        [Command("gploc")]
        public async Task GetGoldPriceAndLoc()
        {
            if (_isManager)
            {
                var embed = await _settingsEmbedBuilder.GetGPLocEmbed();

                await SendEmbed(embed);
            }
        }

        [Command("loc")]
        public async Task GetGoldLocation()
        {
            if (_isManager)
            {
                var embed = await _settingsEmbedBuilder.GetLocEmbed();

                await SendEmbed(embed);
            }
        }

        [Command("settings")]
        public async Task GetAllSettings()
        {
            if (_isAdmin)
            {
                var embed = await _settingsEmbedBuilder.GetAllSettingsEmbed();

                await SendEmbed(embed);
            }
        }

        [Command("updatesetting")]
        public async Task UpdateSetting(string name, string value)
        {
            if (_isAdmin)
            {
                var embed = await _settingsEmbedBuilder.UpdateSettingByName(name, value);

                await SendEmbed(embed);
            }     
        }

        [Command("updategp")]
        public async Task UpdateGoldPrice(string sellValue, string? buyValue = null)
        {
            if (_isAdmin)
            {
                if(await _settingService.UpdateGoldPrice(sellValue, buyValue))
                {
                    await Context.Channel.SendMessageAsync("Gold price updated successfully.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Gold price updated successfully.");
                }
            }
        }

        public async Task SendEmbed(Embed embed)
        {
            await Context.Channel.SendMessageAsync(embed: embed);
        }
    }
}
