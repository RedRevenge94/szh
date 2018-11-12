using DeviceDeviceController;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using szh.measurement;

namespace DeviceController {
    public class AVRDeviceReader {
        public int id { get; private set; }
        public string ip { get; set; }
        public float temperature { get; private set; }
        public int[] adc { get; private set; }
        public int connectionNumber { get; private set; }
        public bool connectIsOk { get; private set; }

        private const int adcChannels = 6;

        Timer avrTimer;
        int timePeriod = 90000;
        public bool isReading { get; private set; }
        private bool isReadingNow;


        //Wyjscie
        Thread sendConf;
        public int portToSet { get; private set; }
        public int valueToSet { get; private set; }

        public AVRDeviceReader(int id, string ip) {
            isReadingNow = false;
            connectIsOk = false;
            this.ip = ip;
            this.id = id;
            adc = new int[adcChannels];
            temperature = 0;
            communicationLogs = new List<string>();
            lastDateCheck = DateTime.Now;
        }

        private void getDataFromDevice() {
            DataGetter getter = new DataGetter(ip);
            if (getter.tempIsOK) {
                temperature = getter.temperatureValue;
            }

            if (getter.adcIsOK) {
                adc = getter.adcValues;
            }

            if (getter.tempIsOK && getter.adcIsOK) {
                connectIsOk = true;
            } else {

                connectIsOk = false;
                if (!getter.tempIsOK) {
                    communicationLogs.Add($"\t[{DateTime.Now}]:  Temperature error message: {getter.temperatureErrorMessage} <br />");
                }
                if (!getter.adcIsOK) {
                    communicationLogs.Add($"\t[{DateTime.Now}]:  ADC error message: {getter.adcErrorMessage} <br />");
                }
            }
        }

        private void onOffDevice() {
            //Czasowe sterowanie wyjsciami
            DateTime dt = DateTime.Now;

            /*if (dt.Hour >= Conf.hOn0 && dt.Hour < Conf.hOff0)
                setOutput(0, 1);
            else
                setOutput(0, 0);*/
        }

        #region Timer

        private int changeStateId;
        private List<string> communicationLogs;

        private bool communicationStatus = false;
        private bool CommunicationStatus {
            get {
                return communicationStatus;
            }
            set {
                communicationStatus = value;
                if (communicationStatus) {
                    communicationLogs.Add("<br />");
                }

                communicationLogs.Add($"[{DateTime.Now}] #{++changeStateId} : Status odczytu danych z urządzenia ({ip}) został zmieniony na {communicationStatus}. <br />");

                if (communicationStatus) {
                    communicationLogs.Add("<br />");
                }
            }
        }

        private DateTime lastDateCheck;
        private void timerTick(object obj) {
            isReadingNow = true;
            getDataFromDevice();
            isReadingNow = false;

            //Obsluga wyjscia
            //onOffDevice();

            //Zapis danych
            if (connectIsOk) {
                Measurement.AddMeasurement(1, id, temperature);
                Measurement.AddMeasurement(2, id, adc[5]);

                if (!CommunicationStatus) {
                    CommunicationStatus = true;
                }

            } else {
                if (CommunicationStatus) {
                    CommunicationStatus = false;
                }
            }

            if (lastDateCheck.Hour % 2 != 0 && DateTime.Now.Hour % 2 == 0) {
                SendEmailWithLogs();
            }
            lastDateCheck = DateTime.Now;
        }

        private void SendEmailWithLogs() {

            string emailContent = "";

            foreach (string log in communicationLogs) {
                emailContent += log;
            }

            for (; communicationLogs.Count > 0;) {
                communicationLogs.RemoveAt(0);
            }
            changeStateId = 0;

            new MailSending().sendMessage(emailContent);
        }

        public void startReading(int time) {
            timePeriod = time;
            stopReading();
            avrTimer = new Timer(new TimerCallback(timerTick),
             this, 0, timePeriod);
            isReading = true;
        }

        public void stopReading() {
            isReading = false;
            if (avrTimer != null)
                avrTimer.Dispose();
        }

        #endregion

        #region Wyjścia

        private void setOutput() {
            while (true) {
                if (!isReadingNow) {
                    if (portToSet >= 0 && valueToSet >= 0) {
                        DataSetter setter = new DataSetter(ip);
                        setter.setOutput(portToSet, valueToSet);
                    }
                    portToSet = -1;
                    valueToSet = -1;
                    break;
                }
                Thread.Sleep(1000);
            }
            sendConf.Abort();
        }

        public void setOutput(int port, int value) {
            portToSet = port;
            valueToSet = value;
            sendConf = new Thread(new ThreadStart(this.setOutput));
            sendConf.Start();
        }

        #endregion

    }
}
