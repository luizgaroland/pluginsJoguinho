using System;
using System.Data;
using DarkRift;
using RSAInterface;
using Npgsql;

namespace dbInterface
{
    public class dbInterface : Plugin
    {
        //Basic puglin data
        public override string name
        {
            get { return "dbInterface"; }
        }

        public override string version
        {
            get { return "0.1"; }
        }

        public override Command[] commands
        {
            get { return new Command[0]; }
        }

        public override string author
        {
            get { return "Luiz G. A. Roland"; }
        }

        public override string supportEmail
        {
            get { return "luizherege@gmail.com"; }
        }

        //Database Interface class specific variables

        //Connection string parameters
        private string dbAdress = "127.0.0.1";
        private string databaseName = "joguinho";
        private string user = "postgres";
        private string password = "63435maldi";

        //The connection String Method
        private string connectionString()
        {
            return
            "Server=" + this.dbAdress + ";" +
            "Database=" + this.databaseName + ";" +
            "User ID=" + this.user + ";" +
            "Password=" + this.password + ";";
        }

        //System.Data connection
        private IDbConnection dbConnection;

        //System.Data reader
        private IDataReader reader;       

        //Commands and their descriptions

        //Login Command Sql string
        private string loginCommandSql = "SELECT iduser, login FROM users WHERE ";

        //login command
        public bool tryLoginCommand(string[] credentials)
        {
            //initializing and attributing commands to their sql strings counterparts
            //Test Command
            IDbCommand command = dbConnection.CreateCommand();
            command.CommandText = this.loginCommandSql + credentials[0] + credentials[1];

            //Test query execution			
            reader = command.ExecuteReader();

            if (reader.Read())
            {
                //Clean up
                reader.Close();
                reader = null;
                command.Dispose();
                command = null;

                //login Successful
                return true;
            }
            else
            {
                //Clean up
                reader.Close();
                reader = null;
                command.Dispose();
                command = null;

                //login Failed
                return false;
            }
        }

        //Class Constructor
        public dbInterface()
        {
            //Connection try catch
            try
            {
                //declaring the connection and trying to opening it
                dbConnection = new NpgsqlConnection(connectionString());
                dbConnection.Open();
            }
            catch (Exception msg)
            {
                //In case of connection failure
                Interface.Log(msg.Message);
                throw;
            }            

        }
    }
}

