//Discord
using Discord;
using Discord.Commands;
using Discord.Commands.Permissions.Levels;

using Discord.Audio;

using Discord.Modules;

//System Stuff
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.IO;
//Aditional Stuff
using Concentus;
using Sodium;

/*Tman's Cyhper Bot
+Ping           Test to see if she is online
+Say Hello      Will introduce self
+RNG            Random Number Genrator between 1-100
+D20            Rolls a d20
*/
namespace Discordbot
{
    class Mybot
    {
        DiscordClient discord;
        CommandService commands;
        IAudioClient vc;
        public string CONT = "```";
        // CONT + "Text" + CONT

        public Mybot()
        {
            //Command exicution
           // bool Fight = false;
           // Fighter[] RosterList = new Fighter[100];

            discord = new DiscordClient(x =>
            {
                x.LogLevel = LogSeverity.Debug;
                x.LogHandler = Log;
            });
            
            //Call Initialiser
            discord.UsingCommands(x => 
            {
                x.PrefixChar = '+';
                x.AllowMentionPrefix = true;
            });
            commands = discord.GetService<CommandService>();
           
            // Opens an AudioConfigBuilder so we can configure our AudioService
            // Tells the AudioService that we will only be sending audio
            discord.UsingAudio(x => 
            {
                x.Mode = AudioMode.Outgoing;
            });
            //var voiceChannel = discord.FindServers("Music Bot Server").FirstOrDefault().VoiceChannels.FirstOrDefault(); // Finds the first VoiceChannel on the server 'Music Bot Server'

           // var _vClient = discord.GetService<AudioService>() // We use GetService to find the AudioService that we installed earlier. In previous versions, this was equivelent to _client.Audio()
                    //.Join(voiceChannel);

            //Commands Classes go here
            Ping();
            RefisterRNDGenCommand();
            Sayhello();
            d20();
            Help();
            JoinVC();
            MoveVC();

            WelcomeNLeave();

            //Stuff used to connect it to the server
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzAzNzg2NjE1NzkxMjg4MzIw.C9eXIA.PCTqVlEQjnjleHr7Nvh8g9QbA5U", TokenType.Bot);
            });
        }


        //Commands
        //Check if the bot is online
        private void Help()
        {
            commands.CreateCommand("Help").Do(async (e) =>
            {
                await e.Channel.SendMessage("```Saddly Tman hasn't given me anything good to do so I can't help```");
            });
        }

        private void Ping()
        {
            commands.CreateCommand("Ping") .Do(async (e) =>
            {
                 await e.Channel.SendMessage("```Hiya~ I'm online.```");
            });
        }
        //say hello
        private void Sayhello()
        {

            commands.CreateCommand("Say Hello").Do(async (e) =>
            {
                await e.Channel.SendMessage("Hello @everyone I'm @Cypher#1556");
            });
        }
        //Roll a radom number
        private void RefisterRNDGenCommand()
        {
            commands.CreateCommand("RNG").Do(async (e) =>
                {
                    Random rnd = new Random();
                    var dmgvalue = rnd.Next(1, 100);
                    string DMG = dmgvalue.ToString();
                    await e.Channel.SendMessage(DMG);

                });
        }
        //Roll a radom number
        private void d20()
        {
            commands.CreateCommand("d20").Do(async (e) =>
            {
                Random rnd = new Random();
                var dmgvalue = rnd.Next(1, 20);
                string DMG = dmgvalue.ToString();
                await e.Channel.SendMessage(DMG);

            });
        }

        private void WelcomeNLeave()
        {
            //Join
            discord.UserJoined += async (s, e) =>
            {
                var channel = e.Server.DefaultChannel;
                var server = e.Server.Name;
                var user = e.User.Mention;
                await channel.SendMessage(string.Format("Welcome " + user + " to " + server));
            };

            discord.UserLeft += async (s, e) =>
            {
                var channel = e.Server.DefaultChannel;
                var server = e.Server.Name;
                var user = e.User.Mention;
                //var user = e.User.Name;
                await channel.SendMessage(string.Format(user + " has removed herself from the server!"));
            };

        }

        //aduiso
        private void JoinVC()
        {
            commands.CreateCommand("Join")
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("```Joining masta!```");
                    await discord.GetService<AudioService>().Join(discord.FindServers("The Lobby").FirstOrDefault().VoiceChannels.FirstOrDefault());
                });
        }
        private void MoveVC()
        {
            commands.CreateCommand("Move")
                .Parameter("Channel", ParameterType.Required)
                .Do(async (e) =>
                {
                    await e.Channel.SendMessage("Joined " + e.GetArg("Channel"));
                });
        }

        //End
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

    }
}


