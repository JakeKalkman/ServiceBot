using Discord;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Services.Interfaces;
using Services.Services;
using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Data;
using Data.Repositories.Interfaces;
using Data.Repositories;
using ServiceBot.EmbedBuilders.Interfaces;
using ServiceBot.EmbedBuilders;
using ServiceBot.BotServices;
using Discord.Commands;
using Services.Services.Interfaces;
using Services;

namespace ServiceBot
{
    class Program
    {
        private DiscordSocketClient _client;
        public static async Task Main(string[] args) => await new Program().MainAsync(args);

        public async Task MainAsync(string[] args)
        {
            //uncomment for data migrations only

            using IHost host = Host.CreateDefaultBuilder(args)
                .ConfigureServices((_, services) =>
                    services.AddDbContext<ServiceBotContext>())
                    .Build();

            //using (var services = ConfigureServices())
            //{
            //    _client = services.GetRequiredService<DiscordSocketClient>();

            //    _client.Log += Log;

            //    //  You can assign your bot token to a string, and pass that in to connect.
            //    //  This is, however, insecure, particularly if you plan to have your code hosted in a public repository.
            //    var token = "OTY3ODM3NTI2MzM5NDg1NzM2.YmWG7w.PSPzskywgS56ZMiZNbj-xsXe25k";
            //    var cildaToken = "OTczNjEyMDgyMzI5MTc4MTcz.GQxwpH.L2eLIaIEBOS_-Zvg-eKS3VaIXplZAl9hR6W_8U";

            //    // Some alternative options would be to keep your token in an Environment Variable or a standalone file.
            //    // var token = Environment.GetEnvironmentVariable("NameOfYourEnvironmentVariable");
            //    // var token = File.ReadAllText("token.txt");
            //    // var token = JsonConvert.DeserializeObject<AConfigurationClass>(File.ReadAllText("config.json")).Token;

            //    await _client.LoginAsync(TokenType.Bot, cildaToken);
            //    await _client.StartAsync();

            //    await services.GetRequiredService<BotCommandHandler>().InstallCommandsAsync();

            //    // Block this task until the program is closed.
            //    await Task.Delay(-1);
            //}
        }

        private Task Log(LogMessage msg)
        {
            Console.WriteLine(msg.ToString());
            return Task.CompletedTask;
        }

        private ServiceProvider ConfigureServices()
        {
            // this returns a ServiceProvider that is used later to call for those services
            // we can add types we have access to here, hence adding the new using statement:
            // using csharpi.Services;
            return new ServiceCollection()
                .AddSingleton<DiscordSocketClient>()
                .AddSingleton<CommandService>()
                .AddSingleton<BotCommandHandler>()
                .AddTransient<IOrderService, OrderService>()
                .AddTransient<IQuoteService, QuoteService>()
                .AddTransient<ISettingService, SettingService>()
                .AddTransient<IAdminService, AdminService>()
                .AddTransient<IQuoteEmbedBuilder, QuoteEmbedBuilder>()
                .AddTransient<ISettingsEmbedBuilder, SettingsEmbedBuilder>()
                .AddScoped<ISkillingRepository, SkillingRepository>()
                .AddScoped<IServiceRepository, ServiceRepository>() 
                .AddScoped<ISettingsRepository, SettingsRepository>()
                .AddDbContext<ServiceBotContext>()
                .BuildServiceProvider();
        }
    }
}
