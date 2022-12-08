using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace test
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            Uri baseAddress = new Uri("http://localhost:8800/ProxyCache");
            ServiceHost serviceHost = new ServiceHost(typeof(Biking), baseAddress);
            binding.MaxReceivedMessageSize = 1000000000;
            serviceHost.AddServiceEndpoint(typeof(IBiking), binding, "");
            ServiceMetadataBehavior serviceMetadataBehavior = new ServiceMetadataBehavior();
            serviceMetadataBehavior.HttpGetEnabled = true;
            serviceMetadataBehavior.HttpsGetEnabled = true;
            serviceHost.Description.Behaviors.Add(serviceMetadataBehavior);
            serviceHost.Open();
            IBiking bike = new Biking();
            Console.WriteLine("Lancement du routeur Let's Go Biking");
            Console.ReadLine();

        }

    }
}
