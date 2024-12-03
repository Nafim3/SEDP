using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDP.Business_Logic
{
    public class Tour_Manager
    {
        private List<Tour> Tours { get; set; } = new List<Tour>();

        public void AddTour(Tour tour)
        {
            if (!Tours.Any(t => t.Identifier == tour.Identifier))
            {
                Tours.Add(tour);
                Console.WriteLine($"Tour '{tour.Name}' added successfully.");
            }
            else
            {
                Console.WriteLine($"A tour with the identifier '{tour.Identifier}' already exists.");
            }
        }

        public void RemoveTour(string identifier)
        {
            Tour? tourToRemove = Tours.FirstOrDefault(t => t.Identifier == identifier);

            if (tourToRemove != null)
            {
                Tours.Remove(tourToRemove);
                Console.WriteLine($"Tour with identifier '{identifier}' removed successfully.");
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
            if (Tours.Count == 0)
            {
                Console.WriteLine("No tours available.");
            }
            else
            {
                foreach (var tour in Tours)
                {
                    Console.WriteLine($"Identifier: {tour.Identifier}, Name: {tour.Name}");
                }
            }
        }
    }
}
