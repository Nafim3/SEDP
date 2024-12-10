using Moq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using SEDP.Business_Logic;  
using System;
using System.Collections.Generic;
using System.IO;

namespace Software_Test
{
    [TestClass]
    public class Testing
    {
        [TestMethod]
        public void Test_AddTour_AddsTourCorrectly()
        {
            // Arrange: Create an instance of Tour_Manager and a new Tour
            var tourManager = new Tour_Manager();
            var tour = new Tour { Name = "Test Tour", Identifier = "1234" };

            // Redirect the console output to capture the output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act: Call AddTour
            tourManager.AddTour(tour);

            // Assert: Check if the tour was added to the collection
            Assert.AreEqual(1, tourManager.Tours.Count, "The tour should be added to the Tours collection.");
            Assert.AreEqual(tour, tourManager.Tours[0], "The added tour should match the one provided.");

            // Assert: Check if the correct message is printed to the console
            var output = stringWriter.ToString().Trim();
            Assert.IsTrue(output.Contains($"Tour '{tour.Name}' added successfully."), "The success message should be printed to the console.");
        }

        [TestMethod]
        public void TestRemoveTour_with_ValidIdentifier_TourRemoved()
        {
            // Arrange: Create a Tour_Manager and add a tour
            var tourManager = new Tour_Manager();
            var tour1 = new Tour { Identifier = "123", Name = "Test Tour 1" };
            var tour2 = new Tour { Identifier = "456", Name = "Test Tour 2" };
            tourManager.Tours = new List<Tour> { tour1, tour2 };

            // Act: Call RemoveTour method with valid identifier
            tourManager.RemoveTour("123");

            // Assert: Verify that the tour was removed
            Assert.AreEqual(1, tourManager.Tours.Count, "One tour should be removed.");
            Assert.IsFalse(tourManager.Tours.Exists(t => t.Identifier == "123"), "Tour with identifier '123' should be removed.");
        }

        [TestMethod]
        public void TestRemoveTour_InvalidIdentifier_TourNotFound()
        {
            // Arrange: Create a Tour_Manager and add a tour
            var tourManager = new Tour_Manager();
            var tour1 = new Tour { Identifier = "123", Name = "Test Tour 1" };
            tourManager.Tours = new List<Tour> { tour1 };

            // Act: Try to remove a tour with an invalid identifier
            tourManager.RemoveTour("999");

            // Assert: Ensure the number of tours is still the same (no tour was removed)
            Assert.AreEqual(1, tourManager.Tours.Count, "No tour should be removed.");
            Assert.IsTrue(tourManager.Tours.Exists(t => t.Identifier == "123"), "Tour with identifier '123' should still exist.");
        }

        [TestMethod]
        public void TestListAllTours_PrintsCorrectly()
        {
            // Arrange: Create a Tour_Manager and add some tours
            var tourManager = new Tour_Manager();
            var tour1 = new Tour { Identifier = "123", Name = "Tour 1" };
            var tour2 = new Tour { Identifier = "456", Name = "Tour 2" };
            tourManager.Tours = new List<Tour> { tour1, tour2 };  // Add tours to the list

            // Redirect the console output to a StringWriter to capture the output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act: Call ListAllTours
            tourManager.ListAllTours();

            // Assert: Check if the correct tours are listed
            var output = stringWriter.ToString();
            Assert.IsTrue(output.Contains("Tour 1"), "The list should contain 'Tour 1'.");
            Assert.IsTrue(output.Contains("Tour 2"), "The list should contain 'Tour 2'.");
        }

        [TestMethod]
        public void TestListAllTours_NoTours_PrintNoToursAvailable()
        {
            // Arrange: Create a Tour_Manager with no tours
            var tourManager = new Tour_Manager();
            tourManager.Tours = new List<Tour>();  // Empty list

            // Redirect the console output to a StringWriter to capture the output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act: Call ListAllTours
            tourManager.ListAllTours();

            // Assert: Check if "No tours available." is printed
            var output = stringWriter.ToString();
            Assert.IsTrue(output.Contains("No tours available."), "The output should contain 'No tours available.'");
        }

        [TestMethod]
        public void TestAddCityToTour_AddsCityCorrectly()
        {
            // Arrange: Create a Tour_Manager and a Tour
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "123", Name = "Tour 1" };
            tourManager.Tours = new List<Tour> { tour };

