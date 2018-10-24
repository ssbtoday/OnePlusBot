using System;
using System.Collections.Generic;
using System.Text;
using Discord.Commands;
using System.Threading.Tasks;
using Discord;

namespace OnePlusBot.Modules
{
    public class EchoModule : ModuleBase<SocketCommandContext>
    {
        [Command("echo")]
        [Summary("Echoes back the remainder argument of the command.")]
        [RequireUserPermission(GuildPermission.ManageMessages)]
        public async Task EchoAsync([Remainder] string text)
        {
            var embed = new EmbedBuilder();

            embed.WithTitle("Message by " + Context.User.Username);

            embed.WithDescription(text);

            embed.WithColor(new Color(0, 255, 0));

            await Context.Channel.SendMessageAsync("", false, embed.Build());
        } 

    }
}
