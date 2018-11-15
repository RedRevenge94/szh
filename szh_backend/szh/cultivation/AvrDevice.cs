using DbManager.Db;
using System;
using System.Collections.Generic;

namespace szh.cultivation {
    public class AvrDevice : Entity {

        #region -----------------DbValues-----------------
        public int id;
        public string ip;
        public Tunnel tunnel;
        public DateTime? last_update;
        #endregion

        public static List<AvrDevice> GetAvrDevicesInTunnel(int tunnelId) =>
            GetAvrDevices($"select * from devices.avr_device where tunnel = {tunnelId}");

        public static void Update(int avrDevice) {
            pgSqlSingleManager.ExecuteSQL($"update devices.avr_device set last_update = '{DateTime.Now}' where id = {avrDevice}");
        }

        public static void DeleteAvrDevice(int id) => pgSqlSingleManager.ExecuteSQL($"delete from devices.avr_device where id = {id}");

        public static List<AvrDevice> GetAvrDevicesSimple() {
            return GetAvrDevices($"select * from devices.avr_device", false);
        }

        public static List<AvrDevice> GetAvrDevices() => GetAvrDevices($"select * from devices.avr_device");

        public static AvrDevice GetAvrDevice(int avrDeviceId) => GetAvrDevices($"select * from devices.avr_device where id = {avrDeviceId}")[0];

        public static AvrDevice CreateAvrDevice(string ip, int tunnel) {
            AvrDevice lastAvrDevice = new AvrDevice() { id = GetMax("devices.avr_device") };

            pgSqlSingleManager.ExecuteSQL($"insert into devices.avr_device (id,ip,tunnel) " +
                $"values ({lastAvrDevice.id + 1},'{ip}',{tunnel})");

            return AvrDevice.GetAvrDevice(lastAvrDevice.id + 1);
        }

        private static List<AvrDevice> GetAvrDevices(string query) {
            return GetAvrDevices(query, true);
        }

        private static List<AvrDevice> GetAvrDevices(string query, bool getTunnelInfo) {
            List<AvrDevice> avrDevices = new List<AvrDevice>();

            foreach (var avrDevice in pgSqlSingleManager.ExecuteSQL(query)) {
                AvrDevice newAvrDevice = new AvrDevice() {
                    id = Int32.Parse(avrDevice["id"]),
                    ip = avrDevice["ip"],
                    last_update = null
                };

                if (getTunnelInfo) {
                    newAvrDevice.tunnel = Tunnel.GetTunnel(Int32.Parse(avrDevice["tunnel"]));
                }

                if (avrDevice["last_update"] != "") {
                    newAvrDevice.last_update = DateTime.Parse(avrDevice["last_update"]);
                }

                avrDevices.Add(newAvrDevice);
            }
            return avrDevices;
        }

    }
}
