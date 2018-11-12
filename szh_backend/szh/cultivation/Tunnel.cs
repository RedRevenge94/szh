using DbManager.Db;
using System;
using System.Collections.Generic;

namespace szh.cultivation {
    public class Tunnel {

        #region -----------------DbValues-----------------
        public int id;
        public string name;
        #endregion

        public static List<Tunnel> GetTunnels() {

            List<Tunnel> result = new List<Tunnel>();
            var sqlResults = pgSqlSingleManager.ExecuteSQL($"select * from cultivation.tunnels");

            foreach (var c_result in sqlResults) {
                result.Add(new Tunnel() {
                    id = Int32.Parse(c_result["id"]),
                    name = c_result["name"]
                });
            }

            return result;
        }

        public static Tunnel GetTunnel(int id) {
            var sqlresults = pgSqlSingleManager.ExecuteSQL($"select * from cultivation.tunnels where id = {id}");
            Tunnel resultTunnel = new Tunnel();

            foreach (var c_result in sqlresults) {
                resultTunnel = new Tunnel() {
                    id = Int32.Parse(c_result["id"]),
                    name = c_result["name"]
                };
            }

            return resultTunnel;
        }

        public static Tunnel CreateTunnel(string name) {
            var tunnelMaxResult = pgSqlSingleManager.ExecuteSQL($"select max(id) from cultivation.tunnels");
            Tunnel lastTunnel = new Tunnel();
            Int32.TryParse(tunnelMaxResult[0]["max"], out lastTunnel.id);

            pgSqlSingleManager.ExecuteSQL($"insert into cultivation.tunnels (id,name) values ({lastTunnel.id + 1},'{name}')");
            var tunnelResult = pgSqlSingleManager.ExecuteSQL($"select * from cultivation.tunnels where id = {lastTunnel.id + 1}");

            Tunnel newTunnel = new Tunnel {
                id = Int32.Parse(tunnelResult[0]["id"]),
                name = tunnelResult[0]["name"]
            };

            return newTunnel;
        }

        public static Tunnel UpdateTunnel(Tunnel oldTunnel, string newName) {
            oldTunnel.name = newName;
            pgSqlSingleManager.ExecuteSQL($"update cultivation.tunnels set name = '{newName}' where id = {oldTunnel.id} and name = '{oldTunnel.name}'");
            return GetTunnel(oldTunnel.id);
        }

        public static void DeleteTunnel(int id) => pgSqlSingleManager.ExecuteSQL($"delete from cultivation.tunnels where id = {id}");

    }
}