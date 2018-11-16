using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace szh.cultivation.plants {
    public class PlantSpeciesInfo : PlantSpecies {

        #region -----------------DbValues-----------------
        public List<Variety> varieties;
        #endregion

        public static List<PlantSpeciesInfo> GetPlantSpeciesInfo() {

            List<PlantSpeciesInfo> plantSpeciesInfoList = new List<PlantSpeciesInfo>();
            List<PlantSpecies> plantSpecies = GetPlantSpecies();

            foreach (var plant in plantSpecies) {
                PlantSpeciesInfo plantSpeciesInfo = new PlantSpeciesInfo {
                    id = plant.id,
                    name = plant.name,
                    varieties = GetVarietiesForPlant(plant.id)
                };

                plantSpeciesInfoList.Add(plantSpeciesInfo);
            }
            return plantSpeciesInfoList;
        }

        public static PlantSpeciesInfo GetPlantSpeciesInfo(int platnSpeciesId) {

            PlantSpecies plantSpecies = GetPlantSpecies(platnSpeciesId);

            PlantSpeciesInfo plantSpeciesInfo = new PlantSpeciesInfo {
                id = plantSpecies.id,
                name = plantSpecies.name,
                varieties = GetVarietiesForPlant(plantSpecies.id)
            };
            return plantSpeciesInfo;
        }
    }
}
