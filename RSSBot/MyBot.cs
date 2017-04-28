using Discord;
using Discord.Commands;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RSSBot
{
    class MyBot
    {
        DiscordClient discord;
        CommandService commands;

        Random rand;

        string[] freshestMemes;

        public MyBot()
        {
            rand = new Random();

            freshestMemes = new string[]
            {
                "memes/boggs.png", // 0
                "memes/jordanlaff.png", // 1
                "memes/ol_robbo.png", // 2
                "memes/thing.png", // 3
                "memes/yephone.png" // 4
            };

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Info;
                x.LogHandler = Log;
            });

            discord.UsingCommands(x =>
            {
                x.PrefixChar = '.';
                x.AllowMentionPrefix = true;
            });

            commands = discord.GetService<CommandService>();

            RegisterMemeCommand();

            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzA3Mjg4OTc3MjcyOTk1ODQw.C-QUSg.3EHlUW-LQd2ljhXwD3ZT-9GrOSM", TokenType.Bot);
            });
        }

        private void RegisterMemeCommand()
        {
            commands.CreateCommand("meme")
                .Do(async (e) =>
                {

                    int randomMemeIndex = rand.Next(freshestMemes.Length);
                    string memeToPost = freshestMemes[randomMemeIndex];
                    await e.Channel.SendFile(memeToPost);
                });
        }

        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }
    }
}
