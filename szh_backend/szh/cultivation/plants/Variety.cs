using DbManager.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace szh.cultivation {
    public class Variety : Entity {

        #region -----------------DbValues-----------------
        public int id;
        public string name;
        #endregion

        public static List<Variety> GetVarietyForPlantSpecies(int plantSpeciesId) {
            return GetVarieties($"select * from cultivation.variety where id in (select plant_variety from cultivation.plants where plant_species = {plantSpeciesId})");
        }

        public static Variety CreateVariety(int plantSpeciesId, string name) {

            pgSqlSingleManager.ExecuteSQL($"insert into cultivation.variety (name) " +
                $"values ('{name}')");

            pgSqlSingleManager.ExecuteSQL($"insert into cultivation.plants (plant_species, plant_variety) " +
                $"values ({plantSpeciesId}, (select id from cultivation.variety where name = '{name}'))");

            return GetVarieties($"select * from cultivation.variety where name = '{name}'")[0];
        }

        public static Variety GetVariety(int varietyId) => GetVarieties($"select * from cultivation.variety where id = {varietyId}")[0];

        private static List<Variety> GetVarieties(string query) {
            List<Variety> varieties = new List<Variety>();

            foreach (var variety in pgSqlSingleManager.ExecuteSQL(query)) {
                Variety newVariety = new Variety() {
                    id = Int32.Parse(variety["id"]),
                    name = variety["name"]
                };

                varieties.Add(newVariety);
            }
            return varieties;
        }

    }
}
