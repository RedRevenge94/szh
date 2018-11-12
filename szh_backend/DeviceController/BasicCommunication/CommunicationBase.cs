using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceController.AdvancedCommunication.Requests;
using DeviceController.BasicCommunication;

namespace DeviceController.BasicCommunication {
    public class CommunicationBase {
        protected string ip;
        protected Request request;
        protected Response response;
        protected bool dataIsOk;
        protected const int adcChannels = 6;

        protected String substring;

        #region For other class

        public bool isOk { get; private set; }
        public string errorMessage { get; protected set; }

        public CommunicationBase() {
            ;
        }

        public CommunicationBase(string ip) {
            this.ip = ip;
            makeRequest();
            response = new Response(request.response);
            if (request.isOk) {
                parse();
            } else {
                errorMessage = request.response;
            }

        }

        #endregion

        #region For constructor

        protected virtual void makeRequest() {
            request = new Request(ip);
        }

        #endregion

        #region Parsing

        protected void parse() {
            prepareParse();
            makeParse();
            finishParse();
        }

        protected virtual void prepareParse() {
            ;
        }

        protected virtual void makeParse() {
            ;
        }

        protected virtual void finishParse() {
            isOk = dataIsOk;
        }

        #endregion

        protected string cutDoctypeOfResponse(string response) {
            return response.Remove(0, 15);
        }

        protected string cutEndOfResponse(string response, int chars) {
            return response.Remove(response.Length - chars, chars);
        }
    }
}
