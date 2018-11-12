using DbManager.Db;
using System;
using System.Collections.Generic;
using szh.cultivation;
using szh.measurement;

namespace szh.dao {
    public class TunnelInfo {

        public Tunnel tunnel;
        public List<Cultivation> cultivations;
        public bool online;
        public double? temperature;

        public static List<TunnelInfo> GetTunnelsInfo() {

            var tunnelResults = pgSqlSingleManager.ExecuteSQL($"select id from cultivation.tunnels");

            List<TunnelInfo> tunnelsInfoList = new List<TunnelInfo>();

            foreach (var tunnelResult in tunnelResults) {
                tunnelsInfoList.Add(GetTunnelInfo(Int32.Parse(tunnelResult["id"])));
            }
            tunnelsInfoList.Sort((x, y) => string.Compare(x.tunnel.name, y.tunnel.name));
            return tunnelsInfoList;
        }

        public static TunnelInfo GetTunnelInfo(int tunnelId) {

            TunnelInfo tunnelInfo = new TunnelInfo {
                //TunelInformacje
                tunnel = Tunnel.GetTunnel(tunnelId),
                //Hodowle
                cultivations = Cultivation.GetCultivationsInTunnel(tunnelId),
                //Online
                online = false,
                //Temperature
                temperature = null
            };
            try {
                tunnelInfo.online = (DateTime.Now - (DateTime)AvrDevice.GetAvrDevicesInTunnel(tunnelId)[0].last_update).TotalSeconds < 60;
                if (tunnelInfo.online) {
                    tunnelInfo.temperature = Measurement.GetTemperatureInTunnel(tunnelId).value;
                }
            } catch {
                ;
            }


            return tunnelInfo;
        }

    }
}
