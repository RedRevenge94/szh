using DeviceController.BasicCommunication;

namespace DeviceController.AdvancedCommunication.Requests
{
    public class TemperatureRequest : Request
    {
        const string context = "gett";

        public TemperatureRequest(string ip, int timeOut)
            :base(ip, context, timeOut)
        {

        }

        public TemperatureRequest(string ip)
            : base(ip, context, defaultDelay)
        {

        }
    }
}
