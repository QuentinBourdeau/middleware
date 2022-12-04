
package generated;

import jakarta.xml.bind.JAXBElement;
import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElementRef;
import jakarta.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour Direction complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType name="Direction"&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="profile" type="{http://www.w3.org/2001/XMLSchema}string" minOccurs="0"/&gt;
 *         &lt;element name="segment" type="{http://schemas.datacontract.org/2004/07/LetsGoBikingSelfHosted}Segment" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "Direction", propOrder = {
    "profile",
    "segment"
})
public class Direction {

    @XmlElementRef(name = "profile", namespace = "http://schemas.datacontract.org/2004/07/LetsGoBikingSelfHosted", type = JAXBElement.class, required = false)
    protected JAXBElement<String> profile;
    @XmlElementRef(name = "segment", namespace = "http://schemas.datacontract.org/2004/07/LetsGoBikingSelfHosted", type = JAXBElement.class, required = false)
    protected JAXBElement<Segment> segment;

    /**
     * Obtient la valeur de la propriété profile.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public JAXBElement<String> getProfile() {
        return profile;
    }

    /**
     * Définit la valeur de la propriété profile.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link String }{@code >}
     *     
     */
    public void setProfile(JAXBElement<String> value) {
        this.profile = value;
    }

    /**
     * Obtient la valeur de la propriété segment.
     * 
     * @return
     *     possible object is
     *     {@link JAXBElement }{@code <}{@link Segment }{@code >}
     *     
     */
    public JAXBElement<Segment> getSegment() {
        return segment;
    }

    /**
     * Définit la valeur de la propriété segment.
     * 
     * @param value
     *     allowed object is
     *     {@link JAXBElement }{@code <}{@link Segment }{@code >}
     *     
     */
    public void setSegment(JAXBElement<Segment> value) {
        this.segment = value;
    }

}
