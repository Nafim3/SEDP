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

        public Member(string name, string bookingNumber)
        {
            Name = name;
            BookingNumber = bookingNumber;
        }

        // Method to add a visited museum
        public void AddVisitedMuseum(Museum museum)
        {
            VisitedMuseums.Add(museum);
        }

       

        // Method to save the Member object to XML file
        public void SaveToXml(string filePath)
        {
            var serializer = new XmlSerializer(typeof(Member));
            using (var writer = new System.IO.StreamWriter(filePath))
            {
                serializer.Serialize(writer, this);
            }
        }

        // Static method to load Member object from XML file
        public static Member LoadFromXml(string filePath)
        {
            var serializer = new XmlSerializer(typeof(Member));
            using (var reader = new System.IO.StreamReader(filePath))
            {
                return (Member)serializer.Deserialize(reader)!;
            }
        }
    }
}
