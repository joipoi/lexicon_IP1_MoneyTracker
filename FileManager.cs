using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace individualProject1_MoneyTracker
{
    static class FileManager
    {
        private static string fileName = "data.json";
        private static string dir = Directory.GetCurrentDirectory();
        private static string path = Path.Combine(dir, fileName);

        public static void UpdateFile(List<Item> itemList)
        {
            string jsonString = JsonSerializer.Serialize(itemList);
            File.WriteAllText(path, jsonString);

            Console.WriteLine("Data has been saved to file");
        }

        public static List<Item> ReadFromFile()
        {

            if (File.Exists(path))
            {
                string jsonString = File.ReadAllText(path);

                List<Item> itemList = JsonSerializer.Deserialize<List<Item>>(jsonString);

                return itemList;
            }
            else
            {
                return new List<Item>(); 
            }
        }

    }
}
