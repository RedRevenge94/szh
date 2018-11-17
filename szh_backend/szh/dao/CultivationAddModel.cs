using DbManager.Db;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using szh.cultivation;
using szh.cultivation.plants;

namespace szh.dao {
    public class CultivationAddModel {

        public string name;
        public int plantSpeciesId;
        public int varietyId;
        public int pieces;
        public int tunnelId;
        public DateTime start_date;

        public static Cultivation CreateCultivation(CultivationAddModel cultivation) {

            pgSqlSingleManager.ExecuteSQL($"insert into cultivation.cultivation (name,plant,pieces,tunnel,start_date) " +
                $"values ('{cultivation.name}', (select id from cultivation.plants where plant_species = {cultivation.plantSpeciesId} " +
                $"and plant_variety = {cultivation.varietyId}) ,{cultivation.pieces},{cultivation.tunnelId},'{cultivation.start_date}')");
            var cultivationResult = pgSqlSingleManager.ExecuteSQL($"select * from cultivation.cultivation where name = '{cultivation.name}'");

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

    }
}
