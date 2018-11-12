using DbManager.Db;
using System;

namespace szh {
    public class Entity {

        protected static int GetMax(string table) {
            string query = $"select max(id) from {table}";
            var maxResult = pgSqlSingleManager.ExecuteSQL(query);
            Int32.TryParse(maxResult[0]["max"], out int result);
            return result;
        }

    }
}
