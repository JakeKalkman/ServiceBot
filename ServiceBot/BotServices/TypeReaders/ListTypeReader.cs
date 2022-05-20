using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;

namespace ServiceBot.BotServices.TypeReaders
{
    public class ListTypeReader : TypeReader
    {
        public override Task<TypeReaderResult> ReadAsync(ICommandContext context, string input, IServiceProvider services)
        {
            var list = input.Replace("\n", string.Empty).Split(",").ToList();
            if (!string.IsNullOrEmpty(input))
                return Task.FromResult(TypeReaderResult.FromSuccess(list));

            return Task.FromResult(TypeReaderResult.FromError(CommandError.ParseFailed, "Input could not be parsed as a List."));
        }
    }
}
