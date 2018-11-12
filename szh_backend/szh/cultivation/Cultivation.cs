using DbManager.Db;
using System;
using System.Collections.Generic;

namespace szh.cultivation {
    public class Cultivation : Entity {

        #region -----------------DbValues-----------------
        public int id;
        public string name;
        public Plant plant;
        public Variety variety;
        public int pieces;
        public Tunnel tunnel;
        public DateTime start_date;
        public DateTime? end_date;
        #endregion

        public static Cultivation GetCultivation(int cultivationId) => GetCultivations($"select * from cultivation.cultivation where id = {cultivationId}")[0];

        public static List<Cultivation> GetCultivations() => GetCultivations($"select * from cultivation.cultivation");

        public static List<Cultivation> GetCultivationsInTunnel(int tunnelId) => GetCultivations($"select * from cultivation.cultivation where tunnel = {tunnelId}");

        #region Creating Cultivation

        public static Cultivation CreateCultivation(Cultivation cultivation) {

            if (cultivation.variety != null && cultivation.variety.id > 0) {
                return CreateCultivation(cultivation.name, cultivation.plant.id, cultivation.variety.id, cultivation.pieces, cultivation.tunnel.id, cultivation.start_date);
            } else {
                return CreateCultivation(cultivation.name, cultivation.plant.id, cultivation.pieces, cultivation.tunnel.id, cultivation.start_date);
            }
        }

        public static Cultivation CreateCultivation(string name, int plant, int variety, int pieces, int tunnel, DateTime start_date) {

            Cultivation lastCultivation = new Cultivation() { id = GetMax("cultivation.cultivation") };

            pgSqlSingleManager.ExecuteSQL($"insert into cultivation.cultivation (id,name,plant,variety,pieces,tunnel,start_date) " +
                $"values ({lastCultivation.id + 1},'{name}',{plant},{variety},{pieces},{tunnel},'{start_date}')");
            var cultivationResult = pgSqlSingleManager.ExecuteSQL($"select * from cultivation.cultivation where id = {lastCultivation.id + 1}");

            Cultivation newCultivation = new Cultivation {
                id = Int32.Parse(cultivationResult[0]["id"]),
                name = cultivationResult[0]["name"],
                pieces = Int32.Parse(cultivationResult[0]["pieces"]),
                plant = Plant.GetPlant(Int32.Parse(cultivationResult[0]["plant"])),
                variety = Variety.GetVariety(Int32.Parse(cultivationResult[0]["variety"])),
                tunnel = Tunnel.GetTunnel(Int32.Parse(cultivationResult[0]["tunnel"])),
                start_date = DateTime.Parse(cultivationResult[0]["start_date"])
            };

            return newCultivation;
        }

        public static Cultivation CreateCultivation(string name, int plant, int pieces, int tunnel, DateTime start_date) {
            Cultivation lastCultivation = new Cultivation() { id = GetMax("cultivation.cultivation") };

            pgSqlSingleManager.ExecuteSQL($"insert into cultivation.cultivation (id,name,plant,pieces,tunnel,start_date) " +
                $"values ({lastCultivation.id + 1},'{name}',{plant},{pieces},{tunnel},'{start_date}')");
            var cultivationResult = pgSqlSingleManager.ExecuteSQL($"select * from cultivation.cultivation where id = {lastCultivation.id + 1}");

            Cultivation newCultivation = new Cultivation {
                id = Int32.Parse(cultivationResult[0]["id"]),
                name = cultivationResult[0]["name"],
                pieces = Int32.Parse(cultivationResult[0]["pieces"]),
                plant = Plant.GetPlant(Int32.Parse(cultivationResult[0]["plant"])),
                tunnel = Tunnel.GetTunnel(Int32.Parse(cultivationResult[0]["tunnel"])),
                start_date = DateTime.Parse(cultivationResult[0]["start_date"])
            };

            return newCultivation;
        }

        #endregion

        public static Cultivation UpdateCultivation(Cultivation cultivation) {

            string endDateValue = "";
            if (cultivation.end_date == null) {
                endDateValue = "null";
            } else {
                endDateValue = $"'{cultivation.end_date}'";
            }


            string sql = $"update cultivation.cultivation set " +
                $"name = '{cultivation.name}', pieces = {cultivation.pieces}, tunnel = {cultivation.tunnel.id}," +
                $" start_date = '{cultivation.start_date}', end_date = {endDateValue} " +
                $"where id = {cultivation.id} ";

            pgSqlSingleManager.ExecuteSQL(sql);
            var cultivationResult = pgSqlSingleManager.ExecuteSQL($"select * from cultivation.cultivation where id = {cultivation.id}");

            Cultivation updatedCultivation = new Cultivation {
                id = Int32.Parse(cultivationResult[0]["id"]),
                name = cultivationResult[0]["name"],
                pieces = Int32.Parse(cultivationResult[0]["pieces"]),
                plant = Plant.GetPlant(Int32.Parse(cultivationResult[0]["plant"])),
                tunnel = Tunnel.GetTunnel(Int32.Parse(cultivationResult[0]["tunnel"])),
                start_date = DateTime.Parse(cultivationResult[0]["start_date"])
            };

            return updatedCultivation;
        }

        public static void DeleteFromTunnel(int id) => pgSqlSingleManager.ExecuteSQL($"delete from cultivation.cultivation where id = {id}");

        private static List<Cultivation> GetCultivations(string query) {
            List<Cultivation> cultivations = new List<Cultivation>();

            foreach (var cultivation in pgSqlSingleManager.ExecuteSQL(query)) {
                Cultivation newCultivation = new Cultivation() {
                    id = Int32.Parse(cultivation["id"]),
                    name = cultivation["name"],
                    plant = Plant.GetPlant(Int32.Parse(cultivation["plant"])),
                    variety = Variety.GetVariety(Int32.Parse(cultivation["variety"])),
                    pieces = Int32.Parse(cultivation["pieces"]),
                    tunnel = Tunnel.GetTunnel(Int32.Parse(cultivation["tunnel"])),
                    start_date = DateTime.Parse(cultivation["start_date"]),
                    end_date = null
                };
                if (cultivation["end_date"] != "") {
                    newCultivation.end_date = DateTime.Parse(cultivation["end_date"]);
                }

                cultivations.Add(newCultivation);
            }
            return cultivations;
        }
    }
}
