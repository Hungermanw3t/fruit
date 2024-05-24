using Newtonsoft.Json;
using System.Reflection;

namespace fruit
{
    internal class Program
    {
        class Fruit
        {
            string name;
        }

        static string Path = @".\Fruits.json";
        static void Main(string[] args)
        {

            
        }

        static void FruitJson()
        {
            // check if file exists
            if (!File.Exists(Path)) { Console.WriteLine("File not found: " + Path); return; }

            // read file
            string jsonText = File.ReadAllText(Path);

            // deserialise json to an array of fruit objects
            Fruit[] fruits = JsonConvert.DeserializeObject<Fruit[]> (jsonText);

        }
    }
}
