using Common.Enums;
using Discord;
using ServiceBot.EmbedBuilders.Interfaces;
using Services.Interfaces;
using Services.Services;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBot.EmbedBuilders
{
    public class SettingsEmbedBuilder : ISettingsEmbedBuilder
    {
        private readonly ISettingService _settingService;

        public SettingsEmbedBuilder(ISettingService settingService)
        {
            _settingService = settingService;
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

        public async Task<Embed> GetAllSettingsEmbed()
        {
            var baseEmbed = BuildBaseEmbed();

            baseEmbed.Title = "All Settings";

            var settings = await _settingService.GetAllSettings();

            foreach (var setting in settings)
            {
                baseEmbed.AddField(new EmbedFieldBuilder()
                {
                    IsInline = true,
                    Name = setting.SettingName,
                    Value = setting.SettingValue
                });
            }

            return baseEmbed.Build();
        }

        public async Task<Embed> GetSettingByNameEmbed(string name)
        {
            var baseEmbed = BuildBaseEmbed();

            var setting = await _settingService.GetSettingByName(name);

            if(setting == null)
            {
                baseEmbed.Description = "No setting with that name found";
                return baseEmbed.Build();
            }

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = true,
                Name = setting.SettingName,
                Value = setting.SettingValue
            });
            

            return baseEmbed.Build();
        }

        public async Task<Embed> GetBTCEmbed()
        {
            var baseEmbed = BuildBaseEmbed();

            var filename = Path.GetFileName("//EmbedBuilders\\Images\\BTCQR.png");
            baseEmbed.WithImageUrl($"attachment://{filename}");
            baseEmbed.Title = "Bitcoin";
            baseEmbed.Description = "Please scan the QR code in your app if it allows, or copy the wallet ID below.";
            var setting = await _settingService.GetSettingByName("BTC");

            if (setting == null)
            {
                baseEmbed.Description = "No setting with that name found";
                return baseEmbed.Build();
            }

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = false,
                Name = "Wallet",
                Value = setting.SettingValue
            });

            return baseEmbed.Build();
        }

        public async Task<Embed> GetETHEmbed()
        {
            var baseEmbed = BuildBaseEmbed();

            var filename = Path.GetFileName("//EmbedBuilders\\Images\\ETHQR.png");
            baseEmbed.WithImageUrl($"attachment://{filename}");
            baseEmbed.Title = "Ethereum";
            baseEmbed.Description = "Please scan the QR code in your app if it allows, or copy the wallet ID below.";
            var setting = await _settingService.GetSettingByName("ETH");

            if (setting == null)
            {
                baseEmbed.Description = "No setting with that name found";
                return baseEmbed.Build();
            }

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = false,
                Name = "Wallet",
                Value = setting.SettingValue
            });

            return baseEmbed.Build();
        }

        public async Task<Embed> GetGPEmbed()
        {
            var baseEmbed = BuildBaseEmbed();

            //var filename = Path.GetFileName("//EmbedBuilders\\Images\\joke.jpg");
            //baseEmbed.WithImageUrl($"attachment://{filename}");
            baseEmbed.WithAuthor("OSRS GP");
            baseEmbed.Author.IconUrl = "https://cdn.discordapp.com/attachments/964019014336671785/974560910150623272/877450722566864956.webp";
            baseEmbed.Description = "Get the dankest GP in the land, you won't find higher quality anywhere else!";
            var setting = await _settingService.GetSettingByName("GoldPrice");
            var buySetting = await _settingService.GetSettingByName("BuyPrice");

            if (setting == null || buySetting == null)
            {
                baseEmbed.Description = "No setting with that name found";
                return baseEmbed.Build();
            }

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = true,
                Name = "__**Buy from us**__",
                Value = $"```fix\n${setting.SettingValue}```"
            });

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = true,
                Name = "__**Sell to us**__",
                Value = $"```fix\n${buySetting.SettingValue}```"
            });

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = false,
                Name = "Payment Methods",
                Value = "<:BTC:960457793045934090> <:ETH:960457895802187826> <:GP:974561123607117824>"
            });

            return baseEmbed.Build();
        }

        public async Task<Embed> GetGPLocEmbed()
        {
            var baseEmbed = BuildBaseEmbed();

            //var filename = Path.GetFileName("//EmbedBuilders\\Images\\joke.jpg");
            //baseEmbed.WithImageUrl($"attachment://{filename}");
            baseEmbed.WithAuthor("OSRS GP");
            baseEmbed.Author.IconUrl = "https://cdn.discordapp.com/attachments/964019014336671785/974560910150623272/877450722566864956.webp";
            baseEmbed.Description = "Get the dankest GP in the land, you won't find higher quality anywhere else!";
            var setting = await _settingService.GetSettingByName("GoldPrice");
            var buySetting = await _settingService.GetSettingByName("BuyPrice");

            if (setting == null || buySetting == null)
            {
                baseEmbed.Description = "No setting with that name found";
                return baseEmbed.Build();
            }

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = true,
                Name = "__**Buy from us**__",
                Value = $"```fix\n${setting.SettingValue}```"
            });

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = true,
                Name = "__**Sell to us**__",
                Value = $"```fix\n${buySetting.SettingValue}```"
            });

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = false,
                Name = "__**Trade us at:**__",
                Value = "Location: **W335 or P2P W367 Lumbridge telespot**\nRSN: **King Cidla**\nWhat's your in-game name?\n```css\n[! Beware of imposters !]```"
            });

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = false,
                Name = "Payment Methods",
                Value = "<:BTC:960457793045934090> <:ETH:960457895802187826> <:GP:974561123607117824>"
            });

            return baseEmbed.Build();
        }

        public async Task<Embed> GetLocEmbed()
        {
            var baseEmbed = BuildBaseEmbed();

            //var filename = Path.GetFileName("//EmbedBuilders\\Images\\joke.jpg");
            //baseEmbed.WithImageUrl($"attachment://{filename}");
            baseEmbed.WithAuthor("OSRS GP");
            baseEmbed.Author.IconUrl = "https://cdn.discordapp.com/attachments/964019014336671785/974560910150623272/877450722566864956.webp";

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = false,
                Name = "__**Trade us at:**__",
                Value = "Location: **W335 or P2P W367 Lumbridge telespot**\nRSN: **King Cidla**\nWhat's your in-game name?\n```css\n[! Beware of imposters !]```"
            });


            return baseEmbed.Build();
        }


        public async Task<Embed> UpdateSettingByName(string name, string value)
        {
            var baseEmbed = BuildBaseEmbed();

            var setting = await _settingService.UpdateSettingValueByName(name, value);

            if (setting == null)
            {
                baseEmbed.Description = "No setting with that name found";
                return baseEmbed.Build();
            }

            baseEmbed.AddField(new EmbedFieldBuilder()
            {
                IsInline = true,
                Name = setting.SettingName,
                Value = setting.SettingValue
            });


            return baseEmbed.Build();
        }
    }
}
