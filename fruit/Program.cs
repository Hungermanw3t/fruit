using Newtonsoft.Json;

namespace fruit
{
    internal class Program
    {
        static string Path = @".\Fruits.json";


        public class Fruit
        {
            public string? name { get; set; }
            public int id { get; set; }
            public string? family { get; set; }
            public string? order { get; set; }
            public string? genus { get; set; }
            public Nutritions? nutritions { get; set; }
        }

        public class Nutritions
        {
            public int calories { get; set; }
            public float fat { get; set; }
            public float sugar { get; set; }
            public float carbohydrates { get; set; }
            public float protein { get; set; }
        }


        static void Main(string[] args)
        {
            // Create Fruit object (deserialised fruit json)
            Fruit[]? DsFJ = FruitJson();
        }

        private static Fruit[]? FruitJson()
        {
            // check if file exists
            if (!File.Exists(Path)) { Console.WriteLine("File not found: " + Path); throw new FileNotFoundException { }; }

            // read file
            string jsonText = File.ReadAllText(Path);

            // deserialise json to an array of fruit objects
            Fruit[]? fruits = JsonConvert.DeserializeObject<Fruit[]>(jsonText);

            return fruits;
        }
    }
}
