using DeviceController.BasicCommunication;

namespace DeviceController.AdvancedCommunication.Requests
{
    public class HelloRequest : Request
    {

        public HelloRequest(string ip, int timeOut)
            :base(ip,"",timeOut)
        {

        }

        public HelloRequest(string ip)
            : base(ip, "", defaultDelay)
        {

        }
    }
}
