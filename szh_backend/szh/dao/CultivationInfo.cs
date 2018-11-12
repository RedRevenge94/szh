using System;
using System.Collections.Generic;
using szh.cultivation;

namespace szh.dao {
    public class CultivationInfo {

        public Cultivation cultivation;
        public List<CultivationComment> cultivationComments;
        public bool online;

        public static List<CultivationInfo> GetCultivationInfo() {

            List<CultivationInfo> cultivationsInfoList = new List<CultivationInfo>();

            foreach (var cultivation in Cultivation.GetCultivations()) {
                cultivationsInfoList.Add(GetCultivationInfo(cultivation.id));
            }

            return cultivationsInfoList;
        }

        public static CultivationInfo GetCultivationInfo(int cultivationId) {

            CultivationInfo cultivationInfo = new CultivationInfo {
                cultivation = Cultivation.GetCultivation(cultivationId),
                cultivationComments = CultivationComment.GetCultivationComments(cultivationId),
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
