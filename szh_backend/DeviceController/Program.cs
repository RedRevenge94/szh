using System;
using System.Collections.Generic;
using System.Threading;
using szh.cultivation;

namespace DeviceController {
    public class Program {
        public static void Main(string[] args) {

            List<AVRDeviceReader> avrDeviceList = new List<AVRDeviceReader>();

            SerialPortReader serialPortReader = new SerialPortReader();

            new Thread(() => { serialPortReader.Update(); }).Start();


            while (true) {
                foreach (AvrDevice avrDevice in AvrDevice.GetAvrDevicesSimple()) {

                    if (avrDeviceList.Find(x => x.id == avrDevice.id) == null) {
                        AVRDeviceReader avrDeviceReader = new AVRDeviceReader(avrDevice.id, avrDevice.ip);
                        avrDeviceReader.startReading(30000);
                        avrDeviceList.Add(avrDeviceReader);
                    } else {
                        avrDeviceList.Find(x => x.id == avrDevice.id).ip = avrDevice.ip;
                    }
                }

                /*foreach (var avrDevice in avrDeviceList) {
                    Console.WriteLine($"{avrDevice.id} {avrDevice.ip}");
                    Console.WriteLine($"temperature: {avrDevice.temperature}");
                    foreach (var adcValue in avrDevice.adc) {
                        Console.Write($"adc: {adcValue}");
                    }
                }*/

                Thread.Sleep(3000);
            }
        }
    }
}
