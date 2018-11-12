using System;

namespace DeviceController.BasicCommunication {
    public class Response {
        public string response { get; private set; }

        public Response() {
            response = "";
        }

        public Response(string response) {
            setResponse(response);
            withReading();
        }

        public void setResponse(string response) {
            this.response = response;
        }

        protected string createLogString() {
            string log = DateTime.Now.ToString() + " [" + this.ToString() + "]: " + response;
            return log;
        }

        protected virtual void withReading() {
            //new Logger.Logger(createLogString());
        }
    }
}
