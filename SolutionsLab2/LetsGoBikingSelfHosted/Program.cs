using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text.Json;
using System.Device.Location;
//using RestClient.Proxy;
using System.ServiceModel;
using System.ServiceModel.Description;

namespace LetsGoBikingSelfHosted
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            Uri baseAddress = new Uri("http://localhost:8800/ProxyCache");
            ServiceHost serviceHost = new ServiceHost(typeof(Biking), baseAddress);
            binding.MaxReceivedMessageSize = 1000000;
            serviceHost.AddServiceEndpoint(typeof(IBiking), binding, "");
            ServiceMetadataBehavior serviceMetadataBehavior = new ServiceMetadataBehavior();
            serviceMetadataBehavior.HttpGetEnabled = true;
            serviceMetadataBehavior.HttpsGetEnabled = true;
            serviceHost.Description.Behaviors.Add(serviceMetadataBehavior);
            serviceHost.Open();
            IBiking bike = new Biking();
            Console.WriteLine(bike.GetItinerary("tour eiffel", "Louvre"));

            Console.WriteLine("Lancement de Let's Go Biking");
            Console.ReadLine();

        }
    
    }
}
