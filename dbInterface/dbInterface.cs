using System;
using System.Data;
using DarkRift;
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

        /// Test Command
        /// Testing Purpose command, it just select a test table		
        private IDbCommand testCommand;
        //Test Command Sql string
        string testCommandSql = "SELECT iduser, login FROM users";

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

            //initializing and attributing commands to their sql strings counterparts
            //Test Command
            testCommand = dbConnection.CreateCommand();
            testCommand.CommandText = testCommandSql;

            //Test query execution			
            reader = testCommand.ExecuteReader();

            while (reader.Read())
            {
                string id = reader.GetInt32(reader.GetOrdinal("iduser")).ToString();
                string login = reader.GetString(reader.GetOrdinal("login"));

                Interface.Log("ID : " + id + " login: " + login);
            }

            //Clean up
            reader.Close();
            reader = null;
            testCommand.Dispose();
            testCommand = null;
            dbConnection.Dispose();
            dbConnection = null;

        }
    }
}

