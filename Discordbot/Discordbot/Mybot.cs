
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
    
    class Mybot
    {
        DiscordClient discord;
        CommandService commands;

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

            discord.UsingAudio(x => 
            {
                x.Mode = AudioMode.Outgoing;
            

            });

            //Commands Classes go here
            Ping();
            RefisterRNDGenCommand();
            Sayhello();
            d20();
            JoinVC();
    

            //Stuff used to connect it to the server
            discord.ExecuteAndWait(async () =>
            {
                await discord.Connect("MzAzNzg2NjE1NzkxMjg4MzIw.C9eXIA.PCTqVlEQjnjleHr7Nvh8g9QbA5U", TokenType.Bot);
                await Task.Delay(TimeSpan.FromSeconds(4));
            });
        }
       
        
        //Commands
        //Check if the bot is online
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

        //aduiso
        private void JoinVC()
        {
            commands.CreateCommand("Join")
                .Do(async (e) =>
                {
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


