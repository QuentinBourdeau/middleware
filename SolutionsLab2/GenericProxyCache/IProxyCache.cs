using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace GenericProxyCache
{
    [ServiceContract]
    public interface IProxyCache
    {

        [OperationContract]
        JCDecauxItem getContractsList();

        [OperationContract]
        JCDecauxItem getStationsList();

        [OperationContract]
        JCDecauxItem getStationsListWithContractName(string contractName);

    }
}
