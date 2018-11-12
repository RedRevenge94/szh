using Npgsql;
using System;

namespace DbManager.Db {
    public class DbBaseManager {

        protected static string ip;
        protected static string port;
        protected static string dbName;
        protected static string dbUser;
        protected static string dbPassword;

        protected string dbType;
        protected static string connectionString;

        public DbBaseManager() {
            GetDbAccessData();
            MakeConnectionString();
        }

        protected virtual void GetDbAccessData() {
            ;
        }

        protected static NpgsqlDataReader ExecuteQuery(string query) {
            LogQuery(query);
            return null;
        }

        protected virtual void MakeConnectionString() {
            ;
        }

        private static void LogQuery(string query) {
            //Console.WriteLine($"\n[{DateTime.Now.ToString()}][{dbType} Query]: \n" + query);
        }
    }
}
