using System.Xml.Serialization;

namespace CBR.Data.Models;

[XmlRoot(ElementName = "Item")]
public class Item
{
    [XmlElement(ElementName = "Name")]
    public string Name { get; set; }

    [XmlElement(ElementName = "EngName")]
    public string EngName { get; set; }

    [XmlElement(ElementName = "Nominal")]
    public int Nominal { get; set; }

    [XmlElement(ElementName = "ParentCode")]
    public string ParentCode { get; set; }

    [XmlAttribute(AttributeName = "ID")]
    public string Id { get; set; }
}

