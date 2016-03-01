using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Discord_first_time_join_bot
{
    class Program
    {
        DiscordClient Client;
        static void Main(string[] args)
        {
            Console.WriteLine("Made by /u/Expired_Marshmallows");
            Program p = new Program();
            p.Start();
        }
        void Start()
        {
            Settings.ReadConfig();
            Client = new DiscordClient();

            Client.UserJoined += (s, e) =>
            {
                Console.WriteLine("User Joined " + e.User.Name);
                Console.WriteLine(Settings.defaultChannel);
                foreach(Channel c in e.Server.TextChannels)
                {
                    if (c.Name.Equals(Settings.defaultChannel))
                    {
                        c.SendMessage(Settings.welcomeMessage + ", " + e.User.Mention);
                        return;
                    }
                }
                Console.WriteLine("The channel specified in the config could not be found, so we're sending the welcome message to the default channel");
                e.Server.DefaultChannel.SendMessage(Settings.welcomeMessage + ", " + e.User.Mention);
            };

            Client.LoggedIn += (s, e) =>
            {
                Console.WriteLine("Connected!");
                foreach (Server se in Client.Servers)
                {
                    foreach (Channel c in se.TextChannels)
                    {
                        if (c.Name.Equals(Settings.defaultChannel)) return;
                    }
                }
                Console.WriteLine("Uh oh! It seems the channel name you specified could not be found! Make sure that you typed the exact name of a text channel, and make sure that the bot has permission to read and send messages to that channel");
                
            };

            Client.ExecuteAndWait(async () =>
            {
                Console.WriteLine("Attempting connection to Discord");
                try
                {
                    await Client.Connect(Settings.email, Settings.password);
                }
                catch(Exception e)
                {
                    Console.WriteLine("Oh no! There was an error connecting to Discord with the provided username and password!");
                    Console.WriteLine(e.Message);
                    return;
                }
                
                try
                {
                    await Client.GetInvite(Settings.invite).Result.Accept();
                }
                catch (Exception e)
                {
                    if (Client.Servers.Any())
                    {
                        Console.WriteLine("Hmm... The invite provided in the config didn't work");
                        Console.WriteLine("That's OK, though, the bot is already a member of at least one server");
                    }
                    else
                    {
                        Console.WriteLine("Uh oh! The invite provided in the config didn't work!");
                        Console.WriteLine("That's a problem, because the bot is currently not a member of any server! That means it won't work");
                        Console.WriteLine("Check the config.txt file and make sure to fill in a valid invite code");
                    }
                }
            });
        }
    }
}
