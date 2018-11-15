using Npgsql;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DbManager.Db {
    public class pgSqlSingleManager : DbBaseManager {

        private static List<NpgsqlConnection> npgsqlConnectionList;

        public pgSqlSingleManager() : base() {
            dbType = "Postgresql";
        }

        #region Connection String

        protected static void GetDbAccessData() {
            try {
                ip = dbAccess.Default.dbServer;
                port = dbAccess.Default.dbPort;
                dbName = dbAccess.Default.dbName;
                dbUser = dbAccess.Default.dbUser;
                dbPassword = dbAccess.Default.dbPassword;
            } catch { }
        }

        protected static void MakeConnectionString() => connectionString = $"Server={ip};Port={port};Database={dbName};User Id={dbUser};Password = {dbPassword};";

        #endregion

        protected new NpgsqlDataReader ExecuteQuery(string sql) {
            //base.ExecuteQuery(sql);
            try {
                // Execute a query
                NpgsqlCommand command = new NpgsqlCommand(sql, GetNpgSqlConnection());

                return command.ExecuteReader();
            } catch (Exception e) {
                Console.WriteLine(e.Message);
                return null;
            }
        }

        public void Destroy() {

            foreach (var connection in npgsqlConnectionList) {
                connection.Close();
            }

        }

        private static NpgsqlConnection GetNpgSqlConnection() {

            if (npgsqlConnectionList == null) {
                GetDbAccessData();
                MakeConnectionString();
                npgsqlConnectionList = new List<NpgsqlConnection>();
                npgsqlConnectionList.Add(new NpgsqlConnection(connectionString));
                npgsqlConnectionList[0].Open();
            }
            return npgsqlConnectionList[0];
        }

        public static List<NameValueCollection> ExecuteSQL(string sql) {

            NpgsqlConnection npgsqlConnection = GetNpgSqlConnection();
            List<NameValueCollection> result = new List<NameValueCollection>();

            lock (npgsqlConnection) {

                Console.WriteLine($"{DateTime.Now} : {sql}");

                //base.ExecuteQuery(sql);
                try {
                    // Execute a query
                    NpgsqlCommand command = new NpgsqlCommand(sql, npgsqlConnection);
                    NpgsqlDataReader sqlResult = command.ExecuteReader();

                    while (sqlResult.Read()) {
                        NameValueCollection c_result = new NameValueCollection();
                        for (int i = 0; i < sqlResult.FieldCount; i++) {
                            c_result[sqlResult.GetName(i)] = sqlResult[i].ToString();
                        }
                        result.Add(c_result);
                    }

                    sqlResult.Close();
                } catch (Exception e) {
                    Console.WriteLine(e.Message);
                }
            }
            return result;
        }

    }
}
