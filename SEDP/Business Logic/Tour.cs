using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDP.Business_Logic
{
    public class Tour
    {
        public string? Name { get; set; }
        public string? Identifier { get; set; }
        public List<City> Cities { get; set; } = new List<City>();
        public List<Member> Members { get; set; } = new List<Member>();

        public void AddCity(City city)
        {
            Cities.Add(city);
        }

        public void RemoveCity(string cityName)
        {
            City cityToRemove = null;

            foreach (var city in Cities)
            {
                if (city.Name == cityName)
                {
                    cityToRemove = city;
                    break;
                }
                else { Console.WriteLine("mentioned city name is not found");}
            }

            if (cityToRemove != null)
            {
                Cities.Remove(cityToRemove);
            }
        }

        public void AddMember(Tour_Manager tour_Manager)
        {
            // Prompt the user to enter the Tour Identifier
            Console.Write("Enter Tour Identifier: ");
            string tourId = Console.ReadLine();

            // Fetch the correct Tour object using the tour identifier
            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);
            if (tour == null)
            {
                Console.WriteLine("Tour not found. Please check the identifier and try again.");
                return;
            }

            // Now, ask for the Member details
            Console.Write("Enter Member Name: ");
            string memberName = Console.ReadLine();

            Console.Write("Enter Booking Number: ");
            string bookingNumber = Console.ReadLine();

            Member member = new Member
            {
                Name = memberName,
                BookingNumber = bookingNumber
            };

            // Check if the member already exists in the tour's member list
            bool alreadyExists = false;
            foreach (var m in tour.Members)
            {
                if (m.BookingNumber == member.BookingNumber)
                {
                    alreadyExists = true;
                    break;
                }
            }

            if (alreadyExists)
            {
                Console.WriteLine("Member already exists.");
            }
            else
            {
                tour.Members.Add(member);
                Console.WriteLine($"Member '{member.Name}' with Booking Number '{member.BookingNumber}' has been added to the tour '{tour.Name}'.");
            }

            // Debugging: Log all members after addition
            Console.WriteLine("\nCurrent Members in the Tour:");
            foreach (var m in tour.Members)
            {
                Console.WriteLine($"Name: {m.Name}, Booking Number: {m.BookingNumber}");
            }
        }




        public void RemoveMember(string bookingNumber)
        {
            Member? memberToRemove = Members.FirstOrDefault(m => m.BookingNumber == bookingNumber);

            if (memberToRemove != null)
            {
                Members.Remove(memberToRemove);
                Console.WriteLine($"Member '{memberToRemove.Name}' removed.");
            }
            else
            {
                Console.WriteLine("Member not found.");
            }
        }

        public decimal CalculateExtraCosts()
        {
            decimal totalExtraCosts = 0;

            foreach (var member in Members)
            {
                if (member.VisitedMuseums.Count > 2)
                {
                    for (int i = 2; i < member.VisitedMuseums.Count; i++)
                    {
                        totalExtraCosts += member.VisitedMuseums[i].Cost + 1000;
                    }
                }
            }

            Console.WriteLine($"Total Cost {totalExtraCosts}");
            return totalExtraCosts;
            
        }

        public List<string> GetCityNames()
        {
            List<string> cityNames = new List<string>();

            foreach (var city in Cities)
            {
                cityNames.Add(city.Name);
            }

            return cityNames;
        }

        public bool ContainsCity(string cityName)
        {
            foreach (var city in Cities)
            {
                if (city.Name == cityName)
                {
                    return true;
                }
            }
            return false;
        }


    }
}
