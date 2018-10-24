using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Discord;
using Discord.Commands;
using Discord.WebSocket;
using OnePlusBot.Core.UserAccounts;

namespace OnePlusBot.Modules
{
   public class WarnModule : ModuleBase<SocketCommandContext>
    {

        [Command("Warn")]
        [RequireUserPermission(GuildPermission.Administrator)]
        [RequireBotPermission(GuildPermission.BanMembers)]
        public async Task WarnUser(IGuildUser user)

        {

            var userAccount = UserAccounts.GetAccount((SocketUser)user);

            userAccount.NumberOfWarnings++;

            UserAccounts.SaveAccounts();

            await ReplyAsync("**insertemote " + user + " has been warned.**");

        }
    }
}
