using System.Xml.Serialization;

namespace CBR.Data.Models;

[XmlRoot(ElementName = "Valuta")]
public class Valuta
{
    [XmlElement(ElementName = "Item")]
    public List<Item> Item { get; set; }

    [XmlAttribute(AttributeName = "name")]
    public string Name { get; set; }
}

