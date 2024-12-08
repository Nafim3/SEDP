using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;

namespace SEDP.Business_Logic
{
    [XmlRoot("City")]  // Define the root element for the City class
    public class City
    {
        [XmlElement("Name")]  // Marks Name as an XML element
        public string? Name { get; set; }

        [XmlArray("Museums")]  // Marks Museums as an XML array
        [XmlArrayItem("Museum")]  // Specifies that each item in the array will be a <Museum> element
        public List<Museum> Museums { get; set; } = new List<Museum>();

        // Constructor
        public City() { }

        // Add a museum to the city
        public void AddMuseum(Museum museum)
        {
            Museums.Add(museum);
        }

        // Remove a museum by its object reference
        public void RemoveMuseum(Museum museum)
        {
            Museums.Remove(museum);
        }

      
    }
}
