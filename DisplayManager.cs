using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace individualProject1_MoneyTracker
{
    static class DisplayManager
    {
        public static void ShowMainMenu(decimal balance)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Welcome to TrackMoney");
            Console.ResetColor();
            Console.WriteLine($"You currently have {balance.ToString("C", CultureInfo.CurrentCulture)} on your account");
            Console.WriteLine("Pick an option");
            Console.WriteLine("(1) Show items (All/Expenses/Incomes)");
            Console.WriteLine("(2) Add New Expense/Income");
            Console.WriteLine("(3) Edit Item");
            Console.WriteLine("(4) Remove Item");
            Console.WriteLine("(5) Save and Quit");
        }
        public static void ShowItemMenu(List<Item> itemList)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Show Items, select display options");
            Console.ResetColor();

            Console.WriteLine("Sort by (title/amount/month)");
            string sortBy = Console.ReadLine().ToLower().Trim();

            Console.WriteLine("Sort by (asc/desc)");
            bool ascDesc = Console.ReadLine().ToLower().Trim() == "asc" ? true : false;

            Console.WriteLine("Show (incomes/expenses/all)");
            string show = Console.ReadLine().ToLower().Trim();

            DisplayItems(itemList, sortBy, ascDesc, show);
        }
        public static void ShowAddMenu(List<Item> items)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Add new Expense");
            Console.ResetColor();

            Console.WriteLine("Enter Title: ");
            string title = Console.ReadLine();

            Console.WriteLine("Enter Amount: ");

            decimal amount;
            while (!decimal.TryParse(Console.ReadLine(), out amount))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a valid amount.");
                Console.ResetColor();
            }

            Console.WriteLine("Enter date in format(yyyy-MM-dd): ");
            DateTime itemDate;

            while (!DateTime.TryParse(Console.ReadLine(), out itemDate))
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please enter a date in valid format(yyyy-MM-dd)");
                Console.ResetColor();
            }

            Console.WriteLine("Is it en expense(Y/N): ");
            bool isExpense = Console.ReadLine().ToLower().Trim() == "y" ? true : false;

            Item item = new Item(title,amount, itemDate, isExpense);
            items.Add(item);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Succesfully added item");
            Console.ResetColor();
        }
        public static void ShowEditMenu(List<Item> items)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Choose item to edit");
            Console.ResetColor();

            for (int i = 0; i < items.Count(); i++)
            {
                Console.WriteLine($"({i}) {items[i].ToString()}");
            }

            int index;
            while (!int.TryParse(Console.ReadLine(), out index) || index < 0 || index >= items.Count)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please choose a valid item");
                Console.ResetColor();
            }

            Console.WriteLine("Choose properties to change");
            Console.WriteLine("(1) title");
            Console.WriteLine("(2) amount");
            Console.WriteLine("(3) date");

            int propIndex;
            while (!int.TryParse(Console.ReadLine(), out propIndex) || propIndex <= 0 || propIndex >= 4)
            {
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Please choose a valid property");
                Console.ResetColor();
            }

            Console.WriteLine("Choose new value for that property");
            string newValue;

            var item = items[index];
            switch (propIndex)
            {
                case 1:
                    newValue = Console.ReadLine();
                    item.Title = newValue; 
                    break;
                case 2:
                    decimal amountInput;
                    while (!decimal.TryParse(Console.ReadLine(), out amountInput) || amountInput < 0)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter a valid amount.");
                        Console.ResetColor();
                       
                    }
                    item.Amount = amountInput;
                    break;
                case 3:
                    DateTime itemDate;
                    while (!DateTime.TryParse(Console.ReadLine(), out itemDate))
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter a date in valid format(yyyy-MM-dd)");
                        Console.ResetColor();
                    }
                    item.ItemDate = itemDate;
                    break;
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Succesfully edited item");
            Console.ResetColor();
        }
        public static void ShowRemoveMenu(List<Item> items)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("Choose item to remove");
            Console.ResetColor();

            for (int i = 0; i < items.Count(); i++)
            {
                Console.WriteLine($"({i}) {items[i].ToString()}");
            }
            int index = int.Parse(Console.ReadLine());
            items.RemoveAt(index);

            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("Succesfully removed item");
            Console.ResetColor();

        }

        //this method was helped a lot by ai
        //it first checks if it should be expenses/incomes or all
        //then it checks which property to sort by and ascending or descending
        //if the user gives wrong input, the list just won't be sorted
        private static void DisplayItems(List<Item> itemList, string sortedBy, bool isAsc, string show)
        {
            if (show == "expenses" || show == "incomes")
            {
                bool isExpenseFilter = show == "expenses";
                itemList = itemList.Where(item => item.IsExpense == isExpenseFilter).ToList();
            }

            switch (sortedBy.ToLower())
            {
                case "title":
                    itemList = isAsc ? itemList.OrderBy(item => item.Title).ToList() : itemList.OrderByDescending(item => item.Title).ToList();
                    break;
                case "amount":
                    itemList = isAsc ? itemList.OrderBy(item => item.Amount).ToList() : itemList.OrderByDescending(item => item.Amount).ToList();
                    break;
                case "month":
                    itemList = isAsc ? itemList.OrderBy(item => item.ItemDate).ToList() : itemList.OrderByDescending(item => item.ItemDate).ToList();
                    break;
            }

            Console.WriteLine("----------------------------------------------------------------------");
            Console.ForegroundColor = ConsoleColor.Cyan;
            Console.WriteLine("Title".PadRight(20) + "Amount".PadRight(20) + "Month".PadRight(20));
            Console.ResetColor();
            foreach (var item in itemList)
            {
                Console.WriteLine(item.ToString());
            }
            Console.WriteLine("----------------------------------------------------------------------");
        }
    }
}
