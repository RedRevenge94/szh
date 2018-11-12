using DeviceController.Setter;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DeviceController
{
    public class DataSetter
    {
        private string ip;
        private const int delay = 500;

        public DataSetter(string ip)
        {
            this.ip = ip;
        }

        public void setOutput(int port, int value)
        {
            for (int i = 0; i < 3; i++)
            {
                SetOutput setOutput = new SetOutput(ip,port,value);
                if (setOutput.isOk)
                    break;
                Thread.Sleep(delay);
            }
        }

    }
}
