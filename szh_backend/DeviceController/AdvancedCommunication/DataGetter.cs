using System.Threading;
using DeviceController.Getter;

namespace DeviceController {
    public class DataGetter {
        private string ip;
        public int[] adcValues { get; private set; }
        public float temperatureValue { get; private set; }

        public bool adcIsOK { get; private set; }
        public bool tempIsOK { get; private set; }
        public string adcErrorMessage { get; private set; }
        public string temperatureErrorMessage { get; private set; }
        public bool connectionNumberIsSet { get; private set; }
        public int connectionNumber { get; private set; }

        private const int delay = 500;
        private const int adcChannels = 6;

        public DataGetter(string ip) {
            this.ip = ip;
            adcValues = new int[adcChannels];
            getValuesFromDevice();
        }

        private void getValuesFromDevice() {
            for (int i = 0; i < 3; i++) {
                if (getADCFromDevice()) {
                    adcIsOK = true;
                    break;
                }
                Thread.Sleep(delay);
            }
            Thread.Sleep(delay);
            for (int i = 0; i < 3; i++) {
                if (getTempFromDevice()) {
                    tempIsOK = true;
                    break;
                }
                Thread.Sleep(delay);
            }
        }

        private bool getADCFromDevice() {
            GetAdc getAdc = new GetAdc(ip);
            if (getAdc.isOk) {
                adcValues = getAdc.adcValue;
                return true;
            } else {
                adcErrorMessage = getAdc.errorMessage;
                return false;
            }
        }

        private bool getConnectionNuber() {
            GetHello getHello = new GetHello(ip);
            if (getHello.isOk) {
                connectionNumber = getHello.connectionNumber;
                return true;
            } else {

                return false;
            }

        }

        private bool getTempFromDevice() {
            GetTemperature getTemperature = new GetTemperature(ip);
            if (getTemperature.isOk) {
                temperatureValue = getTemperature.tempValue;
                return true;
            } else {
                temperatureErrorMessage = getTemperature.errorMessage;
                return false;
            }

        }

        public int[] getADC() {
            return adcValues;
        }

        public float getTemperature() {
            return temperatureValue;
        }
    }
}
