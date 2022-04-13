using DSharpPlus;
using DSharpPlus.EventArgs;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using DSharpPlus.CommandsNext;
using System.IO;
using Newtonsoft.Json;
using Microsoft.Extensions.Logging;
using DSharpPlus.CommandsNext.Attributes;

namespace DiscordGameBot
{
    public class Bot
    {
        public DiscordClient Client { get; private set; }
        public CommandsNextExtension Commands { get; private set; }

        //public bool EnableDefaultHelp { set; }
        public async Task RunAsync()
        {
            var json = string.Empty;

            using (var fs = File.OpenRead("config.json"))
            using (var sr = new StreamReader(fs, new UTF8Encoding(false)))
            json = await sr.ReadToEndAsync().ConfigureAwait(false);

            var configJason = JsonConvert.DeserializeObject<ShowJson>(json);

            var config = new DiscordConfiguration
            {
                Token = configJason.Token,
                TokenType = TokenType.Bot,
                AutoReconnect = true,
                MinimumLogLevel = LogLevel.Debug
            };

            Client = new DiscordClient(config);

            Client.Ready += OnClientReady;
         
            
             var commandsConfig = new CommandsNextConfiguration
             {
                StringPrefixes = new string[] { configJason.Prefix },
                EnableDms = false,
                EnableMentionPrefix = true,
                DmHelp = false,
             };
            
            Commands = Client.UseCommandsNext(commandsConfig);

            
            Commands.RegisterCommands<Commands.FunCommands>();
            Commands.RegisterCommands<Commands.ModerationCommands>();

            await Client.ConnectAsync();

            await Task.Delay(-1);
        }

        private Task OnClientReady(DiscordClient Client, ReadyEventArgs e)
        {
           return Task.CompletedTask;
        }

       
    }
}
