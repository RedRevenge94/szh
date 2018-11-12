using DeviceController.AdvancedCommunication.Requests;
using DeviceController.BasicCommunication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DeviceController.Setter
{
    public class SetOutput :CommunicationBase
    {
        #region For other class

        protected int portId;
        protected int value;

        public SetOutput(string ip, int portId, int value)
            :base()
        {
            this.portId = portId;
            this.value = value;

            this.ip = ip;
            makeRequest();
            response = new Response(request.response);
            if (request.isOk)
                parse();
        }

        protected override void makeRequest()
        {
            request = new SetOutputRequest(ip, this.portId, this.value);
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
                dataIsOk = true;
            else
                dataIsOk = false;
        }

        #endregion
    }
}
