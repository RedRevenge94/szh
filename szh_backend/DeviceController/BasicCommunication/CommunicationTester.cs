using System;
using System.Threading;

namespace DeviceController.BasicCommunication
{
    public class CommunicationTester
    {
        public CommunicationTester(string ip, string context, int timeOut, int delay, int testNumber)
        {
            int licznikPoprawnych = 0;

            for (int i = 0; i < testNumber; i++)
            {
                Request request = new Request(ip, context, timeOut);
                if (request.isOk)
                    licznikPoprawnych++;

                Response response = new Response(request.response);
                Thread.Sleep(delay);
            }

            Console.WriteLine("Eksperyment zakończono!");
            float procent = ((float)licznikPoprawnych / testNumber) * 100;
            Console.WriteLine("Liczba poprawnych odpowiedzi: " + licznikPoprawnych + "/" + testNumber + "      " + procent + "%");
        }
    }
}
