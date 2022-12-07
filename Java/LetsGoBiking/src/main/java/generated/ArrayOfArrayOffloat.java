
package generated;

import java.util.ArrayList;
import java.util.List;
import jakarta.xml.bind.annotation.XmlAccessType;
import jakarta.xml.bind.annotation.XmlAccessorType;
import jakarta.xml.bind.annotation.XmlElement;
import jakarta.xml.bind.annotation.XmlType;


/**
 * <p>Classe Java pour ArrayOfArrayOffloat complex type.
 * 
 * <p>Le fragment de schéma suivant indique le contenu attendu figurant dans cette classe.
 * 
 * <pre>
 * &lt;complexType name="ArrayOfArrayOffloat"&gt;
 *   &lt;complexContent&gt;
 *     &lt;restriction base="{http://www.w3.org/2001/XMLSchema}anyType"&gt;
 *       &lt;sequence&gt;
 *         &lt;element name="ArrayOffloat" type="{http://schemas.microsoft.com/2003/10/Serialization/Arrays}ArrayOffloat" maxOccurs="unbounded" minOccurs="0"/&gt;
 *       &lt;/sequence&gt;
 *     &lt;/restriction&gt;
 *   &lt;/complexContent&gt;
 * &lt;/complexType&gt;
 * </pre>
 * 
 * 
 */
@XmlAccessorType(XmlAccessType.FIELD)
@XmlType(name = "ArrayOfArrayOffloat", namespace = "http://schemas.microsoft.com/2003/10/Serialization/Arrays", propOrder = {
    "arrayOffloat"
})
public class ArrayOfArrayOffloat {

    @XmlElement(name = "ArrayOffloat", nillable = true)
    protected List<ArrayOffloat> arrayOffloat;

    /**
     * Gets the value of the arrayOffloat property.
     * 
     * <p>
     * This accessor method returns a reference to the live list,
     * not a snapshot. Therefore any modification you make to the
     * returned list will be present inside the Jakarta XML Binding object.
     * This is why there is not a <CODE>set</CODE> method for the arrayOffloat property.
     * 
     * <p>
     * For example, to add a new item, do as follows:
     * <pre>
     *    getArrayOffloat().add(newItem);
     * </pre>
     * 
     * 
     * <p>
     * Objects of the following type(s) are allowed in the list
     * {@link ArrayOffloat }
     * 
     * 
     */
    public List<ArrayOffloat> getArrayOffloat() {
        if (arrayOffloat == null) {
            arrayOffloat = new ArrayList<ArrayOffloat>();
        }
        return this.arrayOffloat;
    }

}
