using System;
using System.Collections.Generic;
using System.Linq;
using System.Xml.Serialization;
using System.IO;

namespace SEDP.Business_Logic
{
    [XmlRoot("Tour_Manager")]
    public class Tour_Manager
    {
        [XmlArray("Tours")]
        [XmlArrayItem("Tour")]
        public List<Tour> Tours { get; set; } = new List<Tour>();  // Added XmlArray and XmlArrayItem attributes

        // Assuming you have this method in the Tour_Manager class to add the tour
        public void AddTour(Tour tour)
        {
            // Add the tour to the collection
            Tours.Add(tour); // Assuming `Tours` is a List<Tour>
            Console.WriteLine($"Tour '{tour.Name}' added successfully.");
        }

        public void RemoveTour(string identifier)
        {
            Tour? tourToRemove = Tours.FirstOrDefault(t => t.Identifier == identifier);

            if (tourToRemove != null)
            {
                Tours.Remove(tourToRemove);
                
            }
            else
            {
                Console.WriteLine($"No tour found with the identifier '{identifier}'.");
            }
        }

        public Tour? GetTourByIdentifier(string identifier)
        {
            return Tours.FirstOrDefault(t => t.Identifier == identifier);
        }

        public List<Tour> GetAllTours()
        {
            return Tours;
        }

        public void ListAllTours()
        {
            if (Tours == null || !Tours.Any())
            {
                Console.WriteLine("No tours available.");
                return;
            }

            foreach (var tour in Tours)
            {
                Console.WriteLine($"\nTour Identifier: {tour.Identifier}, Name: {tour.Name}\n");
            }
        }

    }
}
