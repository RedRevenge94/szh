using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using System.Threading;

namespace api {
    public class Program {
        public static void Main(string[] args) {
            new Thread(() => DeviceController.Program.Main(null)).Start();
            new Thread(() => NotificationController.Program.Main(null)).Start();
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .UseUrls(urls: "http://*:5000")
                .Build();
    }
}
