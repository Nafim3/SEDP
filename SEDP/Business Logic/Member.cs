using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SEDP.Business_Logic
{
    [XmlRoot("Member")]  // Define root element for the Member class
    public class Member
    {
        [XmlElement("Name")]  // Mark Name as an XML element
        public string Name { get; set; }

        [XmlElement("BookingNumber")]  // Mark BookingNumber as an XML element
        public string BookingNumber { get; set; } // Must be unique

        [XmlArray("VisitedMuseums")]  // Mark VisitedMuseums as an XML array
        [XmlArrayItem("Museum")]  // Specify that each item in the array will be a <Museum> element
        public List<Museum> VisitedMuseums { get; set; } = new List<Museum>();

        // Constructor
        public Member() { }
    }
}
