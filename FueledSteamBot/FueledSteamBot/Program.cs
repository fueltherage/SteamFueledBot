using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteamKit2;

namespace FueledSteamBot
{
    class Program
    {
        static bool SteamBotRunning = true;

        static void Main(string[] args)
        {
            Console.WindowHeight = 30;           
            Console.Title = "Steam Fueled Bot";
            Bot.SteamBot bot = new Bot.SteamBot();


            while (SteamBotRunning)
            {
                Console.Clear();
                Banner();              

                //Get the bot's username and password   
                string username, password;
                Console.Write("Enter the bot's steam\nusername: ");
                username = Console.ReadLine();
                Console.Write("password: ");
                password = Console.ReadLine();

                bot.BotUsername = username;
                bot.BotPassword = password;

                //bot.BotUsername = "steamfueledbot";
                //bot.BotPassword = "D1-fu&bwc";
                
                bot.ConnectToSteamCloud();

                Console.CancelKeyPress += (s, e) =>
                {
                    e.Cancel = true;

                    Console.WriteLine("Received {0}, disconnecting...", e.SpecialKey);
                    bot.Destory();
                };

                while (bot.Running)
                {
                    bot.Update();
                }
                bot.Destory();
            }
        }
        static void Banner()
        {
            //Banner
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(" _______ _________ _______  _______  _______");
            Console.WriteLine("(  ____ \\\\__   __/(  ____ \\(  ___  )(       )");
            Console.WriteLine("| (    \\/   ) (   | (    \\/| (   ) || () () |");
            Console.WriteLine("| (_____    | |   | (__    | (___) || || || |");
            Console.WriteLine("(_____  )   | |   |  __)   |  ___  || |(_)| |");
            Console.WriteLine("      ) |   | |   | (      | (   ) || |   | |");
            Console.WriteLine("/\\____) |   | |   | (____/\\| )   ( || )   ( |");
            Console.WriteLine("\\_______)   )_(   (_______/|/     \\||/     \\|");
            Console.WriteLine("");
            Console.WriteLine(" _______           _______  _        _______  ______ ");
            Console.WriteLine("(  ____ \\|\\     /|(  ____ \\( \\      (  ____ \\(  __  \\");
            Console.WriteLine("| (    \\/| )   ( || (    \\/| (      | (    \\/| (  \\  )");
            Console.WriteLine("| (__    | |   | || (__    | |      | (__    | |   ) |");
            Console.WriteLine("|  __)   | |   | ||  __)   | |      |  __)   | |   | |");
            Console.WriteLine("| (      | |   | || (      | |      | (      | |   ) |");
            Console.WriteLine("| )      | (___) || (____/\\| (____/\\| (____/\\| (__/  )");
            Console.WriteLine("|/       (_______)(_______/(_______/(_______/(______/");
            Console.WriteLine("");
            Console.WriteLine(" ______   _______ _________");
            Console.WriteLine("(  ___ \\ (  ___  )\\__   __/");
            Console.WriteLine("| (   ) )| (   ) |   ) (");
            Console.WriteLine("| (__/ / | |   | |   | |");
            Console.WriteLine("|  __ (  | |   | |   | |");
            Console.WriteLine("| (  \\ \\ | |   | |   | |");
            Console.WriteLine("| )___) )| (___) |   | |");
            Console.WriteLine("|/ \\___/ (_______)   )_(\n");
            Console.ForegroundColor = ConsoleColor.White;
            //EndBanner
        }
        
    }

}
