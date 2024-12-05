using SEDP.UI;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace SEDP.Business_Logic
{
    public class Operation_Handler
    {
        private readonly Tour_Manager tour_Manager;

        public Operation_Handler(Tour_Manager tourManager)
        {
            tour_Manager = tourManager;
        }

       

        public void Start()
        {
            // Load the Tour_Manager from XML
            var loadedTourManager = this.LoadDataFromXml();

            // Pass the loaded Tour_Manager to Options
            var options = new Options(loadedTourManager);
            options.Run();
        }

        public Tour AddTour(Tour_Manager tour_Manager)
        {

            Console.Write("Enter Tour Name: ");
            string? name = Console.ReadLine();  // Nullable string input

            // Check for null and provide a default value if necessary
            name ??= "Default Tour Name";  // If name is null, use this default value 

            string identifier = new string(Enumerable.Range(0, 4).Select(_ => "A3C9W4E71H5Q2P0Q719"[new Random().Next(18)]).ToArray());

            Tour newTour = new Tour
            {
                Name = name,   // 'name' should now be guaranteed not to be null
                Identifier = identifier
            };
            Console.WriteLine($"Tour Identifier: {identifier}");
            tour_Manager.AddTour(newTour);
            SaveDataToXml(tour_Manager);
            return newTour;
        }

        public void RemoveTour(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Identifier to Remove: ");
            string? identifier = Console.ReadLine();

            if (string.IsNullOrEmpty(identifier))
            {
                Console.WriteLine("Invalid identifier.");
                return;
            }

            var tourToRemove = tour_Manager.Tours.FirstOrDefault(t => t.Identifier == identifier);
            if (tourToRemove == null)
            {
                Console.WriteLine("Tour with the provided identifier not found.");
                return;
            }

            tour_Manager.RemoveTour(identifier);
            SaveDataToXml(tour_Manager);  // Save after changes
            Console.WriteLine($"Tour Identifier {identifier} has been removed successfully.");
        }


        public void AddCityToTour(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Identifier: ");
            string? tourId = Console.ReadLine();
            tourId = tourId ?? string.Empty;  // Ensure tourId is never null

            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour != null)
            {
                Console.Write("Enter City Name: ");
                string? cityName = Console.ReadLine();
                tour.AddCity(new City { Name = cityName });
                SaveDataToXml(tour_Manager);  // Save after changes
                Console.WriteLine($"City '{cityName}' added to Tour '{tour.Name}'.");
            }
            else
            {
                Console.WriteLine("Tour not found.");
            }
        }

        public void RemoveCityFromTour(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Identifier: ");
            string? tourId = Console.ReadLine();
            tourId = tourId ?? string.Empty;  // Ensure tourId is never null

            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour != null)
            {
                Console.Write("Enter City Name to Remove: ");
                string? cityName = Console.ReadLine();
                tour.RemoveCity(cityName);
                SaveDataToXml(tour_Manager);
                Console.WriteLine($"City {cityName} removed");
            }
            else
            {
                Console.WriteLine("Tour not found.");
            }
        }

        public void Add_Museum_Visit_At_Specific_City(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Identifier: ");
            string? tourId = Console.ReadLine();
            tourId = tourId ?? string.Empty;  // Ensure tourId is never null

            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour != null)
            {
                Console.Write("Enter City Name: ");
                string? cityName = Console.ReadLine();
                City? city = tour.Cities.FirstOrDefault(c => c.Name == cityName);

                if (city != null)
                {
                    Console.Write("Enter Museum Name: ");
                    string? museumName = Console.ReadLine();

                    if (string.IsNullOrEmpty(museumName))
                    {
                        Console.WriteLine("Museum name cannot be empty.");
                    }
                    else
                    {
                        Museum museum = new Museum { Name = museumName };
                        city.AddMuseum(museum);  // Ensure AddMuseum handles adding the museum properly
                        Console.WriteLine($"Museum '{museumName}' added to City '{city.Name}' in Tour '{tour.Name}'.");
                    }
                }
                else
                {
                    Console.WriteLine("City not found in the tour.");
                }
            }
            else
            {
                Console.WriteLine("Tour not found.");
            }
        }


        public void RemoveMuseumVisitFromSpecificCity(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Identifier: ");
            string? tourId = Console.ReadLine();
            tourId = tourId ?? string.Empty;  // Ensure tourId is never null

            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour != null)
            {
                Console.Write("Enter City Name: ");
                string? cityName = Console.ReadLine();
                City? city = tour.Cities.FirstOrDefault(c => c.Name == cityName);

                if (city != null)
                {
                    Console.Write("Enter Museum Name to Remove: ");
                    string? museumName = Console.ReadLine();

                    Museum? museumToRemove = city.Museums.FirstOrDefault(m => m.Name == museumName);

                    if (museumToRemove != null)
                    {
                        city.RemoveMuseum(museumToRemove); // You’ll need to add RemoveMuseum method in City class
                        Console.WriteLine($"Museum '{museumName}' removed from City '{cityName}' in Tour '{tour.Name}'.");
                    }
                    else
                    {
                        Console.WriteLine("Museum not found in the city.");
                    }
                }
                else
                {
                    Console.WriteLine("City not found in the tour.");
                }
            }
            else
            {
                Console.WriteLine("Tour not found.");
            }
        }


        public void AddMemberToMuseumVisit(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Identifier: ");
            string? tourId = Console.ReadLine();
            tourId = tourId ?? string.Empty;  // Ensure tourId is never null

            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour != null)
            {
                Console.Write("Enter City Name: ");
                string? cityName = Console.ReadLine();
                City? city = tour.Cities.FirstOrDefault(c => c.Name == cityName);

                if (city != null)
                {
                    Console.Write("Enter Museum Name: ");
                    string? museumName = Console.ReadLine();
                    Museum? museum = city.Museums.FirstOrDefault(m => m.Name == museumName);

                    if (museum != null)
                    {
                        Console.Write("Enter Member Name: ");
                        string? memberName = Console.ReadLine();
                        Member? member = tour.Members.FirstOrDefault(m => m.Name == memberName);

                        if (member != null)
                        {
                            Console.Write("Enter Booking Number: ");
                            string? bookingNumber = Console.ReadLine();

                            // Check if the member is already booked for the museum
                            if (museum.Visitors.Any(m => m.BookingNumber == bookingNumber))
                            {
                                Console.WriteLine("This member has already been booked for this museum.");
                            }
                            else
                            {
                                // Add the member to the museum's visit list
                                member.BookingNumber = bookingNumber!;
                                museum.AddMember(member);

                                // Ensure this museum visit is also recorded in the member's visit history
                                member.VisitedMuseums.Add(museum); // Add the museum to the member's visited list

                                Console.WriteLine($"Total Cost: {tour.CalculateExtraCosts()} BDT");
                                Console.WriteLine($"Member '{memberName}' added to Museum '{museumName}' in City '{cityName}' of Tour '{tour.Name}'.");
                            }
                        }
                        else
                        {
                            Console.WriteLine($"Member '{memberName}' is not part of the tour '{tour.Name}'.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Museum not found in the city.");
                    }
                }
                else
                {
                    Console.WriteLine("City not found in the tour.");
                }
            }
            else
            {
                Console.WriteLine("Tour not found.");
            }
        }



        public void RemoveMemberFromMuseumVisit(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Identifier: ");
            string? tourId = Console.ReadLine();
            tourId = tourId ?? string.Empty;  // Ensure tourId is never null
            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour != null)
            {
                Console.Write("Enter City Name: ");
                string? cityName = Console.ReadLine();
                City? city = tour.Cities.FirstOrDefault(c => c.Name == cityName);

                if (city != null)
                {
                    Console.Write("Enter Museum Name: ");
                    string? museumName = Console.ReadLine();
                    Museum? museum = city.Museums.FirstOrDefault(m => m.Name == museumName);

                    if (museum != null)
                    {
                        Console.Write("Enter Booking Number: ");
                        string? bookingNumber = Console.ReadLine();
                        Member? memberToRemove = museum.Visitors.FirstOrDefault(m => m.BookingNumber == bookingNumber);

                        if (memberToRemove != null)
                        {
                            museum.RemoveMember(memberToRemove);  // Call RemoveMember instead of RemoveVisitor
                            Console.WriteLine($"Member with booking number '{bookingNumber}' removed from Museum '{museumName}' in City '{cityName}' of Tour '{tour.Name}'.");
                        }
                        else
                        {
                            Console.WriteLine("Member not found in the museum.");
                        }
                    }
                    else
                    {
                        Console.WriteLine("Museum not found in the city.");
                    }
                }
                else
                {
                    Console.WriteLine("City not found in the tour.");
                }
            }
            else
            {
                Console.WriteLine("Tour not found.");
            }
        }


        public void AddMemberToTour(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Tour Identifier: ");
            string? tourId = Console.ReadLine();
            tourId = tourId ?? string.Empty;
            Tour? tour = tour_Manager.GetTourByIdentifier(tourId);

            if (tour != null)
            {
                Console.Write("Enter Member Name: ");
                string? memberName = Console.ReadLine();

                // Check if memberName is null or empty
                if (string.IsNullOrEmpty(memberName))
                {
                    Console.WriteLine("Member name cannot be empty.");
                }
                else
                {
                    string bookingNumber = new string(Enumerable.Range(0, 4).Select(_ => "2501789ABCDWXEFGHIQ"[new Random().Next(16)]).ToArray());
                    Console.WriteLine($"Booking Number: {bookingNumber}");

                    // Create the member only if the name is valid
                    Member member = new Member
                    {
                        Name = memberName,
                        BookingNumber = bookingNumber
                    };

                    // Add member to the tour
                    tour.AddMember(member);
                    SaveDataToXml(tour_Manager);  // Save after changes

                    Console.WriteLine($"Member '{memberName}' added to tour '{tour.Name}'.");
                }
            }

            else
            {
                Console.WriteLine("Tour not found.");
            }
        }

        public void RemoveMemberFromTour(Tour_Manager tour_Manager)
        {
            Console.Write("Enter Booking Number: ");
            string? bookingNumber = Console.ReadLine();

            // Validate booking number
            if (string.IsNullOrEmpty(bookingNumber))
            {
                Console.WriteLine("Booking number cannot be empty.");
                return; // Exit the method if no valid booking number is provided
            }

            // Proceed with member removal if booking number is valid
            tour_Manager.GetAllTours().ForEach(t => t.RemoveMember(bookingNumber));
            SaveDataToXml(tour_Manager);  // Save after changes
            Console.WriteLine($"Member with booking number '{bookingNumber}' removed from tour.");
        }


        public void DisplayMembers(Tour_Manager tour_Manager)
        {
            foreach (var tour in tour_Manager.GetAllTours())
            {
                
                foreach (var member in tour.Members)
                {
                    Console.WriteLine($"\nTour name: {tour.Name}");
                    Console.WriteLine($"Name: {member.Name}, Booking number: {member.BookingNumber}\n");
                }
            }
        }

        private const string FilePath = "Tour Info.xml";

        public Tour_Manager LoadDataFromXml()
        {
            var filePath = "Tour Info.xml";
            if (File.Exists(filePath))
            {
                var serializer = new XmlSerializer(typeof(Tour_Manager));
                using (var reader = new StreamReader(filePath))
                {
                    return (Tour_Manager)serializer.Deserialize(reader);
                }
            }
            else
            {
                Console.WriteLine("XML file not found. Starting with an empty tour manager.");
                return new Tour_Manager(); // Return an empty manager if file doesn't exist
            }
        }


        public void SaveDataToXml(Tour_Manager tourManager)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(Tour_Manager));
            using (FileStream fileStream = new FileStream(FilePath, FileMode.Create))
            {
                serializer.Serialize(fileStream, tourManager);
            }
        }

    }
}