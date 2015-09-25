using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SteamKit2;
/*Steam Bot
 * 
 * Edited: Sep 25 2015
 * Coder: Wes A.
 * 
 * Summary: This class is an automated bot
 * It can sign into the the steam network, send friend invites,
 * accept trades, and check items in the trade window.
 * 
 */



namespace FueledSteamBot.Bot
{
    class SteamBot
    {
        public string botName = "DefaultBotName";

        private string username;
        public string BotUsername
        {           
            set { username = value; }
        }
        private string password;
        public string BotPassword
        {          
            set { password = value; }
        }
        private bool running;
        public bool Running
        {
            get { return running; }
        }

        private SteamClient client;
        private CallbackManager cbManager; 
        private SteamUser steamUser;  
        
        public SteamBot()
        {            
            running = true;

            // create our steamclient instance
            client = new SteamClient();
            // create the callback manager which will route callbacks to function calls
            cbManager = new CallbackManager(client);
            // get the steamuser handler, which is used for logging on after successfully connecting    
            steamUser = client.GetHandler<SteamUser>();

            //Register Callbacks
            cbManager.Subscribe<SteamClient.ConnectedCallback>(OnConnect);
            cbManager.Subscribe<SteamClient.DisconnectedCallback>(OnDisconnect);

            cbManager.Subscribe<SteamUser.LoggedOnCallback>(OnLogin);
            //cbManager.Subscribe<SteamUser.LoggedOffCallback>(OnLoggedOff);
                
        }
        public void Update()
        {
            cbManager.RunWaitCallbacks(TimeSpan.FromSeconds(1));
        }
        public void ConnectToSteamCloud()
        {
            bool emptyInfo = false;
            if (username == null || username =="")
            {
                PrintError("ERROR: " + botName + " is missing username");
                emptyInfo = true;
            }
            if (password == null || password == "")
            {
                PrintError("ERROR: " + botName + " is missing password");
                emptyInfo = true;
            }
            if (emptyInfo)
            {
                //Abort Connecting
                return;
            }
            else
            {
                Console.WriteLine("Connecting...");
                client.Connect();
                running = true;
            }
        }

        void OnConnect(SteamClient.ConnectedCallback callback)
        {
            if (callback.Result != EResult.OK)
            {
                PrintError("ERROR: "+callback.Result.ToString());
                running = false;
                return;
            }
            Console.WriteLine("Connected!");

            steamUser.LogOn(new SteamUser.LogOnDetails {
                Username = username,
                Password = password,
            });
        }

        void OnDisconnect(SteamClient.DisconnectedCallback callback)
        {            
            Console.WriteLine("Disconnected from steam cloud.");
            running = false;
            Console.WriteLine("Press any key to restart bot");
            Console.ReadKey();
        }

        void OnLogin(SteamUser.LoggedOnCallback callback)
        {
            if (callback.Result != EResult.OK)
            {
                if (callback.Result == EResult.AccountLogonDenied)
                {
                    //SteamGuard is stopping login
                }
                PrintError(callback.Result.ToString());
                PrintError(callback.ExtendedResult.ToString());
                return;
            }
            Console.WriteLine("Login Successful");
            //Login Success 
        }

        public void Destory()
        {
            steamUser.LogOff();
            running = false;
        }

        static void PrintError(string _error)
        {
            Console.BackgroundColor = ConsoleColor.Red;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.WriteLine(_error);
            Console.BackgroundColor = ConsoleColor.Black;
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
