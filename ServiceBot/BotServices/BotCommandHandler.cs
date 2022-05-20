using Discord.Commands;
using Discord.WebSocket;
using ServiceBot.BotServices.TypeReaders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBot.BotServices
{
    public class BotCommandHandler
    {
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IServiceProvider services;

        public BotCommandHandler(DiscordSocketClient client, CommandService commands, IServiceProvider serviceProvider)
        {
            _commands = commands;
            _client = client;
            services = serviceProvider;
        }

        public async Task InstallCommandsAsync()
        {

            _client.MessageReceived += HandleCommandAsync;
            _commands.AddTypeReader(typeof(List<string>), new ListTypeReader());

            var result = await _commands.AddModulesAsync(assembly: Assembly.GetEntryAssembly(),
                                        services: services);
        }

        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            if ((message.HasMentionPrefix(_client.CurrentUser, ref argPos)) ||
                message.Author.IsBot)
                return;

            var context = new SocketCommandContext(_client, message);

            if (message.Channel is Discord.WebSocket.SocketDMChannel)
            {
                var dmResult = await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: services);

                return;
            }

            else if (!message.HasCharPrefix('!', ref argPos))
            {
                return;
            }

            var result = await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: services);
        }
    }
}
