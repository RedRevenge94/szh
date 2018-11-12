using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using szh.cultivation;
using szh.measurement;

namespace api.Controllers {
    [Route("api/[controller]")]
    public class DbDataController : Controller {

        private void ClearDataBase() {

            foreach (Tunnel tunnel in Tunnel.GetTunnels()) {

                foreach (Cultivation breeding in Cultivation.GetCultivationsInTunnel(tunnel.id)) {
                    CultivationComment.DeleteFromBreeding(breeding.id);
                    Cultivation.DeleteFromTunnel(breeding.id);
                }

                foreach (AvrDevice avr_device in AvrDevice.GetAvrDevicesInTunnel(tunnel.id)) {
                    Measurement.DeleteMeasurements();
                    AvrDevice.DeleteAvrDevice(avr_device.id);
                }

                Tunnel.DeleteTunnel(tunnel.id);
            }

            Plant.DeletePlants();
        }

        [HttpPut("{prepare}")]
        public IActionResult PrepareDataBase(bool prepare) {

            ClearDataBase();

            if (prepare) {
                Random gen = new Random();

                List<string> plantsNames = new List<string>();
                plantsNames.Add("Awokado");
                plantsNames.Add("Lychee");
                plantsNames.Add("Cytryna");
                plantsNames.Add("Pomarańcza");
                plantsNames.Add("Mango");

                foreach (string plantName in plantsNames) {
                    int newPlantId = Plant.CreatePlant(plantName).id;

                    for (int i = 0; i < 5; i++) {
                        Variety.CreateVariety($"{plantName}_variety_{i}", newPlantId);
                    }
                }

                for (int i = 0; i < 4; i++) {
                    int id = Tunnel.CreateTunnel($"Tunnel{i}").id;
                    AvrDevice.CreateAvrDevice($"localhost/espSim/{i + 10}", id);

                    for (int j = 0; j < gen.Next(1, 6); j++) {
                        int breedingId = Cultivation.CreateCultivation($"Hodowla {i}/{j}", gen.Next(1, plantsNames.Count), gen.Next(1, 6), i + i * j, id, DateTime.Now).id;
                        for (int k = 0; k < gen.Next(0, 10); k++) {
                            CultivationComment.AddCultivationComents($"Komentarz do hodowli { i + i * j * k}", breedingId);
                        }
                    }
                }
            }

            return new NoContentResult();
        }

    }
}
