using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PersistenceLib
{
    class DBConnect
    {
        private MySqlConnection connection;
        private String server;
        private String database;
        private String username;
        private String password;

        public String Server
        {
            get { return this.server; }
        }

        public String Database
        {
            get { return this.database; }
        }

        public DBConnect() {
            AppConfig config = new AppConfig();
            this.server = config.GetAppSetting("server");
            this.database = config.GetAppSetting("database");
            this.username = config.GetAppSetting("username");
            this.password = config.GetAppSetting("password");
            string connectionString = "SERVER=" + this.server + ";" + "DATABASE=" + this.database + ";" + "UID=" + this.username + ";" + "PASSWORD=" + this.password + ";";
            this.connection = new MySqlConnection(connectionString);
        }

        // Open MySql connection
        private bool openConnection()
        {
            try
            {
                this.connection.Open();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message + " (" + this.server + " - " + this.database + ")");
            }
        }

        // Close MySql connection
        private bool closeConnection()
        {
            try
            {
                this.connection.Close();
                return true;
            }
            catch (MySqlException ex)
            {
                throw new Exception(ex.Message + " (" + this.server + " - " + this.database + ")");
            }
        }

        // Insert statement
        public long? Insert(string query)
        {
            if (this.openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                command.ExecuteNonQuery();
                this.closeConnection();
                return command.LastInsertedId;
            }
            return null;
        }

        // Update statement
        public long? Update(string query)
        {
            if (this.openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                int result = command.ExecuteNonQuery();
                this.closeConnection();
                return result;
            }
            return null;
        }

        //Count statement
        public int Count(String query)
        {
            int count = -1;

            //Open Connection
            if (this.openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                count = int.Parse(command.ExecuteScalar() + "");
                this.closeConnection();
            }
            return count;
        }


        public List<List<String>> Select(String query)
        {
            List<List<String>> result = new List<List<String>>();

            if (this.openConnection() == true)
            {
                MySqlCommand command = new MySqlCommand(query, connection);
                MySqlDataReader dataReader = command.ExecuteReader();
                while (dataReader.Read())
                {
                    List<String> row = new List<String>();
                    for (int i = 0; i < dataReader.FieldCount; i++)
                    {
                        String field = dataReader.GetName(i);
                        row.Add(dataReader[field].ToString());
                    }
                    result.Add(row);
                }
                dataReader.Close();
                this.closeConnection();

                return result;
            }

            return null;
        }

    }
}
