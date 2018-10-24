using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using System.Text.RegularExpressions;
using OnePlusBot.Core.LevelingSystem;
using OnePlusBot.Core.UserAccounts;

namespace OnePlusBot
{
    public class CommandHandler
    {
        private readonly DiscordSocketClient _bot;
        private readonly CommandService _commands;
        private readonly IServiceProvider _services;

        public CommandHandler(DiscordSocketClient bot, CommandService commands, IServiceProvider services)
        {
            _commands = commands;
            _bot = bot;
            _services = services;
        }


        public async Task InstallCommandsAsync()
        {
            _bot.MessageReceived += HandleCommandAsync;
            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _services);
        }


        private async Task HandleCommandAsync(SocketMessage messageParam)
        {
            var message = messageParam as SocketUserMessage;
            if (message == null) return;

            int argPos = 0;

            IReadOnlyCollection<SocketGuild> guilds = _bot.Guilds;
            SocketGuild oneplusGuild = guilds.FirstOrDefault(x => x.Name == "/r/oneplus");
            SocketGuildChannel wallpapersChannel = oneplusGuild.Channels.FirstOrDefault(x => x.Name == "wallpapers");

           if(messageParam.Channel.Id == wallpapersChannel.Id)
            {
                var messageContent = messageParam.Content;

                if (!Regex.IsMatch(messageContent, @"^http(s)?://([\w-]+.)+[\w-]+(/[\w- ./?%&=])?$") && messageParam.Attachments.Count == 0 && messageParam.Embeds.Count == 0)
                {
                    await messageParam.DeleteAsync();
                }
            }

            var context = new SocketCommandContext(_bot, message);
            if (context.User.IsBot) return;


            // Mute check

            var userAccount = UserAccounts.GetAccount(context.User);

            if (userAccount.IsMuted)

            {

                await context.Message.DeleteAsync();

                return;

            }

            // Leveling
            Leveling.UserSentMessage((SocketGuildUser) context.User, (SocketTextChannel) context.Channel);


            if (!(message.HasCharPrefix(';', ref argPos) ||
                message.HasMentionPrefix(_bot.CurrentUser, ref argPos)))
                return;

            var result = await _commands.ExecuteAsync(
                context: context,
                argPos: argPos,
                services: _services);

        }
    }


}
