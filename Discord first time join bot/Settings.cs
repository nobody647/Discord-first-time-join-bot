using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Discord;

namespace Discord_first_time_join_bot
{
    class Settings
    {
        public static string email;
        public static string password;
        public static string defaultChannel;
        public static string welcomeMessage;
        public static string invite;

        public static void ReadConfig()
        {
            try
            {
                StreamReader sr = new StreamReader("config.txt");

                String line = sr.ReadLine();
                //line = line.Replace(" ", "");

                while (line != null)
                {
                    if (line.Contains("=") && !line.StartsWith("#"))
                    {
                        switch (line.Split('=')[0].Trim(' '))
                        {
                            case "email":
                                email = line.Split('=')[1].Trim(' ');
                                break;
                            case "password":
                                password = line.Split('=')[1].Trim(' ');
                                break;
                            case "defaultChannel":
                                defaultChannel = line.Split('=')[1].Trim(' ');
                                break;
                            case "welcomeMessage":
                                welcomeMessage = line.Split('=')[1].Trim(' ');
                                break;
                            case "invite":
                                invite = line.Split('=')[1].Trim(' ');
                                break;
                            default:
                                break;
                        }
                    }
                    line = sr.ReadLine();

                }
                sr.Close();
            }
            catch(Exception e)
            {
                Console.WriteLine("Uh oh! There was a problem with reading the config.txt file!");
                Console.WriteLine(e.Message);
            }
        }

        public static void VerifyConfig()
        {
            
        }
    }
}
