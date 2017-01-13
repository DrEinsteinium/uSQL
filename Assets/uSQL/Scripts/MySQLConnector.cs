using UnityEngine;
using MySql.Data.MySqlClient;
using System;
using System.Diagnostics;
using System.Collections.Generic;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace uSQL.MySQL
{
    /// <summary>
    /// MySQLConnector is a scriptable object that can be made to store and serialize
    /// information used to connect to a database object. 
    /// Is also used to execute SQLStatement objects and return data in the form of 
    /// tables.
    /// </summary>
    [CreateAssetMenu(fileName = "MySQLConnector", menuName = "uSQL/MySQLConnector")]
    public class MySQLConnector : ScriptableObject
    {
        public bool logQueries;
        public string serverAddress;
        public int port;
        public string username;
        public string password;
        public string databaseName;
        public CharacterSet characterSet;

        private MySqlConnection connection;

        public string GetConnectionString()
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = this.serverAddress;
            builder.Port = (uint)port;
            builder.UserID = username;
            builder.Password = password;
            builder.Database = databaseName;
            builder.CharacterSet = characterSet.SqlConnectionType();
            return builder.ToString();
        }

        private void GenerateConnection()
        {
            this.connection = new MySqlConnection(this.GetConnectionString());
        }

        private void Open()
        {
            if (this.connection == null)
                GenerateConnection();
            connection.Open();
            if (this.logQueries)
                UnityEngine.Debug.Log("Opened connection to " + serverAddress + ":" + port);         
        }

        private void Close()
        {
            if (this.connection != null)
                connection.Close();
            if (this.logQueries)
                UnityEngine.Debug.Log("Closed connection to " + serverAddress + ":" + port);
        }

        public bool Ping(bool closeConnection = false)
        {
            this.Open();
            bool result = false;

            if (logQueries)
            {
                Stopwatch watch = new Stopwatch();
                watch.Start();
                result = this.connection.Ping();
                watch.Stop();

                UnityEngine.Debug.Log("Ping connection to " + serverAddress + ":" + port + ". Connection Time[" + watch.ElapsedMilliseconds + "ms]");
            }
            else result = this.connection.Ping();

            if (closeConnection)
                this.Close();
            return result;
        }

        /// <summary>
        /// Takes an SQL statement and executes it, returning an SQLTable object.
        /// </summary>
        /// <param name="statement">The SQL statement that should be executed.</param>
        /// <returns></returns>
        public SQLTable Execute(string statement)
        {
            MySqlDataReader reader = null;

            try
            {
                this.Open();
                if (this.Ping())
                {
                    SQLTable table = new SQLTable();
                    MySqlCommand command = new MySqlCommand();
                    command.CommandText = statement;

                    reader = command.ExecuteReader();

                    if (logQueries)
                        UnityEngine.Debug.Log("Executing Statement[" + command.CommandText + "]");

                    int colCount = reader.FieldCount;

                    while (reader.Read())//foreach row
                    {
                        Dictionary<string, object> row = table.AddRow();

                        for (int i = 0; i < colCount; i++)//go through each col
                            row.Add(reader.GetName(i), reader.GetValue(i));
                    }

                    reader.Close();
                    return table;
                }
            }
            catch (Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
            finally
            {
                this.Close();
                if (reader != null && !reader.IsClosed)
                    reader.Close();
            }

            return null;
        }


        /// <summary>
        /// Takes an SQL statement and executes it, returning an SQLTable object.
        /// </summary>
        /// <param name="statement">The SQL statement that should be executed.</param>
        /// <param name="watch">The time that elapses when the SQL command is executed.</param>
        /// <returns></returns>
        public SQLTable Execute(string statement, out Stopwatch watch)
        {
            watch = new Stopwatch();
            MySqlDataReader reader = null;

            try
            {
                this.Open();
                if(this.Ping())
                {
                    SQLTable table = new SQLTable();
                    MySqlCommand command = new MySqlCommand();
                    command.CommandText = statement;

                    watch.Start();
                    reader = command.ExecuteReader();
                    watch.Stop();

                    if(logQueries)
                    UnityEngine.Debug.Log("Executing Statement[" + command.CommandText +"]\nConnection Time[" + watch.ElapsedMilliseconds + "ms]");

                    int colCount = reader.FieldCount;

                    while(reader.Read())//foreach row
                    {
                        Dictionary<string, object> row = table.AddRow();

                        for (int i = 0; i < colCount; i++)//go through each col
                            row.Add(reader.GetName(i), reader.GetValue(i));
                    }

                    reader.Close();
                    return table;
                }
            }
            catch(Exception e)
            {
                UnityEngine.Debug.LogException(e);
            }
            finally
            {
                this.Close();
                if (reader != null && !reader.IsClosed)
                    reader.Close();    
            }

            return null;
        }

        /// <summary>
        /// Takes an SQL statement and executes it, returning an SQLTable object.
        /// </summary>
        /// <param name="statement">The SQL statement that should be executed.</param>
         /// <returns></returns>
        public SQLTable Execute(SQLStatement statement)
        {
            return this.Execute(statement.FinalizeStatement());
        }

        /// <summary>
        /// Takes an SQL statement and executes it, returning an SQLTable object.
        /// </summary>
        /// <param name="statement">The SQL statement that should be executed.</param>
        /// <param name="watch">The time that elapses when the SQL command is executed.</param>
        /// <returns></returns>
        public SQLTable Execute(SQLStatement statement, out Stopwatch watch)
        {
            return this.Execute(statement.FinalizeStatement(), out watch);
        }
    }



#if UNITY_EDITOR
    [CustomEditor(typeof(MySQLConnector))]
    public class MySQLConnectorEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            MySQLConnector target = this.target as MySQLConnector;

            EditorGUILayout.LabelField("Server Connection", EditorStyles.boldLabel);
            target.serverAddress = EditorGUILayout.TextField("Server Address", target.serverAddress);
            target.port = EditorGUILayout.IntField("Server Port", target.port);

            EditorGUILayout.Space();

            EditorGUILayout.LabelField("User Information", EditorStyles.boldLabel);
            target.username = EditorGUILayout.TextField("Username", target.username);
            target.password = EditorGUILayout.PasswordField("Password", target.password);

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Database Settings", EditorStyles.boldLabel);
            target.databaseName = EditorGUILayout.TextField("Database Name", target.databaseName);

            target.characterSet = (CharacterSet)EditorGUILayout.EnumPopup("Character Encoding", target.characterSet) ;

            EditorGUILayout.Space();
            EditorGUILayout.LabelField("Unity Settings", EditorStyles.boldLabel);
            target.logQueries = EditorGUILayout.Toggle("Log Database Activity", target.logQueries);
        }
    }
#endif
}