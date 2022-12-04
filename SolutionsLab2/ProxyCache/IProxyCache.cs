using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Runtime.Caching;

namespace ProxyCache
{
    [ServiceContract]
    public interface IProxyCache
    {
        [OperationContract]
        string Request(string url);

        [OperationContract]
        string getContractsList();

        [OperationContract]
        string getStationsList();

        [OperationContract]
        string getStationsListWithContractName(string contractName);

    }
    }
