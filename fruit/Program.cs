using Newtonsoft.Json;

namespace fruit
{
    internal class Program
    {
        #region classes
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
        #endregion

        static Fruit[]? DsFJ;
        static string Path = @".\Fruits.json";
        static string? name;


        static void Main(string[] args)
        {
            // Create Fruit object (deserialised fruit json)
            try { DsFJ = FruitJson(); }
            catch (FileNotFoundException)
            {
                Console.WriteLine("Data not found");
                return;
            }

            do
            {
                Console.Clear();
                Console.Write("Your Name: ");
                name = Console.ReadLine();
            } while (name == null || name == "");
            Console.Clear();

            //Console.WriteLine("Task 1");
            //Task1("Rosaceae");

            //Console.WriteLine("Task 2");
            //Task2();

            //Console.WriteLine("Task 3");
            //Task3();

            //Console.WriteLine("Task 4");
            //Task4();

            Console.WriteLine("Task 5");
            Task5();
        }

        private static void Task1(string family)
        {
            // list that gets the name of each fruit that meets the condition
            List<string> names = new List<string>();
            for (int i = 0; i < DsFJ!.Length; i++)
            {
                if (DsFJ[i].family == family)
                {
                    names.Add(DsFJ[i].name!);
                }
            }

            // writes the fruit names in a , seperated list. last , is replaced by "and"
            for (int i = 0; i < names.Count; i++)
            {
                Console.Write("{0}", names[i]);
                if (i == names.Count - 2) { Console.Write(" and "); }
                else if (i < names.Count - 1) { Console.Write(", "); }
            }
            Console.Write(" are all part of the {0} family\n\n", family);

            return;
        }

        private static void Task2()
        {
            // adds just the name of each fruit object so they can be sorted
            List<string> names = new List<string>();
            for (int i = 0; i < DsFJ!.Length; i++)
            {
                names.Add(DsFJ![i].name!);
            }

            // sorts the names...
            names.Sort();

            // writes each name. now in alphabetical order
            for (int i = names.Count - 1; i >= 0; i--) { Console.WriteLine(names[i]); }
            return;
        }

        private static void Task3()
        {
            // list that gets the name of each fruit that meets the condition
            List<string> names = new List<string>();
            for (int i = 0; i < DsFJ!.Length; i++)
            {
                if (DsFJ[i].nutritions!.calories > 100)
                {
                    names.Add(DsFJ[i].name!);
                }
            }

            // writes the fruit names in a , seperated list. last , is replaced by "and"
            for (int i = 0; i < names.Count; i++)
            {
                Console.Write("{0}", names[i]);
                if (i == names.Count - 2) { Console.Write(" and "); }
                else if (i < names.Count - 1) { Console.Write(", "); }
            }
            Console.Write(" all have more than 100 calories");

            return;
        }

        private static void Task4()
        {
            // makes a padding value to align each line
            int maxlength = DsFJ!.Max(x => x.name!.Length);

            // indicates what each column means
            Console.WriteLine("NAME".PadLeft(maxlength) + ": SUGAR");

            // writes to the console with each fruit and its sugar value (formatted)
            for (int i = 0; i < DsFJ!.Length; i++)
            {
                Console.WriteLine($"{DsFJ[i].name!.PadLeft(maxlength)}: {DsFJ[i].nutritions!.sugar}");
            }

            return;
        }

        private static void Task5()
        {
            // go through each fruit name and check if it starts with users first name
            List<string> names = new List<string>();
            for (int i = 0; i < DsFJ!.Length; i++)
            {
                if (DsFJ[i].name.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                {
                    names.Add(DsFJ[i].name);
                }
            }

            // writes the fruit names in a "," seperated list. last "," is replaced by "and"

            if (names.Count != 0)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    Console.Write("{0}", names[i]);
                    if (i == names.Count - 2) { Console.Write(" and "); }
                    else if (i < names.Count - 1) { Console.Write(", "); }
                }
                Console.Write(" Start with {0}", name); 
            }
            else { Console.WriteLine("There are no fruits that start with {0} in the given data"); }
            
            return;
        }

        private static Fruit[] FruitJson()
        {
            // check if file exists
            if (!File.Exists(Path)) { Console.WriteLine("File not found: " + Path); throw new FileNotFoundException { }; }

            // read file
            string jsonText = File.ReadAllText(Path);

            // deserialise json to an array of fruit objects
            Fruit[]? fruits = JsonConvert.DeserializeObject<Fruit[]>(jsonText);

            return fruits!;
        }
    }
}