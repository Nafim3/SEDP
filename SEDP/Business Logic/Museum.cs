using System;
using System.Collections.Generic;
using System.Xml.Serialization;

namespace SEDP.Business_Logic
{
    [XmlRoot("Museum")]  // This defines the root element in XML
    public class Museum
    {
        [XmlElement("Name")]  // Marks the Name property for XML serialization
        public string Name { get; set; }

        [XmlElement("Cost")]  // Marks the Cost property for XML serialization
        public decimal Cost { get; set; } = 2000;

        [XmlArray("Visitors")]  // Marks the collection of visitors to be serialized
        [XmlArrayItem("Member")]  // Specifies each item in the collection will be a <Member> element
        public List<Member> Visitors { get; set; } = new List<Member>();

        // Constructor to initialize the museum with a name
        public Museum(string name)
        {
            Name = name;
        }

        // Default constructor needed for deserialization
        public Museum() { }

        // Method to add a visitor
        public void AddMember(Member member)
        {
            Visitors.Add(member);
            Console.WriteLine($"Visitor {member.Name} added to the museum {Name}.");
        }

        // Method to remove a visitor
        public void RemoveMember(Member member)
        {
            // Check if the member is in the list of visitors
            if (Visitors.Contains(member))
            {
                Visitors.Remove(member);  // Remove the member from the list
                Console.WriteLine($"Visitor {member.Name} removed from the museum {Name}.");
            }
            else
            {
                Console.WriteLine($"Visitor {member.Name} not found in the museum {Name}.");
            }
        }




        // Method to get the number of visitors
        public int GetVisitorCount()
        {
            return Visitors.Count;
        }

        // Method to display museum summary
        public void DisplayMuseumSummary()
        {
            Console.WriteLine($"Museum: {Name}");
            Console.WriteLine($"Entry Cost: {Cost}");
            Console.WriteLine($"Visitors Count: {GetVisitorCount()}");
        }

        // Method to save the museum object to XML file
        public void SaveToXml(string filePath = "Tour Info.xml")
        {
            var serializer = new XmlSerializer(typeof(Museum));
            using (var writer = new System.IO.StreamWriter(filePath))
            {
                serializer.Serialize(writer, this);
            }
        }

        // Method to load the museum object from XML file
        //public static Museum LoadFromXml(string filePath = "Tour Info.xml")
        //{
        //    var serializer = new XmlSerializer(typeof(Museum));
        //    using (var reader = new System.IO.StreamReader(filePath))
        //    {
        //        return (Museum)serializer.Deserialize(reader);
        //    }
        //}
    }
}
