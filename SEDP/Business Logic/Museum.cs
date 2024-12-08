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
     
    }
}
