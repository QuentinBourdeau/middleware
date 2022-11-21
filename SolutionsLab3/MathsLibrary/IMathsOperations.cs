using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace MathsLibrary
{
    [ServiceContract]
    public interface IMathsOperations
    {
        [OperationContract]
        int Add(int left, int right);

        [OperationContract]
        int Subtract(int left, int right);

        [OperationContract]
        int Multiply(int left, int right);
    }
}
