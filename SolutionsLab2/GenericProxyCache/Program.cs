using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Description;
using System.Text;
using System.Threading.Tasks;

namespace GenericProxyCache
{
    class Program
    {
        static void Main(string[] args)
        {
            BasicHttpBinding binding = new BasicHttpBinding();
            Uri baseAddress = new Uri("http://localhost:8733/ProxyCache");
            ServiceHost serviceHost = new ServiceHost(typeof(ProxyCache),baseAddress);
            binding.MaxReceivedMessageSize = 1000000;
            serviceHost.AddServiceEndpoint(typeof(IProxyCache), binding, "");
            ServiceMetadataBehavior serviceMetadataBehavior= new ServiceMetadataBehavior();
            serviceMetadataBehavior.HttpGetEnabled= true;
            serviceMetadataBehavior.HttpsGetEnabled= true;
            serviceHost.Description.Behaviors.Add(serviceMetadataBehavior);
            serviceHost.Open();
            Console.WriteLine("Le proxy est en cours d'utilisation");
            Console.ReadLine();
        }
    }
}
