using System;
using DarkRift;
using _debugHandle;
using RSAInterface;

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
        public static bool AcceptingLogin = false;
        //Reference to the RSAInterface plugin
        private RSAInterface.RSAInterface RSAref;
        //Reference to the debugMode
        private bool debug = ((_debugHandle._debugHandle)PluginManager.plugins["_debughandle"]).debugMode;

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
                loginHandler.AcceptingLogin = true;
                RSAref = (RSAInterface.RSAInterface)PluginManager.plugins["RSAInterface"];
                if (debug)
                {
                    Interface.Log("RSAref on Login Handler is set");
                }
                Interface.Log("AcceptingLogin set to true");
            }
            else if (parts[0] == "false")
            {
                loginHandler.AcceptingLogin = false;
                Interface.Log("AcceptingLogin set to false");
            }

        }

        public loginHandler()
        {

        }
    }
}
