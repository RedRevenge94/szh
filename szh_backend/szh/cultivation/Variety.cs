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
        public Plant plant;
        #endregion

        public static Variety CreateVariety(string name, int plant_id) {
            Variety lastVariety = new Variety() { id = GetMax("cultivation.variety") };

            pgSqlSingleManager.ExecuteSQL($"insert into cultivation.variety (id,name,plant) " +
                $"values ({lastVariety.id + 1},'{name}',{plant_id})");
            var varietyResult = pgSqlSingleManager.ExecuteSQL($"select * from cultivation.variety where id = {lastVariety.id + 1}");

            Variety newVariety = new Variety {
                id = Int32.Parse(varietyResult[0]["id"]),
                name = varietyResult[0]["name"],
                plant = Plant.GetPlant(Int32.Parse(varietyResult[0]["plant"]))
            };

            return newVariety;
        }

        public static Variety GetVariety(int varietyId) => GetVarieties($"select * from cultivation.variety where id = {varietyId}")[0];

        public static List<Variety> GetVarietiesForPlant(int plantId) => GetVarieties($"select * from cultivation.variety where plant = {plantId}");

        private static List<Variety> GetVarieties(string query) {
            List<Variety> varieties = new List<Variety>();

            foreach (var variety in pgSqlSingleManager.ExecuteSQL(query)) {
                Variety newVariety = new Variety() {
                    id = Int32.Parse(variety["id"]),
                    name = variety["name"],
                    plant = Plant.GetPlant(Int32.Parse(variety["plant"]))
                };

                varieties.Add(newVariety);
            }
            return varieties;
        }

    }
}
