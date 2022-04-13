using System;
using System.Collections.Generic;
using System.Text;
using DSharpPlus.CommandsNext;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext.Attributes;
using System.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net.Http;
using System.IO;
using DSharpPlus.Entities;

namespace DiscordGameBot.Commands
{
    public class ModerationCommands : BaseCommandModule
    {

        [Command("mute")]
        [Description("mutes a member")]
        [RequireRoles(RoleCheckMode.All, "MODERATOR", "Admin")]
        public async Task mute(CommandContext ctx)
        {
            await ctx.RespondAsync("e");
        }

        [Command("ban")]
        [Description("bans the member")]
        [RequireRoles(RoleCheckMode.All , "MODERATOR" , "ADMIN")]
        public async Task ban(CommandContext ctx)
        {
            await ctx.RespondAsync("e");
        }

        [Command("warns")]
        [Description("warns the member")]
        [RequireRoles(RoleCheckMode.All, "MODERATOR", "ADMIN")]
        public async Task warn(CommandContext ctx)
        {
            await ctx.RespondAsync("e");
        }

        [Command("kick")]
        [Description("kicks the member")]
        [RequireRoles(RoleCheckMode.All, "Moderator", "Admin")]
        public async Task kick(CommandContext ctx)
        {
            await ctx.RespondAsync("e");
        }
    }
}
