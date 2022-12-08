using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace test
{
    [ServiceContract]
    public interface IBiking
    {
        [OperationContract]
        //returns itinerary after data processing
        Itinerary GetItinerary(string origin, string destination);
    }
}
