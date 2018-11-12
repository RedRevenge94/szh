using System;
using System.Collections.Generic;
using szh.cultivation;

namespace szh.dao {
    public class CultivationInfoBasic {
        public Cultivation cultivation;
        public bool online;

        public static List<CultivationInfoBasic> GetCultivationInfo() {

            List<CultivationInfoBasic> cultivationsInfoList = new List<CultivationInfoBasic>();

            foreach (var cultivation in Cultivation.GetCultivations()) {
                cultivationsInfoList.Add(GetCultivationInfo(cultivation.id));
            }

            return cultivationsInfoList;
        }

        public static CultivationInfoBasic GetCultivationInfo(int cultivationId) {

            CultivationInfoBasic cultivationInfo = new CultivationInfoBasic {
                cultivation = Cultivation.GetCultivation(cultivationId),
                //Online
                online = false
            };

            try {
                cultivationInfo.online = (DateTime.Now - (DateTime)AvrDevice.GetAvrDevicesInTunnel(cultivationInfo.cultivation.tunnel.id)[0].last_update).TotalSeconds < 60;
            } catch {
                ;
            }

            return cultivationInfo;
        }
    }
}
