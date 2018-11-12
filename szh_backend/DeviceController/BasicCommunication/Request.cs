using System;
using System.Net;
using System.IO;

namespace DeviceController.BasicCommunication {
    public class Request {
        public static int allREquests = 0;

        public string ip { get; private set; }
        public string contextRoot { get; private set; }
        public int timeOut { get; private set; }
        public string response { get; private set; }
        public bool isOk { get; private set; }

        public const int defaultDelay = 2000;
        public const int defaultTimeOut = 2000;

        public Request(string ip, string contextRoot, int timeOut) {
            this.ip = ip;
            this.contextRoot = contextRoot;
            this.timeOut = timeOut;
            response = "";
            isOk = false;
            send();
        }

        public Request(string ip) {
            this.ip = ip;
            contextRoot = "";
            timeOut = defaultTimeOut;
            response = "";
            isOk = false;
            send();
        }

        private void send() {
            string path = setPath();
            try {
                withSending();
                TimedWebClient client = new TimedWebClient(timeOut);
                client.Headers.Add("user-agent", "C# Application");
                Stream data = client.OpenRead(path);
                StreamReader reader = new StreamReader(data);
                response = reader.ReadToEnd();
                data.Close();
                reader.Close();
                isOk = true;
            } catch (Exception e) {
                string errorMessage = $"Error in sending request to: {path} : \n\n {e}";
                Console.WriteLine(errorMessage);
                response = $"{e}";
            }
        }

        protected virtual void withSending() {
            //new Logger.Logger(createLogString());
            //allREquests++;
            //Console.WriteLine("Request nr:          " + allREquests);
        }

        protected string createLogString() {
            string log = DateTime.Now.ToString() + " [" + this.ToString() + "]: " + setPath();
            return log;
        }

        protected string setPath() {
            return "http://" + ip + "/" + contextRoot;
        }

    }

    public class TimedWebClient : WebClient {
        public int Timeout { get; set; }

        public TimedWebClient(int ms) {
            this.Timeout = ms;
        }

        protected override WebRequest GetWebRequest(Uri address) {
            var objWebRequest = base.GetWebRequest(address);
            objWebRequest.Timeout = this.Timeout;
            return objWebRequest;
        }
    }


}
