using Common.Enums;
using Discord;
using Discord.Commands;
using Services.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceBot.Modules
{
    public class AdminModule : ModuleBase<SocketCommandContext>
    {
        private bool _isAdmin = false;
        private bool _isManager = false;
        private readonly IAdminService _adminService;

        public AdminModule(IAdminService adminService)
        {
            _adminService = adminService;
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

        [Command("deletesm")]
        public async Task DeleteSkillingMethod(string methodName, string skillType)
        {
            if (_isAdmin)
            {
                SkillType skillEnumType = Enum.Parse<SkillType>(skillType, true);
                var result = await _adminService.DeleteSkillingMethod(skillEnumType, methodName);

                if (result)
                {
                    await Context.Channel.SendMessageAsync("Deleted Successfully.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Failed to delete skilling method.");
                }
            }
            
        }

        [Command("addsm")]
        public async Task AddSkillingMethod(string methodName, string skillType, string amount, int startLevel, int? endLevel = null, string description = null)
        {
            if (_isAdmin)
            {
                SkillType skillEnumType = Enum.Parse<SkillType>(skillType, true);
                var result = await _adminService.AddSkillingMethod(methodName, description, amount, skillEnumType, startLevel, endLevel ?? 120);

                if (result)
                {
                    await Context.Channel.SendMessageAsync("Added Successfully.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Failed to add skilling method.");
                }
            }

        }

        [Command("updatesm")]
        public async Task UpdateSkillingMethod(string methodName, string skillType, string amount, int? startLevel = null, int? endLevel = null, string description = null, string? newName = null)
        {
            if (_isAdmin)
            {
                SkillType skillEnumType = Enum.Parse<SkillType>(skillType, true);
                var result = await _adminService.UpdateSkillingMethod(methodName, skillEnumType, newName, amount, description, startLevel, endLevel ?? 120);

                if (result)
                {
                    await Context.Channel.SendMessageAsync("Updated Successfully.");
                }
                else
                {
                    await Context.Channel.SendMessageAsync("Failed to update skilling method.");
                }
            }

        }
    }
}
