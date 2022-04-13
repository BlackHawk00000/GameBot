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
    public  class FunCommands : BaseCommandModule
    {
        [Command("ping")]
        [Description("Returns pong")]
        public async Task Ping(CommandContext ctx)
        {
            var pongEmbed = new DiscordEmbedBuilder
            {
                Title = "Pong! :ping_pong:",
                Description = "Time Taken : "  + ctx.Client.Ping  + " milliseconds",
                Color = DiscordColor.Green
            };

            var pongMessage = await ctx.RespondAsync(embed: pongEmbed).ConfigureAwait(false);
        }

        [Command("betterHelp")]
        public async Task Help(CommandContext ctx)
        {
            var HelpEmbed = new DiscordEmbedBuilder
            {
                Description = " **NOTE :scroll: : ** \n Use the default help command for command info. \n \n **MAIN COMMANDS** : \n ping :ping_pong:\n " +
                "Intro :scroll: \n math :1234: \n game :crystal_ball: \n \n **FUN COMMANDS :peach: :** \n " +
                "Ask :interrobang:  \n coinflip :coin: \n Say :speaking_head:  \n joemama :point_up:  \n" +
                "Random :1234:  \n \n **EXTRA COMMANDS :sparkles: : ** \n YouTube :tv: \n Google :mag: \n ddg :mag_right: \n \n ",
                Color = DiscordColor.MidnightBlue
            };

            var helpMsg = await ctx.Channel.SendMessageAsync(embed: HelpEmbed).ConfigureAwait(false);
        }

        [Command("myinfo")]
        [Description("gives information about you")]

        public async Task info(CommandContext ctx)
        {
            var embed = new DiscordEmbedBuilder();
            embed.WithTitle("Information :chart_with_upwards_trend: ").WithColor(DiscordColor.Green);
            embed.AddField("Username", ctx.User.Username, true);
            embed.AddField("Nickname", ctx.Member.Nickname, true);
            embed.AddField("User ID :1234:", ctx.User.Id.ToString(), true);
            embed.AddField("Is Bot :robot:", ctx.User.IsBot.ToString(), true);
            var IntroMsg = await ctx.RespondAsync(embed: embed).ConfigureAwait(false);
        }

        [Command("Intro")]
        [Description("Gives Introduction")]
        public async Task intro(CommandContext ctx)
        {
            string text = "Hello one!I am GameBot, I can do some basic math stuff , recommend games with downloadable links , I can repeat whatever you say(please don't make me say bad words which could ban me) , I can answer your questions (kind of). \n \n I am made in C# using DSharpPlus Library. My master's name is BlackHawk#8944. I am currently under development , so if I misbehave contact my master :)";
            var IntroEmbed = new DiscordEmbedBuilder
            {
                Title = "Introduction : ",
                Description = text,
                Color = DiscordColor.Green
            };

            var IntroMsg = await ctx.Channel.SendMessageAsync(embed: IntroEmbed).ConfigureAwait(false);
        }

        [Command("math")]
        public async Task MathAsync(CommandContext ctx, string math)
        {
            var dataTable = new DataTable();
            var result = dataTable.Compute(math, null);

            await ctx.RespondAsync($"Result : {result}").ConfigureAwait(false);
        }

        [Command("say")]
        [Description("Says whatever you say in quotation marks")]

        public async Task Say(CommandContext ctx , string repeat)
        {
            await ctx.Channel.SendMessageAsync(repeat).ConfigureAwait(false);
                

        }

        [Command("github")]

        public async Task github(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("https://github.com/BlackHawk00000/GameBot.git");
        }

        [Command("ask")]
        [Description("Answers your question with yes,no or maybe!")]

        public async Task Reply(CommandContext ctx, string repeat)
        {
            string[] replies = new string[] { "Yep", "No", "Absolutely", "Maybe", "I dont think so" , "why are u even asking these questions like serioulsly mate , get a life!" };

            Random generator = new Random();
            int random = generator.Next(replies.Length);

            string reply = replies[random];

            await ctx.RespondAsync(reply).ConfigureAwait(false);
        }

        [Command("game")]
        [Description("Recommends and informs you about a random game with downloadable links!")]
        public async Task GameFunction(CommandContext ctx)
        {
            using var client = new HttpClient();
            var result = await client.GetStringAsync("https://static.nvidiagrid.net/supported-public-game-list/gfnpc.json?JSON");


            dynamic json = JsonConvert.DeserializeObject(result);
            Random generator = new Random();
            int random = generator.Next(json.Count);
            var game = json[random];

            string genres = "Genres: ";
            if (game.genres.Count > 1)
            {
                foreach (string genre in game.genres)
                {
                    genres = genres + genre + " , ";
                }
            }
            else
            {
                genres = game.genres;
            }

            if(game.store == "Ubisoft" || game.store == "Epic")
            {
                await ctx.Channel.SendMessageAsync("Game name : " + game.title + "\n" +
            "Publisher : " + game.publisher + "\n" + genres + "\n" +
            "Status : " + game.status + "\n" + "Store : " + game.store + "\n" +
            "Download Link : N/A");
            }
            else
            {
                await ctx.Channel.SendMessageAsync("Game name : " + game.title + "\n" +
            "Publisher : " + game.publisher + "\n" + genres + "\n" +
            "Status : " + game.status + "\n" + "Store : " + game.store + "\n" +
            "Download Link : " + game.steamUrl);
            }

            //remove the comment below to view the full json file in console
            //Console.WriteLine(json);
        }

        [Command("google")]
        
        public async Task Google(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Sounds like something" +
                " <https://www.google.com/> would know");


        }

        [Command("ddg")]
        public async Task ddg(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Sounds like something" +
                " https://ddg.gg/ would know");

        }

        [Command("youtube")]

        public async Task utube(CommandContext ctx)
        {
            await ctx.Channel.SendMessageAsync("Time to watch a video on " + " <https://www.youtube.com/> I guess ");
        }

        [Command("random")]
        [Description("gives a radom number between , example : []random 1 100")]

        public async Task Random(CommandContext ctx , int min , int max)
        {
            Random generator = new Random();
            int random = generator.Next(min, max);

            await ctx.Channel.SendMessageAsync("Your Random Number : " + random);
        }

        [Command("joemama")]
       
        [Description("Tells you a joke about your mama ")]

        public async Task joemama(CommandContext ctx)
        {
            string[] jokes = new string[] { "Yo mama's so fat, when she fell I didn't laugh, but the sidewalk cracked up." ,
                "Yo mama's so fat, when she skips a meal, the stock market drops." ,
                "Yo mama's so fat, it took me two buses and a train to get to her good side." ,
                "Yo mama's so fat, when she goes camping, the bears hide their food." ,
                "Yo mama's so fat, if she buys a fur coat, a whole species will become extinct." ,
                "Yo mama's so fat, she stepped on a scale and it said: To Be Continued " ,
                "Yo mama's so fat, when she sits around the house, she SITS AROUND the house." ,
                "Yo mama's so fat, she can't even jump to a conclusion." ,
                "Yo mama's so fat, she brought a spoon to the Super Bowl." ,
                "Yo mama's so stupid, she stared at a cup of orange juice for 12 hours because it said : concentrate " ,
                "Yo mama's so stupid, she put lipstick on her forehead to make up her mind." ,
                "Yo mama's so stupid, when they said, 'Order in the court,' she asked for fries and a shake." ,
                "Yo mama's so stupid, she thought a quarterback was a refund." ,
                "Yo mama's so stupid, she got hit by a parked car." ,
                "Yo mama's so ugly, she threw a boomerang and it refused to come back." ,
                "Yo mama's so ugly, she made a blind kid cry." ,
                "Yo mama's teeth are so yellow when she smiles at traffic, it slows down." ,
                "Yo mama's so ugly, when she was little, she had to trick-or-treat by phone." ,
                "Yo mama's so ugly, her birth certificate is an apology letter."};

            Random generator = new Random();
            int random = generator.Next(jokes.Length);

            string reply = jokes[random];

            await ctx.Channel.SendMessageAsync(reply).ConfigureAwait(false);
        }

        [Command("coinflip")]

        [Description("flips a coin and tells you whether it is a head or a tail")]
        public async Task coin(CommandContext ctx) => await ctx.Channel.SendMessageAsync(
        new Random().Next(2) is 0 ? "Heads! :coin:" : "Tails! :coin:");



    }
}