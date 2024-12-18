﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SEDP.Business_Logic;

namespace SEDP.UI
{
    public class Gate
    {

        private readonly string Login_ID = "SysCtrl";
        private string Password = "access";

        public void Security()
        {

            bool isAuthenticated = false;

            while (!isAuthenticated)
            {
                Console.WriteLine("Enter Login ID:");
                string? usernameInput = Console.ReadLine();

                if (usernameInput == Login_ID)
                {
                    while (true)
                    {
                        Console.WriteLine("Enter Password:");
                        string? passwordInput = Console.ReadLine();

                        if (passwordInput == Password)
                        {
                            isAuthenticated = true;
                            Console.WriteLine("Login successful");
                            var operationHandler = new Operation_Handler(new Tour_Manager());
                            operationHandler.Start();
                            break;
                        }
                        else
                        {
                            Console.WriteLine("Invalid Password.");
                            Console.WriteLine("Forgotten Password?");
                            Console.WriteLine("1. Yes");
                            Console.WriteLine("2. No");

                            string? retryPasswordChoice;
                            while (true)
                            {
                                retryPasswordChoice = Console.ReadLine();
                                if (retryPasswordChoice == "1" || retryPasswordChoice == "2")
                                    break;
                                Console.WriteLine("Invalid choice, enter 1 or 2:");
                            }

                            if (retryPasswordChoice == "1")
                            {
                                Console.WriteLine("To reset your password, answer the following question.");
                                Console.WriteLine("In which city is the University of Hull located?\nAnswer should be in all uppercase letters");
                                string? securityAnswer = Console.ReadLine();

                                if (securityAnswer == "HULL")
                                {
                                    Console.WriteLine("Security answer verified. Enter a new password:");
                                    string? newPassword = Console.ReadLine();
                                    Password = newPassword!;
                                    Console.WriteLine("Your password has been changed successfully, try logging in again.");
                                    break;
                                }
                                else
                                {
                                    Console.WriteLine("Incorrect answer. Returning to login.");
                                }
                            }
                            else if (retryPasswordChoice == "2")
                            {
                                Console.WriteLine("Would you like to try entering the password again?");
                                Console.WriteLine("1. Yes");
                                Console.WriteLine("2. No");

                                string? retryPasswordChoiceAfterForgot;
                                while (true)
                                {
                                    retryPasswordChoiceAfterForgot = Console.ReadLine();
                                    if (retryPasswordChoiceAfterForgot == "1" || retryPasswordChoiceAfterForgot == "2")
                                        break;
                                    Console.WriteLine("Invalid choice, enter 1 or 2:");
                                }

                                if (retryPasswordChoiceAfterForgot == "2")
                                {
                                    Console.WriteLine("Exiting the system.");
                                    return;
                                }
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Invalid Login ID.");
                    Console.WriteLine("Would you like to see the correct Login ID?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");

                    string? viewLoginIDChoice;
                    while (true)
                    {
                        viewLoginIDChoice = Console.ReadLine();
                        if (viewLoginIDChoice == "1" || viewLoginIDChoice == "2")
                            break;
                        Console.WriteLine("Invalid choice, enter 1 or 2:");
                    }

                    if (viewLoginIDChoice == "1")
                    {
                        Console.WriteLine($"The correct Login ID is: {Login_ID}");
                    }

                    Console.WriteLine("Would you like to continue by entering the Login ID?");
                    Console.WriteLine("1. Yes");
                    Console.WriteLine("2. No");

                    string? retryLoginChoice;
                    while (true)
                    {
                        retryLoginChoice = Console.ReadLine();
                        if (retryLoginChoice == "1" || retryLoginChoice == "2")
                            break;
                        Console.WriteLine("Invalid choice, enter 1 or 2:");
                    }

                    if (retryLoginChoice == "2")
                    {
                        Console.WriteLine("Exiting the system.");
                        break;
                    }
                }
            }
        }
    }

}
