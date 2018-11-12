using System;
using DeviceController.AdvancedCommunication.Requests;
using DeviceController.BasicCommunication;


namespace DeviceController.Getter {
    public class GetTemperature : CommunicationBase {

        #region For other class

        public float tempValue { get; private set; }

        public GetTemperature(string ip)
            : base(ip) {
        }

        protected override void makeRequest() {
            request = new TemperatureRequest(ip);
        }

        #endregion

        #region Parsing

        protected override void prepareParse() {
            substring = cutEndOfResponse(response.response, 3);
            substring = cutDoctypeOfResponse(substring);
        }

        protected override void makeParse() {
            tryParseTemperature(substring);

            if (!dataIsOk) {
                substring = changeToDot(substring);
                tryParseTemperature(substring);
            }

            if (!dataIsOk) {
                substring = changeFromDot(substring);
                tryParseTemperature(substring);
            }
        }

        #endregion

        #region Temperature parsing

        private void validTemperature() {
            if (tempValue == 0 || tempValue >= 100) {
                dataIsOk = false;
                errorMessage = $"Invalid temperature value = {tempValue}";
            } else
                dataIsOk = true;
        }

        private void tryParseTemperature(string textTemperaure) {
            try {
                tempValue = float.Parse(textTemperaure);
                validTemperature();
            } catch (Exception e) {
                errorMessage = $"{e}";
                dataIsOk = false;
            }
        }

        private string changeToDot(string textTemperature) {
            return textTemperature.Replace(',', '.');
        }

        private string changeFromDot(string textTemperature) {
            return textTemperature.Replace('.', ',');
        }

        #endregion
    }
}
