using System;
using System.Collections.Generic;
using System.Text;
using Discord.WebSocket;
using System.Threading.Tasks;
using Discord;

namespace OnePlusBot.Core.LevelingSystem
{
    internal static class Leveling
    {

        internal async static void UserSentMessage(SocketGuildUser user, SocketTextChannel channel)
        {

            var userAccount = UserAccounts.UserAccounts.GetAccount(user);

            uint oldLevel = userAccount.LevelNumber;

            userAccount.XP += 50;

            UserAccounts.UserAccounts.SaveAccounts();

            uint newLevel = userAccount.LevelNumber;



            if (oldLevel != newLevel)

            {

                // the user leveled up

                var embed = new EmbedBuilder();

                embed.WithColor(67, 160, 71);

                embed.WithTitle("LEVEL UP!");

                embed.WithDescription(user.Username + " just leveled up!");

                embed.AddField("LEVEL", newLevel);

                embed.AddField("XP", userAccount.XP);

                await channel.SendMessageAsync("", embed: embed.Build());

            }

        }
    }
}
