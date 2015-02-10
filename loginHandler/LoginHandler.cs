using System;
using DarkRift;
using _debugHandler;
using RSAInterface;
using System.Collections;

namespace loginHandler
{
    public class loginHandler : Plugin
    {
        //Basic puglin data
        public override string name
        {
            get { return "loginHandler"; }
        }

        public override string version
        {
            get { return "0.1"; }
        }

        public override Command[] commands
        {
            get
            {
                return new Command[]
                    {
                        new Command("acceptlogins", "makes the server accept logins variable true or false", AcceptLogins )
                    };
            }
        }

        public override string author
        {
            get { return "Luiz G. A. Roland"; }
        }

        public override string supportEmail
        {
            get { return "luizherege@gmail.com"; }
        }

        //Boolean defining if the server is accepting logins or not
        public static bool AcceptingLogin = true;
        //Processing Connection
        public static ConnectionService processingCon;
        //Waiting Queue, where connections wait to be processed
        public static Queue waitingQueue;

        //Reference to the RSAInterface plugin
        private RSAInterface.RSAInterface RSAref;
        //DebugMode
        private bool debug = _debugHandler._debugHandler.debugMode;
        //login Queue size
        private int loginQueueSize = 50;
        //waiting Queue size
        private int waitingQueueSize = 50; 

        //Command To turn on the boolean to accept logins
        public void AcceptLogins(string[] parts)
        {
            //validating the size of the arguments
            if (parts.Length != 1)
            {
                Interface.LogError("acceptlogins can only receive a single argument : 'true' | 'false'");
                return;
            }
            //validating the possibilites of the arguments
            if (parts[0] != "true" && parts[0] != "false")
            {
                Interface.LogError("acceptlogins can only receive a single argument : 'true' | 'false'");
                return;
            }

            if (parts[0] == "true")
            {
                //setting that the server is accepting logins and then 
                loginHandler.AcceptingLogin = true;
                //initializing the RSAref variable that would be unnedded otherwise
                RSAref = (RSAInterface.RSAInterface)PluginManager.plugins["RSAInterface"];
                //*DEBUGINFO
                if (debug)
                {
                    Interface.Log("RSAref on Login Handler is set");
                }//DEBUGEND*/

                Interface.Log("AcceptingLogin set to true");
            }
            else if (parts[0] == "false")
            {
                loginHandler.AcceptingLogin = false;
                Interface.Log("AcceptingLogin set to false");
            }
        }
        
        //The Function that will handle all the player connections        
        public void onPlayerConnectionEvent(ConnectionService con)
        {
            //*DEBUGINFO
            if (debug)
            {
                Interface.Log("Trying to login from " + con.id);
            }//DEBUGEND*/

            if (loginHandler.AcceptingLogin)
            {
                if (loginHandler.waitingQueue.Count > waitingQueueSize)
                {
                    //*DEBUGINFO
                    if (debug)
                    {
                        Interface.Log("Waiting queue full closing loginHandler");
                    }//DEBUGEND*/

                    //call the acceptlogins commmand with false
                    PluginManager.plugins["loginHandler"].commands[0].callback(new string[] { "false" });                    
                    con.Close();
                    return;
                }

                //*DEBUGINFO
                if (debug)
                {
                    Interface.Log("id : " + con.id + " Just Connected and is waiting for login");
                }//DEBUGEND*/

                loginHandler.waitingQueue.Enqueue(new WaitingQueueUnit(con));                               

                lock (processingCon)
                {
                    processingCon = con;
                    //*DEBUGINFO
                    if (debug)
                    {
                        Interface.Log("Logging in with " + con.id);
                    }//DEBUGEND*/

                     ConnectionService dequeuedCon = (ConnectionService)loginHandler.waitingQueue.Dequeue();
                    //else disconnect
                    if(dequeuedCon.id != processingCon.id)
                    {
                        //*DEBUGINFO
                        if (debug)
                        {
                            Interface.Log("Integrity problem while processing the loginqueue at: " + con.id);
                        }//DEBUGEND*/
                        con.Close();
                        return;
                    }
                }
            }
            else
            {
                Interface.Log("denying login from " + con.id);
                con.Close();
                return;
            }
        }

        public loginHandler()
        {
            //Attributing the onPlayerConnect event from darkRift server with the developed event 
            ConnectionService.onPlayerConnect += onPlayerConnectionEvent;
        }
    }
}
