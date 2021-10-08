using Discord;
using Discord.Commands;
using Discord.WebSocket;
using Microsoft.Extensions.Configuration;
using System;
using System.Reflection;
using System.Threading.Tasks;

namespace YAMB.Services
{
    public class StartupService
    {
        private readonly IServiceProvider _provider;
        private readonly DiscordSocketClient _client;
        private readonly CommandService _commands;
        private readonly IConfigurationRoot _config;

        public StartupService(IServiceProvider provider,
            DiscordSocketClient client,
            CommandService commands,
            IConfigurationRoot config)
        {
            _provider = provider;
            _client = client;
            _commands = commands;
            _config = config;
        }

        public async Task StartAsync()
        {
            //TODO: Move to config.
            string youtubeApiKey = Environment.GetEnvironmentVariable("YAMB_YOUTUBE_API");
            if (string.IsNullOrEmpty(youtubeApiKey))
            {
                // TODO: Better error.
                throw new Exception("YAMB_YOUTUBE_API environment variable not found.");
            }

            string discordApiKey = Environment.GetEnvironmentVariable("YAMB_DISCORD_BOT");
            if (string.IsNullOrEmpty(discordApiKey))
            {
                // TODO: Better error.
                throw new Exception("YAMB_DISCORD_BOT environment variable not found.");
            }

            await _client.LoginAsync(TokenType.Bot, discordApiKey);
            await _client.StartAsync();

            await _commands.AddModulesAsync(Assembly.GetEntryAssembly(), _provider);
        }
    }
}
