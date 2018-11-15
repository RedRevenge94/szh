using DbManager.Db;
using System;
using System.Collections.Generic;

namespace szh.cultivation.plants {
    public class Plant : Entity {

        #region -----------------DbValues-----------------
        public int id;
        public PlantSpecies plantSpecies;
        public Variety variety;
        #endregion

        public static Plant CreatePlant(string name) {

            pgSqlSingleManager.ExecuteSQL($"insert into cultivation.plant_species (name) " +
                $"values ('{name}')");

            pgSqlSingleManager.ExecuteSQL($"insert into cultivation.plants (plant_species) " +
                $"select id from cultivation.plant_species where name = '{name}'");

            return GetPlants($"select * from cultivation.plants where " +
                $"plant_species in ( select id from cultivation.plant_species where name = '{name}')")[0];
        }

        public static List<Plant> GetPlants() {
            return GetPlants("select * from cultivation.plants");
        }

        public static Plant GetPlant(int plantId) {
            return GetPlants($"select * from cultivation.plants where id = {plantId}")[0];
        }

        public static int GetMinIdPlant() {
            return Int32.Parse(pgSqlSingleManager.ExecuteSQL($"select min(id) from cultivation.plants")[0]["min"]);
        }

        public static int GetMaxIdPlant() {
            return Int32.Parse(pgSqlSingleManager.ExecuteSQL($"select max(id) from cultivation.plants")[0]["max"]);
        }

        public static void DeletePlants() {
            GetPlants($"delete from cultivation.plants");
            GetPlants($"delete from cultivation.plant_species");
            GetPlants($"delete from cultivation.variety");

        }

        private static List<Plant> GetPlants(string query) {
            List<Plant> plants = new List<Plant>();

            foreach (var plant in pgSqlSingleManager.ExecuteSQL(query)) {
                Plant newPlant = new Plant {
                    id = Int32.Parse(plant["id"]),
                    plantSpecies = PlantSpecies.GetPlantSpecies(Int32.Parse(plant["plant_species"]))
                };

                try {
                    newPlant.variety = Variety.GetVariety(Int32.Parse(plant["plant_variety"]));
                } catch { }

                plants.Add(newPlant);
            }

            return plants;
        }

    }
}
