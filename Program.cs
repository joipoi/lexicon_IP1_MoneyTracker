using individualProject1_MoneyTracker;

var items = FileManager.ReadFromFile();
Account acc = new Account(items);
while (true)
{
    DisplayManager.ShowMainMenu(acc.Balance);
    string input = Console.ReadLine().Trim();

    switch (input)
    {
        case "1":
            DisplayManager.ShowItemMenu(acc.ItemList);
            break;
        case "2":
            DisplayManager.ShowAddMenu(items);
            break;
        case "3":
            DisplayManager.ShowEditMenu(items);
            break;
        case "4":
            DisplayManager.ShowRemoveMenu(items);
            break;
        case "5":
            FileManager.UpdateFile(items);
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Please choose a valid option");
            break;
    }
    acc.UpdateBalance();
}