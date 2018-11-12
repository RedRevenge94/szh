using DeviceController.BasicCommunication;

namespace DeviceController.AdvancedCommunication.Requests
{
    public class AdcRequest : Request
    {
        const string context = "getm";

        public AdcRequest(string ip, int timeOut)
            :base(ip, context, timeOut)
        {

        }

        public AdcRequest(string ip)
            : base(ip, context, defaultDelay)
        {

        }
    }
}