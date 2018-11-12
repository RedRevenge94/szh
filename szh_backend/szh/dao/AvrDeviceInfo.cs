using System;
using System.Collections.Generic;
using szh.cultivation;

namespace szh.dao {
    public class AvrDeviceInfo {

        public AvrDevice avrDevice;
        public bool online;

        public static List<AvrDeviceInfo> GetAvrDeviceInfo() {

            List<AvrDeviceInfo> avrDevicesInfoList = new List<AvrDeviceInfo>();

            foreach (var avrDevice in AvrDevice.GetAvrDevices()) {
                avrDevicesInfoList.Add(GetAvrDeviceInfo(avrDevice.id));
            }

            return avrDevicesInfoList;
        }

        public static AvrDeviceInfo GetAvrDeviceInfo(int avrDeviceId) {

            //TYMCZASOWE LOSOWANIE ONLINE
            Random gen = new Random();
            int prob = gen.Next(100);
            ////////////////////////////////

            AvrDeviceInfo avrDeviceInfo = new AvrDeviceInfo {
                avrDevice = AvrDevice.GetAvrDevice(avrDeviceId),
                online = false
            };

            try {
                avrDeviceInfo.online = (DateTime.Now - (DateTime)AvrDevice.GetAvrDevicesInTunnel(avrDeviceInfo.avrDevice.tunnel.id)[0].last_update).TotalSeconds < 60;
            } catch {
                ;
            }

            return avrDeviceInfo;
        }

    }
}
