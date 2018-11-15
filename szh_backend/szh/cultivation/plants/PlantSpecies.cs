﻿using DbManager.Db;
using System;
using System.Collections.Generic;

namespace szh.cultivation.plants {
    public class PlantSpecies : Entity {

        #region -----------------DbValues-----------------
        public int id;
        public string name;
        #endregion

        public static PlantSpecies GetPlantSpecies(int id) {
            return GetPlantSpecies($"select * from cultivation.plant_species where id = {id}")[0];
        }

        public static PlantSpecies GetPlantSpeciesByName(string plantSpeciesName) {
            return GetPlantSpecies($"select * from cultivation.plant_species where name = '{plantSpeciesName}'")[0];
        }

        public static List<PlantSpecies> GetPlantSpecies() {
            return GetPlantSpecies($"select * from cultivation.plant_species");
        }

        public static List<Variety> GetVarietiesForPlant(int plantSpeciesId) {
            return Variety.GetVarietyForPlantSpecies(plantSpeciesId);
        }

        private static List<PlantSpecies> GetPlantSpecies(string query) {
            List<PlantSpecies> plantSpecies = new List<PlantSpecies>();

            foreach (var plantSpec in pgSqlSingleManager.ExecuteSQL(query)) {
                PlantSpecies newPlantSpec = new PlantSpecies {
                    id = Int32.Parse(plantSpec["id"]),
                    name = plantSpec["name"]
                };
                plantSpecies.Add(newPlantSpec);
            }

            return plantSpecies;
        }

    }
}
