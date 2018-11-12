using DbManager.Db;
using System;
using System.Collections.Generic;

namespace szh.cultivation {
    public class Plant : Entity {

        #region -----------------DbValues-----------------
        public int id;
        public string name;
        #endregion

        public static Plant CreatePlant(string name) {
            Plant lastPlant = new Plant() { id = GetMax("cultivation.plants") };

            pgSqlSingleManager.ExecuteSQL($"insert into cultivation.plants (id,name) " +
                $"values ({lastPlant.id + 1},'{name}')");
            var plantResult = pgSqlSingleManager.ExecuteSQL($"select * from cultivation.plants where id = {lastPlant.id + 1}");

            Plant newPlant = new Plant {
                id = Int32.Parse(plantResult[0]["id"]),
                name = plantResult[0]["name"]
            };

            return newPlant;
        }

        public static List<Plant> GetPlants() => GetPlants($"select * from cultivation.plants");

        public static Plant GetPlant(int plantId) => GetPlants($"select * from cultivation.plants where id = {plantId}")[0];

        public static void DeletePlants() {
            GetPlants($"delete from cultivation.plants");
        }

        private static List<Plant> GetPlants(string query) {
            List<Plant> plants = new List<Plant>();

            foreach (var plant in pgSqlSingleManager.ExecuteSQL(query)) {
                Plant newPlant = new Plant() {
                    id = Int32.Parse(plant["id"]),
                    name = plant["name"]
                };

                plants.Add(newPlant);
            }
            return plants;
        }

    }
}
