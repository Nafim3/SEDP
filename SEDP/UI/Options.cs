using SEDP.Business_Logic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SEDP.UI
{
    public class Options
    {
       
        private readonly Tour_Manager loadedTourManager;

      

        public Options(Tour_Manager loadedTourManager)
        {
            this.loadedTourManager = loadedTourManager;
        }

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
                Console.WriteLine("6. Add museum visits at a specific city");
                Console.WriteLine("7. Remove museum visits from a specific city");
                Console.WriteLine("8. Add a member in a tour");
                Console.WriteLine("9. Remove a member from a tour");
                Console.WriteLine("10. Add members to a museum visit");
                Console.WriteLine("11. Remove members from a museum visit");
                Console.WriteLine("12. See members' list");
                Console.WriteLine("13. Exit");
                Console.WriteLine("Select an option");
                string? Choose_option = Console.ReadLine();

                
                var operationHandler = new Operation_Handler(loadedTourManager);

                switch (Choose_option)
                {
                    case "1":
                        operationHandler.AddTour(loadedTourManager);
                        break;

                    case "2":
                        loadedTourManager.ListAllTours();
                        operationHandler.RemoveTour(loadedTourManager);
                        break;

                    case "3":
                        loadedTourManager.ListAllTours();
                        break;

                    case "4":
                        loadedTourManager.ListAllTours();
                        operationHandler.AddCityToTour(loadedTourManager);
                        break;

                    case "5":
                        loadedTourManager.ListAllTours();
                        operationHandler.RemoveCityFromTour(loadedTourManager);
                        break;

                    case "6":
                        loadedTourManager.ListAllTours();
                        operationHandler.Add_Museum_Visit_At_Specific_City(loadedTourManager);
                        break;

                    case "7":
                        loadedTourManager.ListAllTours();
                        operationHandler.RemoveMuseumVisitFromSpecificCity(loadedTourManager);
                        break;

                    case "8":
                        loadedTourManager.ListAllTours();
                        Console.WriteLine("Current Members:\n");
                        operationHandler.DisplayMembers(loadedTourManager);
                        operationHandler.AddMemberToTour(loadedTourManager);
                        break;

                    case "9":
                        loadedTourManager.ListAllTours();
                        Console.WriteLine("Current Members:\n");
                        operationHandler.DisplayMembers(loadedTourManager);
                        operationHandler.RemoveMemberFromTour(loadedTourManager);
                        break;

                    case "10":
                        loadedTourManager.ListAllTours();
                        Console.WriteLine("Current Members:\n");
                        operationHandler.DisplayMembers(loadedTourManager);
                        operationHandler.AddMemberToMuseumVisit(loadedTourManager);
                        break;

                    case "11":
                        loadedTourManager.ListAllTours();
                        Console.WriteLine("Current Members:\n");
                        operationHandler.DisplayMembers(loadedTourManager);
                        operationHandler.RemoveMemberFromMuseumVisit(loadedTourManager);
                        break;

                    case "12":
                        operationHandler.DisplayMembers(loadedTourManager);
                        break;

                    case "13":
                        Run_the_Loop = false;
                        Console.WriteLine("Exited the program");
                        break;

                    default:
                        Console.WriteLine("Invalid option. Try again");
                        break;
                }
            }
        }
    }
}