            // Simulate user input for the tour identifier and city name
            var userInput = "123\nParis\n"; // Tour identifier "123", City "Paris"
            var stringReader = new StringReader(userInput);
            Console.SetIn(stringReader);  // Redirect Console input

            // Redirect the console output to capture it
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);  // Redirect Console output

            // Act: Call AddCityToTour
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.AddCityToTour(tourManager);

            // Assert: Check if the correct messages were printed to the console
            var output = stringWriter.ToString();
            Assert.IsTrue(output.Contains("City 'Paris' added to Tour 'Tour 1'"), "The city should be added to the tour.");

            // Verify that the city was added to the tour
            var cityAdded = tour.Cities.Any(city => city.Name == "Paris");
            Assert.IsTrue(cityAdded, "The city 'Paris' should be added to the tour.");
        }

        [TestMethod]
        public void TestAddCityToTour_TourNotFound()
        {
            // Arrange: Create a Tour_Manager with no tour matching the identifier
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "456", Name = "Tour 2" };
            tourManager.Tours = new List<Tour> { tour };

            // Simulate user input for a non-existent tour identifier
            var userInput = "123\nParis\n"; // Non-existent tour "123", City "Paris"
            var stringReader = new StringReader(userInput);
            Console.SetIn(stringReader);  // Redirect Console input

            // Redirect the console output to capture it
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);  // Redirect Console output

            // Act: Call AddCityToTour
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.AddCityToTour(tourManager);

            // Assert: Check if the correct error message was printed to the console
            var output = stringWriter.ToString();
            Assert.IsTrue(output.Contains("Tour not found."), "The message 'Tour not found.' should be printed.");
        }

        [TestMethod]
        public void TestRemoveCityFromTour_RemovesCityCorrectly()
        {
            // Arrange: Create a Tour_Manager and a Tour with a City
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "Tour1", Name = "Tour 1" };
            var city = new City { Name = "Paris" };
            tour.AddCity(city); // Assume AddCity is already implemented in the Tour class
            tourManager.Tours = new List<Tour> { tour };

            // Redirect the console output to capture the output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Simulate user input for the tour identifier and city name
            var reader = new StringReader("Tour1\nParis\n");
            Console.SetIn(reader);

            // Create an instance of Operation_Handler and call RemoveCityFromTour
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.RemoveCityFromTour(tourManager);

            // Assert: Check if the correct city removal message is printed
            var output = stringWriter.ToString().Trim();
            Assert.IsTrue(output.Contains("City Paris removed"), "The city should be removed from the tour.");
        }


        [TestMethod]
        public void TestRemoveCityFromTour_TourNotFound()
        {
            // Arrange: Create a Tour_Manager with a different tour identifier
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "456", Name = "Tour 2" };
            tour.AddCity(new City { Name = "London" });
            tourManager.Tours = new List<Tour> { tour };

            // Simulate user input for a non-existent tour identifier
            var userInput = "123\nParis\n"; // Non-existent tour "123", City "Paris"
            var stringReader = new StringReader(userInput);
            Console.SetIn(stringReader);  // Redirect Console input

            // Redirect the console output to capture it
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);  // Redirect Console output

            // Act: Call RemoveCityFromTour
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.RemoveCityFromTour(tourManager);

            // Assert: Check if the correct error message was printed to the console
            var output = stringWriter.ToString();
            Assert.IsTrue(output.Contains("Tour not found."), "The message 'Tour not found.' should be printed.");
        }

        [TestMethod]
        public void TestAddMuseumVisitAtSpecificCity_AddsMuseumCorrectly()
        {
            // Arrange: Create a Tour_Manager and a Tour with a City
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "Tour1", Name = "Tour 1" };
            var city = new City { Name = "Dhaka" };
            tour.AddCity(city); // Assume AddCity is already implemented in the Tour class
            tourManager.Tours = new List<Tour> { tour };

            // Redirect the console output to capture the output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Simulate user input for Tour Identifier, City Name, Museum Name, and Museum Cost
            var reader = new StringReader("Tour1\nDhaka\nNational Museum\n20\n");
            Console.SetIn(reader);

            // Create an instance of Operation_Handler and call Add_Museum_Visit_At_Specific_City
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.Add_Museum_Visit_At_Specific_City(tourManager);

            // Assert: Check if the correct success message is printed
            var output = stringWriter.ToString().Trim();
            Assert.IsTrue(output.Contains("Museum 'National Museum' added to City 'Dhaka'"), "The museum should be added to the city in the tour.");

            // Assert: Check if the museum was actually added to the city
            var addedMuseum = city.Museums.FirstOrDefault(m => m.Name == "National Museum");
            Assert.IsNotNull(addedMuseum, "The museum should have been added to the city.");
        }

        [TestMethod]
        public void TestRemoveMuseumVisitFromSpecificCity_RemovesMuseumCorrectly()
        {
            // Arrange: Create a Tour_Manager and a Tour with a City and Museum
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "Tour1", Name = "Tour 1" };
            var city = new City { Name = "Dhaka" };
            var museum = new Museum { Name = "National Museum" };
            city.AddMuseum(museum);  // Add a museum to the city
            tour.AddCity(city);      // Add the city to the tour
            tourManager.Tours = new List<Tour> { tour };

            // Redirect the console output to capture the output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Simulate user input for Tour Identifier, City Name, and Museum Name
            var reader = new StringReader("Tour1\nDhaka\nNational Museum\n");
            Console.SetIn(reader);

            // Create an instance of Operation_Handler and call RemoveMuseumVisitFromSpecificCity
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.RemoveMuseumVisitFromSpecificCity(tourManager);

            // Assert: Check if the correct success message is printed
            var output = stringWriter.ToString().Trim();
            Assert.IsTrue(output.Contains("Museum 'National Museum' removed from City 'Dhaka'"), "The museum should be removed from the city in the tour.");

            // Assert: Check if the museum was actually removed from the city
            var removedMuseum = city.Museums.FirstOrDefault(m => m.Name == "National Museum");
            Assert.IsNull(removedMuseum, "The museum should have been removed from the city.");
        }

        [TestMethod]
        public void TestAddMemberToMuseumVisit_AddsMemberCorrectly()
        {
            // Arrange: Create a Tour_Manager, Tour, City, Museum, and Member
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "Tour1", Name = "Tour 1" };
            var city = new City { Name = "Dhaka" };
            var museum = new Museum { Name = "National Museum" };
            var member = new Member { Name = "Nehal" };

            city.AddMuseum(museum); // Add a museum to the city
            tour.AddCity(city);      // Add the city to the tour
            tour.AddMember(member);  // Add member to the tour
            tourManager.Tours = new List<Tour> { tour };

            // Redirect the console output to capture the output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Simulate user input for Tour Identifier, City Name, Museum Name, Member Name, and Booking Number
            var reader = new StringReader("Tour1\nDhaka\nNational Museum\nNehal\n12345\n");
            Console.SetIn(reader);

            // Create an instance of Operation_Handler and call AddMemberToMuseumVisit
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.AddMemberToMuseumVisit(tourManager);

            // Assert: Check if the correct success message is printed
            var output = stringWriter.ToString().Trim();
            Assert.IsTrue(output.Contains("Member 'Nehal' added to Museum 'National Museum'"), "The member should be added to the museum visit.");

            // Assert: Check if the member was actually added to the museum's visitor list
            var addedMember = museum.Visitors.FirstOrDefault(v => v.Name == "Nehal");
            Assert.IsNotNull(addedMember, "The member should have been added to the museum's visitor list.");

            // Assert: Check if the member's booking number is set
            Assert.AreEqual("12345", addedMember?.BookingNumber, "The booking number should be correctly assigned to the member.");
        }

        [TestMethod]
        public void TestRemoveMemberFromMuseumVisit_RemovesMemberCorrectly()
        {
            // Arrange: Create a Tour_Manager, Tour, City, Museum, and Member
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "Tour1", Name = "Tour 1" };
            var city = new City { Name = "Dhaka" };
            var museum = new Museum { Name = "National Museum" };
            var member = new Member { Name = "Shamim", BookingNumber = "12345" };

            city.AddMuseum(museum); // Add a museum to the city
            tour.AddCity(city);      // Add the city to the tour
            tour.AddMember(member);  // Add member to the tour
            museum.AddMember(member); // Add member to the museum's visitor list
            tourManager.Tours = new List<Tour> { tour };

            // Redirect the console output to capture the output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Simulate user input for Tour Identifier, City Name, Museum Name, and Booking Number
            var reader = new StringReader("Tour1\nDhaka\nNational Museum\n12345\n");
            Console.SetIn(reader);

            // Create an instance of Operation_Handler and call RemoveMemberFromMuseumVisit
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.RemoveMemberFromMuseumVisit(tourManager);

            // Assert: Check if the correct success message is printed
            var output = stringWriter.ToString().Trim();
            Assert.IsTrue(output.Contains("Member with booking number '12345' removed from Museum 'National Museum'"), "The member should be removed from the museum.");

            // Assert: Check if the member was actually removed from the museum's visitor list
            var removedMember = museum.Visitors.FirstOrDefault(v => v.BookingNumber == "12345");
            Assert.IsNull(removedMember, "The member should have been removed from the museum's visitor list.");
        }


        [TestMethod]
        public void TestAddMemberToTour_AddsMemberCorrectly()
        {
            // Arrange: Create a Tour_Manager, Tour, and Member
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "Tour1", Name = "Tour 1" };
            tourManager.Tours = new List<Tour> { tour };

            // Redirect the console output to capture the output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Simulate user input for Tour Identifier and Member Name
            var reader = new StringReader("Tour1\nKobir\n");
            Console.SetIn(reader);

            // Create an instance of Operation_Handler and call AddMemberToTour
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.AddMemberToTour(tourManager);

            // Assert: Check if the correct success message is printed
            var output = stringWriter.ToString().Trim();
            Assert.IsTrue(output.Contains("Member 'Kobir' added to tour 'Tour 1'"), "The member should be added to the tour.");

            // Assert: Check if the member is actually added to the tour
            var addedMember = tour.Members.FirstOrDefault(m => m.Name == "Kobir");
            Assert.IsNotNull(addedMember, "The member should have been added to the tour.");

            // Assert: Check if the booking number is generated and assigned correctly
            Assert.IsNotNull(addedMember.BookingNumber, "The member should have a booking number.");
        }


        [TestMethod]
        public void TestRemoveMemberFromTour_RemovesMemberCorrectly()
        {
            // Arrange: Create a Tour_Manager, a Tour, and a Member
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "Tour1", Name = "Tour 1" };
            var member = new Member { Name = "Nafim", BookingNumber = "12345" };
            tour.AddMember(member); // Assuming AddMember is implemented correctly
            tourManager.Tours = new List<Tour> { tour };

            // Redirect the console output to capture the output
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Simulate user input for Booking Number
            var reader = new StringReader("12345\n");
            Console.SetIn(reader);

            // Create an instance of Operation_Handler and call RemoveMemberFromTour
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.RemoveMemberFromTour(tourManager);

            // Assert: Check if the correct success message is printed
            var output = stringWriter.ToString().Trim();
            Assert.IsTrue(output.Contains("Member with booking number '12345' removed from tour."), "The member should be removed from the tour.");

            // Assert: Check if the member is actually removed from the tour
            var removedMember = tour.Members.FirstOrDefault(m => m.BookingNumber == "12345");
            Assert.IsNull(removedMember, "The member should have been removed from the tour.");
        }

        [TestMethod]
        public void TestDisplayMembers_DisplaysMembersCorrectly()
        {
            // Arrange: Create a Tour_Manager and set up tours with and without members
            var tourManager = new Tour_Manager();

            // Tour with members
            var tourWithMembers = new Tour { Identifier = "Tour1", Name = "Tour 1" };
            tourWithMembers.AddMember(new Member { Name = "Sakib", BookingNumber = "12345" });
            tourWithMembers.AddMember(new Member { Name = "Farid", BookingNumber = "67890" });

            // Tour without members
            var tourWithoutMembers = new Tour { Identifier = "Tour2", Name = "Tour 2" };

            // Add tours to the Tour_Manager
            tourManager.Tours = new List<Tour> { tourWithMembers, tourWithoutMembers };

            // Redirect the console output to capture it
            var stringWriter = new StringWriter();
            Console.SetOut(stringWriter);

            // Act: Call the DisplayMembers method
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.DisplayMembers(tourManager);

            // Capture and analyze the output
            var output = stringWriter.ToString().Trim();

            // Assert: Validate the output
            // Check the tour with members
            Assert.IsTrue(output.Contains("Tour name: Tour 1"), "The tour name 'Tour 1' should be displayed.");
            Assert.IsTrue(output.Contains("Name: Sakib, Booking number: 12345"), "The member 'Sakib' should be listed.");
            Assert.IsTrue(output.Contains("Name: Farid, Booking number: 67890"), "The member 'Farid' should be listed.");

            // Check the tour without members
            Assert.IsTrue(output.Contains("Tour name: Tour 2"), "The tour name 'Tour 2' should be displayed.");
            Assert.IsTrue(output.Contains("No members are booked for this tour."), "A message indicating no members should be displayed.");
        }





        [TestMethod]
        public void TestSaveDataToXml_SavesTourManagerToXmlFile()
        {
            // Arrange: Create a Tour_Manager with some data
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "Tour1", Name = "Tour 1" };
            tourManager.Tours = new List<Tour> { tour };

            // Act: Call the SaveDataToXml method
            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.SaveDataToXml(tourManager);

            // Assert: Check if the XML file is created
            string filePath = "Tour Info.xml";
            Assert.IsTrue(File.Exists(filePath), "The XML file should be created.");

            // Optionally, you can check the content of the file
            var xmlContent = File.ReadAllText(filePath);
            Assert.IsTrue(xmlContent.Contains("Tour1"), "The XML content should contain the tour identifier.");
            Assert.IsTrue(xmlContent.Contains("Tour 1"), "The XML content should contain the tour name.");

            // Clean up: Delete the file after the test
            File.Delete(filePath);
        }


        [TestMethod]
        public void TestLoadDataFromXml_LoadsTourManagerFromXmlFile()
        {
            // Arrange: Create a Tour_Manager and save it to an XML file
            var tourManager = new Tour_Manager();
            var tour = new Tour { Identifier = "Tour1", Name = "Tour 1" };
            tourManager.Tours = new List<Tour> { tour };

            var operationHandler = new Operation_Handler(tourManager);
            operationHandler.SaveDataToXml(tourManager);  // Save the data first

            // Act: Call LoadDataFromXml to load the Tour_Manager from the file
            var loadedTourManager = operationHandler.LoadDataFromXml();

            // Assert: Verify the loaded data is correct
            Assert.IsNotNull(loadedTourManager, "The loaded Tour_Manager should not be null.");
            Assert.AreEqual(1, loadedTourManager.Tours.Count, "The loaded Tour_Manager should have one tour.");
            Assert.AreEqual("Tour1", loadedTourManager.Tours[0].Identifier, "The tour identifier should be 'Tour1'.");
            Assert.AreEqual("Tour 1", loadedTourManager.Tours[0].Name, "The tour name should be 'Tour 1'.");

            // Clean up: Delete the XML file after the test
            File.Delete("Tour Info.xml");
        }

        [TestMethod]
        public void TestLoadDataFromXml_WhenFileNotExists_ReturnsNewTourManager()
        {
            // Arrange: Ensure the file does not exist
            string filePath = "Tour Info.xml";
            if (File.Exists(filePath))
            {
                File.Delete(filePath);
            }

            // Pass an instance of Tour_Manager to Operation_Handler
            var tourManager = new Tour_Manager();
            var operationHandler = new Operation_Handler(tourManager);

            // Act: Call LoadDataFromXml when no file exists
            var loadedTourManager = operationHandler.LoadDataFromXml();

            // Assert: Check that a new Tour_Manager instance is returned
            Assert.IsNotNull(loadedTourManager, "A new Tour_Manager should be returned.");
            Assert.AreEqual(0, loadedTourManager.Tours.Count, "A new Tour_Manager should have no tours.");
        }


        [TestMethod]
        public void TestLoadDataFromXml_WhenXmlIsCorrupted_ReturnsNewTourManager()
        {
            // Arrange: Create a corrupted XML file
            string filePath = "Tour Info.xml";
            var corruptedXmlContent = "<Tour_Manager><Tours></Tours></Tour_Manager>";
            File.WriteAllText(filePath, corruptedXmlContent);

            // Act: Call LoadDataFromXml to load the corrupted file
            var tourManager = new Tour_Manager();
            var operationHandler = new Operation_Handler(tourManager);

            // Act: Call LoadDataFromXml when no file exists
            var loadedTourManager = operationHandler.LoadDataFromXml();

            // Assert: Check that a new Tour_Manager instance is returned
            Assert.IsNotNull(loadedTourManager, "A new Tour_Manager should be returned when the XML is corrupted.");
            Assert.AreEqual(0, loadedTourManager.Tours.Count, "A new Tour_Manager should have no tours if the XML is corrupted.");

            // Clean up: Delete the corrupted XML file after the test
            File.Delete(filePath);
        }



    }
}