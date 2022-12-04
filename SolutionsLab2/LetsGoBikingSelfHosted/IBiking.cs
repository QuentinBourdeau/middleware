using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace LetsGoBikingSelfHosted
{
    [ServiceContract]
    public interface IBiking
    {
        [OperationContract]
        //returns itinerary after data 
        Itinerary GetItinerary(string origin, string destination);
    }
}
