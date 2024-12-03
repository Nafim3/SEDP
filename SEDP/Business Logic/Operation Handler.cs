using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDP.Business_Logic
{
    public class Operation_Handler
    {
        Tour_Manager tour_Manager = new Tour_Manager();
        Tour tour = new Tour();
        Member member = new Member();

        public void Run()
        {
            bool Run_the_Loop = true;

            while (Run_the_Loop)
            {
                Console.WriteLine("\n******* Museum Tour Administration System *******\n");
                Console.WriteLine("1. Add a tour");
                Console.WriteLine("2. Remove a tour");
                Console.WriteLine("3. See all tours");
                Console.WriteLine("4. Add a city to the tour");
                Console.WriteLine("5. Remove a city from the tour");
                Console.WriteLine("6. Add museum visits at a specific city"); //br 
                Console.WriteLine("7. Remove museum visits from a specific city");
                Console.WriteLine("8. Add a member in a tour");
                Console.WriteLine("9. Remove a member from a tour");
                Console.WriteLine("10. Add members to a museum visit");
                Console.WriteLine("11. Remove members from a museum visit");
                Console.WriteLine("12. See members' list");
                Console.WriteLine("13. Exit");
                Console.WriteLine("Select an option");
                string? Choose_option = Console.ReadLine();

                switch (Choose_option)
                {
                    case "1":
                        AddTour(tour_Manager);
                        break;

                    case "2":
                        RemoveTour(tour_Manager);
                        break;

                    case "3":
                        ListTours(tour_Manager); 
                        break;

                    case "4":
                        AddCityToTour(tour_Manager);
                        break;

                    case "5": RemoveCityFromTour(tour_Manager);
                        break;

                    case "13": 
                        Run_the_Loop = false;
                        Console.WriteLine("Exited the program"); 
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again");
                        break;

                    case "6":
                        Add_Museum_Visit_At_Specific_City(tour_Manager);
                        break;

                    case "7":
                        RemoveMuseumVisitFromSpecificCity(tour_Manager);
                        break;

                    case "8":
                        Console.Write("Enter Member Name: ");
                        string memberName = Console.ReadLine();

                        Console.Write("Enter Booking Number: ");
                        string bookingNumber = Console.ReadLine();

                        Member member = new Member
                        {
                            Name = memberName,
                            BookingNumber = bookingNumber
                        };
                        tour.AddMember(tour_Manager);
                        break;

                    case "9":

                        Console.Write("Enter Booking Number: ");
                        string RMV_bookingNumber = Console.ReadLine();

                        Member RMV_member = new Member
                        {
                            BookingNumber = RMV_bookingNumber
                        };
                       // RemoveMemberFromTour(tour_Manager);
                        tour.RemoveMember(RMV_bookingNumber);
                        break;


                    case "10":

                        AddMemberToMuseumVisit(tour_Manager);
                        break;

                    case "11":
                        RemoveMemberFromMuseumVisit(tour_Manager);
                        break;

                    case "12":
                        DisplayMembers(tour);
                        break;
                }
                
            }
        }
        private void AddTour(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Name: ");
            string name = Console.ReadLine();
            Console.Write("Enter Unique Identifier: ");
            string identifier = Console.ReadLine();

            Tour tour = new Tour { Name = name, Identifier = identifier };
            tour_Manager.AddTour(tour);
        }
        private void RemoveTour(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Identifier to Remove: ");
            string identifier = Console.ReadLine();
            tour_Manager.RemoveTour(identifier);
        }
        private void ListTours(Tour_Manager tour_Manager)
        {
            Console.WriteLine("\nAvailable Tours:");
            tour_Manager.ListAllTours();
        }
        private void AddCityToTour(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Identifier: ");
            string tourId = Console.ReadLine();
            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour != null)
            {
                Console.Write("Enter City Name: ");
                string? cityName = Console.ReadLine();
                tour.AddCity(new City { Name = cityName });
                Console.WriteLine($"City '{cityName}' added to Tour '{tour.Name}'.");
            }
            else
            {
                Console.WriteLine("Tour not found.");
            }
        }
        private void RemoveCityFromTour(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Identifier: ");
            string tourId = Console.ReadLine();
            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour != null)
            {
                Console.Write("Enter City Name to Remove: ");
                string cityName = Console.ReadLine();
                tour.RemoveCity(cityName);
            }
            else
            {
                Console.WriteLine("Tour not found.");
            }
        }
        private void Add_Museum_Visit_At_Specific_City(Tour_Manager tour_Manager)
        {
            // Prompt the user to select the tour
            Console.Write("Enter the Tour Identifier: ");
            string? tourId = Console.ReadLine();

            // Fetch the correct Tour object using its identifier
            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour == null)
            {
                Console.WriteLine("Tour not found. Please add a tour first.");
                return;
            }

            // Check if the tour has cities added
            if (tour.Cities.Count == 0)
            {
                Console.WriteLine("No cities have been added to this tour yet. Please add a city first.");
                return;
            }

            // Display the list of cities
            Console.WriteLine("List of Cities in the Tour:");
            foreach (var currentCity in tour.Cities)
            {
                Console.WriteLine($"- {currentCity.Name}");
            }

            // Prompt the user to select a city
            Console.Write("Enter the name of the city to add a museum to: ");
            string? cityName = Console.ReadLine();

            // Check if the entered city name exists in the list
            City? selectedCity = tour.Cities.FirstOrDefault(c => c.Name.Equals(cityName?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (selectedCity != null)
            {
                // Prompt the user for the museum name
                Console.Write("Enter the name of the museum to add: ");
                string? museumName = Console.ReadLine();

                // Add the museum to the selected city
                selectedCity.AddMuseum(new Museum(museumName!));
                Console.WriteLine($"Museum '{museumName}' has been successfully added to the city '{cityName}'.");
            }
            else
            {
                Console.WriteLine($"The city '{cityName}' does not exist in the tour. Operation cancelled.");
            }
        }



        private void RemoveMuseumVisitFromSpecificCity(Tour_Manager tour_Manager)
        {
            // Prompt the user to select the tour
            Console.Write("Enter the Tour Identifier: ");
            string? tourId = Console.ReadLine();

            // Fetch the correct Tour object using its identifier
            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour == null)
            {
                Console.WriteLine("Tour not found. Please add a tour first.");
                return;
            }

            if (tour.Cities.Count == 0)
            {
                Console.WriteLine("The tour has no cities.");
                return;
            }

            Console.Write("Enter City Name: ");
            string cityName = Console.ReadLine();

            // Check if the city exists in the tour
            var city = tour.Cities.FirstOrDefault(c => c.Name.Equals(cityName?.Trim(), StringComparison.OrdinalIgnoreCase));

            if (city != null)
            {
                Console.Write("Enter Museum Name to Remove: ");
                string museumName = Console.ReadLine();

                // Check if the museum exists in the city's museum list
                var museum = city.Museums.FirstOrDefault(m => m.Name.Equals(museumName?.Trim(), StringComparison.OrdinalIgnoreCase));

                if (museum != null)
                {
                    city.RemoveMuseum(museumName);
                    Console.WriteLine($"Museum '{museumName}' has been successfully removed from city '{cityName}'.");
                }
                else
                {
                    Console.WriteLine($"Museum '{museumName}' not found in city '{cityName}'.");
                }
            }
            else
            {
                Console.WriteLine($"City '{cityName}' not found in the tour.");
            }
        }

        private static void RemoveMemberFromTour(Tour_Manager tour_Manager)
        {
            // Get the tour identifier from the user
            Console.Write("Enter Tour Identifier: ");
            string tourId = Console.ReadLine();

            // Retrieve the tour using the identifier
            Tour tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour == null)
            {
                Console.WriteLine("Tour not found.");
                return;
            }

            // Call the method to display current members
            DisplayMembers(tour);

            // Prompt the user to enter the booking number of the member they want to remove
            Console.Write("\nEnter the Booking Number of the member to remove: ");
            string bookingNumber = Console.ReadLine();

            // Check if the booking number is valid (not empty or whitespace)
            if (string.IsNullOrWhiteSpace(bookingNumber))
            {
                Console.WriteLine("Booking number cannot be empty.");
                return;
            }

            // Call the RemoveMember method to remove the member from the tour
            tour.RemoveMember(bookingNumber);

            // Confirm the removal
            Console.WriteLine($"Member with booking number '{bookingNumber}' has been removed.");
        }




        public void AddMemberToMuseumVisit(Tour_Manager tour_Manager)
        {
            // Get the tour identifier from the user
            Console.Write("Enter Tour Identifier: ");
            string tourId = Console.ReadLine();

            // Fetch the correct Tour object
            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);
            if (tour == null)
            {
                Console.WriteLine("Tour not found. Please add a tour first.");
                return;
            }

            // Log current members for debugging
            Console.WriteLine("\nCurrent Members in the Tour before adding:");
            foreach (var m in tour.Members)
            {
                Console.WriteLine($"Name: {m.Name}, Booking Number: {m.BookingNumber}");
            }

            // Check if the tour has cities
            if (tour.Cities.Count == 0)
            {
                Console.WriteLine("No cities have been added to this tour yet. Please add a city first.");
                return;
            }

            // Get the city name
            Console.Write("Enter City Name: ");
            string cityName = Console.ReadLine();

            // Find the city
            var city = tour.Cities.FirstOrDefault(c => c.Name.Equals(cityName?.Trim(), StringComparison.OrdinalIgnoreCase));
            if (city == null)
            {
                Console.WriteLine($"City '{cityName}' not found in the tour.");
                return;
            }

            // Check if the city has museums
            if (city.Museums.Count == 0)
            {
                Console.WriteLine($"No museums have been added to the city '{cityName}' yet.");
                return;
            }

            // Get the museum name
            Console.Write("Enter Museum Name: ");
            string museumName = Console.ReadLine();

            // Find the museum in the city's list of museums
            var museum = city.Museums.FirstOrDefault(m => m.Name.Equals(museumName?.Trim(), StringComparison.OrdinalIgnoreCase));
            if (museum == null)
            {
                Console.WriteLine($"Museum '{museumName}' not found in city '{cityName}'.");
                return;
            }

            // Get the member booking number
            Console.Write("Enter Member Booking Number: ");
            string bookingNumber = Console.ReadLine()?.Trim();

            if (string.IsNullOrEmpty(bookingNumber))
            {
                Console.WriteLine("Booking number cannot be empty. Please try again.");
                return;
            }

            // Log current members for debugging
            Console.WriteLine("\nCurrent Members in the Tour after adding:");
            foreach (var m in tour.Members)
            {
                Console.WriteLine($"Name: {m.Name}, Booking Number: {m.BookingNumber}");
            }

            // Find the member in the tour
            var member = tour.Members.FirstOrDefault(m => m.BookingNumber.Equals(bookingNumber, StringComparison.OrdinalIgnoreCase));
            if (member == null)
            {
                Console.WriteLine($"No member found with booking number '{bookingNumber}'. They must be part of the tour.");
                return;
            }

            // Check if the member is already a visitor of this museum
            if (museum.Visitors.Contains(member))
            {
                Console.WriteLine($"Member '{member.Name}' is already booked for museum '{museumName}' in city '{cityName}'.");
                return;
            }

            // Add the member to the museum's visitors list
            museum.Visitors.Add(member);

            // Optionally add the museum to the member's visited list
            if (!member.VisitedMuseums.Contains(museum))
            {
                member.VisitedMuseums.Add(museum);
            }

            Console.WriteLine($"Member '{member.Name}' has been added to the museum '{museumName}' in city '{cityName}'.");
        }






        public void RemoveMemberFromMuseumVisit(Tour_Manager tour_Manager)
        {
            // Get user input for booking number and museum name
            Console.Write("Enter Member Booking Number: ");
            string bookingNumber = Console.ReadLine();

            Console.Write("Enter Museum Name: ");
            string museumName = Console.ReadLine();

            // Find the member in the tour
            var member = tour.Members.FirstOrDefault(m => m.BookingNumber == bookingNumber);
            if (member == null)
            {
                Console.WriteLine($"No member found with booking number '{bookingNumber}'. They must be part of the tour.");
                return;
            }

            // Find the museum in the member's visited museums
            var museum = member.VisitedMuseums.FirstOrDefault(m => m.Name == museumName);
            if (museum == null)
            {
                Console.WriteLine($"No such museum '{museumName}' found for this member.");
                return;
            }

            // Check if the member is a visitor of this museum
            if (!museum.Visitors.Contains(member))
            {
                Console.WriteLine($"Member '{member.Name}' is not a visitor of museum '{museumName}'.");
                return;
            }

            // Remove the member from the museum's visitors list
            museum.Visitors.Remove(member);
            member.VisitedMuseums.Remove(museum);

            Console.WriteLine($"Member '{member.Name}' has been removed from the museum '{museumName}'.");
        }

        private static void DisplayMembers(Tour tour)
        {
            if (tour.Members.Count == 0)
            {
                Console.WriteLine("Blank list.");
                return;
            }

            Console.WriteLine("Current Members:");
            foreach (var member in tour.Members)
            {
                Console.WriteLine($"Name: {member.Name}, Booking Number: {member.BookingNumber}");
            }
        }






    }
}
