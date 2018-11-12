using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceController.AdvancedCommunication.Requests;
using DeviceController.BasicCommunication;

namespace DeviceController.Getter {

    public class GetAdc : CommunicationBase {
        private String[] substrings;

        #region For other class

        public int[] adcValue { get; private set; }

        public GetAdc(string ip)
            : base(ip) {
        }

        protected override void makeRequest() {
            adcValue = new int[adcChannels];
            request = new AdcRequest(ip);
        }

        #endregion

        #region Parsing

        protected override void prepareParse() {
            substring = cutEndOfResponse(response.response, 1);
            substring = cutDoctypeOfResponse(substring);
            substrings = cutString(substring);
        }

        protected override void makeParse() {
            tryParseAdc(substrings);
        }

        #endregion

        #region ADC parsing

        private void validAdc() {
            //niezrobione
            ;
        }

        private string[] cutString(string response) {
            Char delimiter = '#';
            return response.Split(delimiter);
        }

        private void tryParseAdc(String[] substrings) {
            dataIsOk = false;
            for (int i = 0; i < adcChannels; i++) {
                try {
                    adcValue[i] = int.Parse(substrings[i]);
                } catch (Exception e) {
                    adcValue[i] = 0;
                    errorMessage = $"{e}";
                }
            }

            dataIsOk = true;
        }



        #endregion

    }
}
