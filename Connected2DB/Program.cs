using Connected2DB.DAL;
using Connected2DB.Model;
using System;
using System.Collections.Generic;
using Microsoft.Data.SqlClient;

namespace Connected2DB
{
    internal class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                DBContext dbContext = new DBContext();
                int noOFRowsAfftected = 0;
                Console.WriteLine("Enter DB Operation Choice: 1. Select, 2. Insert, 3. Update, 4. Delete");
                int opChoice = Convert.ToInt32(Console.ReadLine());

                switch (opChoice)
                {
                    case 1:
                        // Select all records
                        List<Emp> allEmployees = dbContext.SelectRecords();
                        foreach (var emp in allEmployees)
                        {
                            Console.WriteLine($"Id: {emp.Id}, Name: {emp.Name}, Address: {emp.Address}");
                        }
                        break;

                    case 2:
                        // Insert a new record
                        Emp empToBeInserted = new Emp();
                        Console.WriteLine("Enter Id: ");
                        int enteredId = Convert.ToInt32(Console.ReadLine());

                        // Check if the Id already exists in the database
                        bool idExists = dbContext.CheckIfIdExists(enteredId);
                        if (idExists)
                        {
                            Console.WriteLine("The Id already exists. Please provide a unique Id.");
                            break; // Exit the loop if the Id exists
                        }

                        empToBeInserted.Id = enteredId;
                        Console.WriteLine("Enter Name: ");
                        empToBeInserted.Name = Console.ReadLine();
                        Console.WriteLine("Enter Address: ");
                        empToBeInserted.Address = Console.ReadLine();

                        // Insert the new record into the database
                        noOFRowsAfftected = dbContext.InsertRecords(empToBeInserted);
                        if (noOFRowsAfftected > 0)
                        {
                            Console.WriteLine("Record inserted into DB successfully.");
                        }
                        break;

                    case 3:
                        // Update a record
                        Emp empToBeUpdated = new Emp();
                        Console.WriteLine("Enter Id:");
                        empToBeUpdated.Id = Convert.ToInt32(Console.ReadLine());
                        Console.WriteLine("Enter Name:");
                        empToBeUpdated.Name = Console.ReadLine();
                        Console.WriteLine("Enter Address:");
                        empToBeUpdated.Address = Console.ReadLine();
                        noOFRowsAfftected = dbContext.UpdateRecords(empToBeUpdated);
                        if (noOFRowsAfftected > 0)
                        {
                            Console.WriteLine("Records updated in DB successfully");
                        }
                        break;

                    case 4:
                        // Delete a record
                        Console.WriteLine("Enter Id:");
                        int id = Convert.ToInt32(Console.ReadLine());
                        noOFRowsAfftected = dbContext.DeleteRecords(id);
                        if (noOFRowsAfftected > 0)
                        {
                            Console.WriteLine("Records deleted from DB successfully");
                        }
                        break;

                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

                Console.WriteLine("Do you wish to continue? (y/n)");
                string ynChoice = Console.ReadLine();
                if (ynChoice.ToLower() == "n")
                {
                    break;
                }
            }
        }
    }
}
