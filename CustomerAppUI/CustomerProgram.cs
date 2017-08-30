using System;
using CustomerAppUI.Model;
using RestAppBLL;
using RestAppBLL.BusinessObjects;

namespace RestAppUI
{
    internal static class CustomerProgram
    {
        private static readonly BLLFacade BLLFacade = new BLLFacade();
        private static readonly MenuModel MenuModel = new MenuModel();
        private static bool _userIsDone;

        private static void Main(string[] args)
        {
            while (!_userIsDone)
            {
                ShowMenu(MenuModel.MenuItems);

                var userSelection = GetInputFromUser();

                ReactToUserInput(userSelection);
            }

            Console.WriteLine("Bye bye!");
        }

        /// <summary>
        ///     React to the user input
        /// </summary>
        /// <param name="selection"></param>
        private static void ReactToUserInput(int selection)
        {
            switch (selection)
            {
                case 1:
                    ListCustomers();
                    break;
                case 2:
                    AddCustomers();
                    break;
                case 3:
                    ListCustomers();
                    DeleteCustomer();
                    break;
                case 4:
                    ListCustomers();
                    EditCustomer();
                    break;
                case 5:
                    _userIsDone = true;
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        ///     Propmt user for id to edit a customer
        /// </summary>
        private static void EditCustomer()
        {
            var customer = FindCustomerById();
            if (customer != null)
            {
                Console.Write("FirstName: ");
                customer.FirstName = Console.ReadLine();
                Console.Write("LastName: ");
                customer.LastName = Console.ReadLine();
                Console.Write("Address: ");
                customer.Address = Console.ReadLine();
                BLLFacade.CustomerService.Update(customer);
            }
            else
            {
                Console.WriteLine("Customer not Found!");
            }
        }

        /// <summary>
        ///     Prompt user for id to find customer
        /// </summary>
        /// <returns>Customer with parsed id</returns>
        private static CustomerBO FindCustomerById()
        {
            Console.Write("Insert Customer Id: ");
            int id;
            while (!int.TryParse(Console.ReadLine(), out id))
                Console.WriteLine("Please insert a number");
            return BLLFacade.CustomerService.GetById(id);
        }

        /// <summary>
        ///     Prompt user for id for customer to delete
        /// </summary>
        private static void DeleteCustomer()
        {
            Console.WriteLine("Please write id of customer to delete");
            var customerFound = FindCustomerById();
            if (customerFound != null)
                BLLFacade.CustomerService.Delete(customerFound.Id);

            var response = customerFound == null ? "Customer not found" : "Customer was deleted";
            Console.WriteLine(response);
        }

        /// <summary>
        ///     Prompt user for information to add new customer
        /// </summary>
        private static void AddCustomers()
        {
            Console.Write("Firstname: ");
            var firstName = Console.ReadLine();

            Console.Write("Lastname: ");
            var lastName = Console.ReadLine();

            Console.Write("Address: ");
            var address = Console.ReadLine();

            var createdCustomer = BLLFacade.CustomerService.Create(new CustomerBO
            {
                FirstName = firstName,
                LastName = lastName,
                Address = address
            });
            Console.WriteLine("\nCustomer created:");
            DisplayCustomer(createdCustomer);
        }

        /// <summary>
        ///     List all customers
        /// </summary>
        private static void ListCustomers()
        {
            Console.WriteLine("\nList of Customers");
            foreach (var customer in BLLFacade.CustomerService.GetAll())
                DisplayCustomer(customer);
        }

        private static void DisplayCustomer(CustomerBO customerToDispaly)
        {
            Console.WriteLine($"Id: {customerToDispaly.Id} Name: {customerToDispaly.FullName()} Adress: {customerToDispaly.Address}");
        }


        private static void ShowMenu(string[] menuItems)
        {
            Console.WriteLine("\nSelect What you want to do:\n");

            for (var i = 0; i < menuItems.Length; i++)
                //Console.WriteLine((i + 1) + ":" + menuItems[i]);
                Console.WriteLine($"{i + 1}: {menuItems[i]}");
        }

        /// <summary>
        ///     Get input from user
        /// </summary>
        /// <returns></returns>
        private static int GetInputFromUser()
        {
            Console.Write("Your input: ");
            int selection;
            while (!int.TryParse(Console.ReadLine(), out selection)
                   || selection < 1
                   || selection > 5)
                Console.WriteLine("Please select a number between 1-5");

            return selection;
        }
    }
}