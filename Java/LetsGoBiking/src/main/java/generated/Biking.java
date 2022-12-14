
package generated;

import java.net.MalformedURLException;
import java.net.URL;
import javax.xml.namespace.QName;
import jakarta.xml.ws.Service;
import jakarta.xml.ws.WebEndpoint;
import jakarta.xml.ws.WebServiceClient;
import jakarta.xml.ws.WebServiceException;
import jakarta.xml.ws.WebServiceFeature;


/**
 * This class was generated by the JAX-WS RI.
 * JAX-WS RI 4.0.0-M4
 * Generated source version: 3.0
 * 
 */
@WebServiceClient(name = "Biking", targetNamespace = "http://tempuri.org/", wsdlLocation = "http://localhost:8800/ProxyCache?wsdl")
public class Biking
    extends Service
{

    private final static URL BIKING_WSDL_LOCATION;
    private final static WebServiceException BIKING_EXCEPTION;
    private final static QName BIKING_QNAME = new QName("http://tempuri.org/", "Biking");

    static {
        URL url = null;
        WebServiceException e = null;
        try {
            url = new URL("http://localhost:8800/ProxyCache?wsdl");
        } catch (MalformedURLException ex) {
            e = new WebServiceException(ex);
        }
        BIKING_WSDL_LOCATION = url;
        BIKING_EXCEPTION = e;
    }

    public Biking() {
        super(__getWsdlLocation(), BIKING_QNAME);
    }

    public Biking(WebServiceFeature... features) {
        super(__getWsdlLocation(), BIKING_QNAME, features);
    }

    public Biking(URL wsdlLocation) {
        super(wsdlLocation, BIKING_QNAME);
    }

    public Biking(URL wsdlLocation, WebServiceFeature... features) {
        super(wsdlLocation, BIKING_QNAME, features);
    }

    public Biking(URL wsdlLocation, QName serviceName) {
        super(wsdlLocation, serviceName);
    }

    public Biking(URL wsdlLocation, QName serviceName, WebServiceFeature... features) {
        super(wsdlLocation, serviceName, features);
    }

    /**
     * 
     * @return
     *     returns IBiking
     */
    @WebEndpoint(name = "BasicHttpBinding_IBiking")
    public IBiking getBasicHttpBindingIBiking() {
        return super.getPort(new QName("http://tempuri.org/", "BasicHttpBinding_IBiking"), IBiking.class);
    }

    /**
     * 
     * @param features
     *     A list of {@link jakarta.xml.ws.WebServiceFeature} to configure on the proxy.  Supported features not in the <code>features</code> parameter will have their default values.
     * @return
     *     returns IBiking
     */
    @WebEndpoint(name = "BasicHttpBinding_IBiking")
    public IBiking getBasicHttpBindingIBiking(WebServiceFeature... features) {
        return super.getPort(new QName("http://tempuri.org/", "BasicHttpBinding_IBiking"), IBiking.class, features);
    }

    private static URL __getWsdlLocation() {
        if (BIKING_EXCEPTION!= null) {
            throw BIKING_EXCEPTION;
        }
        return BIKING_WSDL_LOCATION;
    }

}
