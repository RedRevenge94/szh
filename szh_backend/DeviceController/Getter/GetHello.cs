using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DeviceController.AdvancedCommunication.Requests;
using DeviceController.BasicCommunication;

namespace DeviceController.Getter
{
    public class GetHello :CommunicationBase
    {
        #region For other class

        public int idOfDevice;
        public int connectionNumber { get; private set; }

        public GetHello(string ip)
            :base(ip)
        {
            idOfDevice = 0;
        }

        protected override void makeRequest()
        {
            request = new HelloRequest(ip);
        }

        #endregion

        #region Parsing

        protected override void prepareParse()
        {
            substring = cutDoctypeOfResponse(response.response);
        }

        protected override void makeParse()
        {
            tryParseHello();
        }

        #endregion

        #region Hello parsing

        private void tryParseHello()
        {
            if (substring[1] == 'O' && substring[2] == 'K')
            {
                dataIsOk = true;
                this.connectionNumber = substring[4] - 48;
            }
            else
                dataIsOk = false;
        }

        #endregion
    }
}
