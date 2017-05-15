
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

//Discord
using Discord;
using Discord.Commands;
using Discord.Commands.Permissions.Levels;

using Discord.Audio;

using Discord.Modules;


/*Tman's Cyhper Bot
+Ping           Test to see if she is online
+Say Hello      Will introduce self
+RNG            Random Number Genrator between 1-100
+D20            Rolls a d20
*/

namespace Discordbot
{
<<<<<<< HEAD
    
=======
>>>>>>> origin/master
    class Mybot
    {
        DiscordClient discord;
        CommandService commands;
<<<<<<< HEAD
=======
        IAudioClient vc;
        public string CONT = "```";
        // CONT + "Text" + CONT
>>>>>>> origin/master

        public Mybot()
        {
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

<<<<<<< HEAD
            discord.UsingAudio(x => 
            {
                x.Mode = AudioMode.Outgoing;
            

            });
=======
            // Opens an AudioConfigBuilder so we can configure our AudioService
            // Tells the AudioService that we will only be sending audio
           
            discord.UsingAudio(x => 
            {
                x.Mode = AudioMode.Outgoing;
            });
            //var voiceChannel = discord.FindServers("Music Bot Server").FirstOrDefault().VoiceChannels.FirstOrDefault(); // Finds the first VoiceChannel on the server 'Music Bot Server'

           // var _vClient = discord.GetService<AudioService>() // We use GetService to find the AudioService that we installed earlier. In previous versions, this was equivelent to _client.Audio()
                    //.Join(voiceChannel);
>>>>>>> origin/master

            //Commands Classes go here
            Ping();
            RefisterRNDGenCommand();
            Sayhello();
            d20();
<<<<<<< HEAD
=======
            Help();
>>>>>>> origin/master
            JoinVC();
    

            WelcomeNLeave();

            //Stuff used to connect it to the server
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzAzNzg2NjE1NzkxMjg4MzIw.C9eXIA.PCTqVlEQjnjleHr7Nvh8g9QbA5U", TokenType.Bot);
                await Task.Delay(TimeSpan.FromSeconds(4));
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
<<<<<<< HEAD
                    var CommandExicutor = e.User.VoiceChannel;

                    // var voiceChannel = discord.FindServers(CommandExicutor).FirstOrDefault().VoiceChannels.FirstOrDefault(); // Finds the first VoiceChannel on the server 'Music Bot Server'
                    try
                    {
                        await discord.GetService<AudioService>() // We use GetService to find the AudioService that we installed earlier. In previous versions, this was equivelent to _client.Audio()
                                .Join(CommandExicutor); // Join the Voice Channel, and return the IAudioClient.
                        await e.Channel.SendMessage("Bro i'm comming calm your tits");
                    }
                    catch(InvalidOperationException)
                    {
                        await e.Channel.SendMessage("Well that didn't work?");
                    }
                    
=======
                    var server = e.Server.Name;
                    var voiceChannel = discord.FindServers(server).FirstOrDefault().VoiceChannels.FirstOrDefault();

                    await e.Channel.SendMessage("Joining " + voiceChannel + "!");
                    await discord.GetService<AudioService>()
                    .Join(voiceChannel);

                    await e.Channel.SendMessage("Joined " + voiceChannel + "!");
>>>>>>> origin/master
                });
        }
        public void SendAudio(string pathOrUrl)
        {
            
        }

        //End
        private void Log(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine(e.Message);
        }

    }
}


