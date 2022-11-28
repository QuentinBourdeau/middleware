using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.Runtime.Caching;

namespace ProxyCache
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom d'interface "IService1" à la fois dans le code et le fichier de configuration.
    [ServiceContract]
    public interface IProxy
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
