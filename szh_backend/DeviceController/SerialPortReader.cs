using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Ports;
using System.Threading;

namespace DeviceController {
    public class SerialPortReader {

        string path = "logs.txt";

        private string port = "COM3";
        private int baudRate = 38400;

        private List<String> logs;

        private SerialPort serialPort;

        public SerialPortReader() {
            try {
                serialPort = new SerialPort(port, baudRate);
                logs = new List<string>();
                serialPort.Open();
            } catch (Exception e) {
                ;
            }
        }

        ~SerialPortReader() {
            try {
                serialPort.Close();
            } catch { }
        }

        public void Update() {

            while (true) {

                try {
                    if (logs.Count > 100) {
                        SaveData();
                    }
                    logs.Add($"[{DateTime.Now}]: {serialPort.ReadLine()}");
                    Thread.Sleep(100);
                } catch {
                    break;
                }


            }

        }

        private void SaveData() {
            //zapisz i usun

            for (; logs.Count > 0;) {
                File.AppendAllText(path, logs[0]);
                logs.RemoveAt(0);
            }
        }

    }
}
