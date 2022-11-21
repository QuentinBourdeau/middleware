using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.ServiceModel.Web;
using System.Text;

namespace MathsLibraryREST
{
    [ServiceContract]
    public interface IMathsOperationsREST
    {
        [OperationContract]
        [WebInvoke(UriTemplate = "Add?x={left}&y={right}", Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int Add(int left, int right);

        [OperationContract]
        [WebInvoke(UriTemplate = "Subtract?x={left}&y={right}", Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int Subtract(int left, int right);

        [OperationContract]
        [WebInvoke(UriTemplate = "Multiply?x={left}&y={right}", Method = "GET", ResponseFormat = WebMessageFormat.Json, BodyStyle = WebMessageBodyStyle.Wrapped)]
        int Multiply(int left, int right);
    }
}
