using DeviceController.BasicCommunication;

namespace DeviceController.AdvancedCommunication.Requests
{
    public class SetOutputRequest :Request
    {
        const string context = "set";

        public SetOutputRequest(string ip, int timeOut,int portId, int value)
            :base(ip, context + portId.ToString() + value.ToString(), timeOut)
        {

        }

        public SetOutputRequest(string ip, int portId, int value)
            : base(ip, context + portId.ToString() + value.ToString(), defaultDelay)
        {

        }
    }
}
