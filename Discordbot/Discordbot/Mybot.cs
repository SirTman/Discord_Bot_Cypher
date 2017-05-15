
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

        public string CONT = "```";
        // CONT + "Text" + CONT


        public Mybot()
        {
            discord = new DiscordClient(x =>
            {
                x.AppName = "Cypher";
                x.LogLevel = LogSeverity.Debug;
                x.LogHandler = Log;
            });
            
            //Call Initialiser
            discord.UsingCommands(x => 
            {
                x.PrefixChar = '+';
                x.AllowMentionPrefix = true;
                x.HelpMode = HelpMode.Public;
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
            JoinVC();

            Destroy();

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
        private void Ping()
        {
            commands.CreateCommand("Ping")
                .Description("Used to see if i'm online and working")
                .Do(async (e) =>
            {
                 await e.Channel.SendMessage("```Hiya~ I'm online.```");
            });
        }
        //say hello
        private void Sayhello()
        {

            commands.CreateCommand("Say Hello")
                .Description("I'll say hello to everyone")
                .Do(async (e) =>
            {
                await e.Channel.SendMessage("Hello @everyone I'm @Cypher#1556");
            });
        }
        //Roll a radom number
        private void RefisterRNDGenCommand()
        {
            commands.CreateCommand("RNG")
                .Parameter("Num", ParameterType.Optional)
                .Description("Roll a random number between 1 and a number. Defalt is 10")
                .Do(async (e) =>
                {
                    int MaxCap = 10;
                    if (Int32.TryParse(e.GetArg("Num"), out MaxCap))
                    {
                        Random rnd = new Random();
                        var dmgvalue = rnd.Next(1, MaxCap);
                        string DMG = dmgvalue.ToString();
                        await e.Channel.SendMessage(CONT + DMG + CONT);
                    }
                    //Failed
                    else if (e.GetArg("Num") != " ")
                    {
                        Random rnd = new Random();
                        var dmgvalue = rnd.Next(1, MaxCap);
                        string DMG = dmgvalue.ToString();
                        await e.Channel.SendMessage(CONT + DMG + CONT);
                    }
                    else
                    {
                        await e.Channel.SendMessage("```" + e.GetArg("Num") + " Isn't vaild number```");
                    }


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

        public void Destroy()
        {
            commands.CreateCommand("Delete")
            .Description("Cypher will mass delete messages!!")
            .Parameter("DeleteAmount",ParameterType.Optional)
            .Do(async (e) =>
            {
                //Stuff
                await e.Channel.SendIsTyping();
                var userPermissions = e.User.GetPermissions(e.Channel).ManageMessages;
                int number;
                
                //Converter
                if (Int32.TryParse(e.GetArg("DeleteAmount"), out number)) { }
                else if (e.GetArg("DeleteAmount") != " ") { number = 1; }
                else { await e.Channel.SendMessage("```" + e.GetArg("DeleteAmount") + " Isn't vaild number```"); }

                Message[] message = new Message[number];
                message = e.Channel.DownloadMessages(number).Result;
                if (userPermissions == true)
                {
                    await e.Channel.SendMessage(CONT + "Mwhaha!!\nYes Master. It won't take me more than a single Pokemon to delete them." + CONT);
                    System.Threading.Thread.Sleep(3000);
                    await e.Channel.SendIsTyping();
                    //Check
                    try
                    {
                        await e.Channel.DeleteMessages(message);
                        if (e.User.Id == 187456924894232585)
                        {
                            await e.Channel.SendMessage(CONT + "All " + number + " Foe's have been eliminated, Master Tman" + CONT);
                        }
                        else
                        {
                            await e.Channel.SendMessage(CONT + "All " + number + " Foe's have been eliminated" + CONT);
                        }
                    }
                    catch (Exception ex)
                    {
                        if (ex.Message != "The server responded with error 404 (NotFound): \"Unknown Message\"")
                            await e.Channel.SendMessage(CONT + "Wait I don't have access to that!!!" + CONT);
                    }
                   
                   

                }
                else
                {
                    Console.WriteLine(CONT + "Woah! This is urlawful access at a command that could can this whole opration.\nUnless your my master or commander``` ```Back off!" + CONT);
                }

            });
        }

        //aduiso
        private void JoinVC()
        {
            commands.CreateCommand("Join")
                .Description("Used for Voice [WIP]")
                .Do(async (e) =>
                {
                    try
                    {
                        var server = e.Server.Name;
                        var voiceChannel = discord.FindServers(server).FirstOrDefault().VoiceChannels.FirstOrDefault();

                        await e.Channel.SendMessage("Joining " + voiceChannel + "!");
                        await discord.GetService<AudioService>()
                        .Join(voiceChannel);
                        await e.Channel.SendMessage("Joined " + voiceChannel + "!");
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


