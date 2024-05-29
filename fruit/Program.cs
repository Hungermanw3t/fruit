using Newtonsoft.Json;
using System.Text;

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
            public Nutritions nutritions { get; set; }
        }

        public class Nutritions
        {
            public int? calories { get; set; }
            public float? fat { get; set; }
            public float? sugar { get; set; }
            public float? carbohydrates { get; set; }
            public float? protein { get; set; }
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


            // task 1
            CustomWrite("Task 1\n");
            Task1(GetValidFam()); ;
            WaitForKey();


            // task 2
            CustomWrite("Task 2\n");
            Task2();
            WaitForKey();


            // task 3
            CustomWrite("Task 3\n");
            Task3();
            WaitForKey();


            // task 4
            CustomWrite("Task 4\n");
            Task4();
            WaitForKey();


            // task 5
            CustomWrite("Task 5\n");
            Task5();
            WaitForKey();

            // task 6
            CustomWrite("Task 6\n");
            Task6();
        }

        #region utils
        private static void CustomWrite(string buffer)
        {
            for (int i = 0; i < buffer.Length; i++)
            {
                Console.Write(buffer[i]);
                Thread.Sleep(1);
            }
        }
        
        private static int CustomReadKey(int length, string prompt)
        {
            int index = 0;
            int digits = GetDigits(length);

            // write prompt
            CustomWrite(prompt);
            do
            {
                // get the next input
                ConsoleKeyInfo tmpStr = Console.ReadKey(false); ;
                if (tmpStr.KeyChar == 13)
                {
                    break;
                }
                else if (!int.TryParse(tmpStr.KeyChar.ToString(), out _))
                {
                    continue;
                }

                index = Convert.ToInt32($"{index}" + tmpStr.KeyChar);

            } while (GetDigits(index) < digits);

            return index;
        }

        private static int GetDigits(int number)
        {
            // if number is larget than 0 then log10(number) else number 
            number = number > 0 ? (int)Math.Log10(number) + 1 : 1;

            return number;
        }

        private static string GetValidFam()
        {
            bool IsValid = false;
            string? usrFam;
            string CorrectFam = "";
            do
            {
                CustomWrite("Enter family: ");
                usrFam = Console.ReadLine();
                for (int i = 0; i < DsFJ!.Length; i++)
                {
                    if (DsFJ[i].family!.Equals(usrFam, StringComparison.OrdinalIgnoreCase))
                    {
                        IsValid = true;
                        CorrectFam = DsFJ[i].family!;
                    }
                }
                Console.Clear();
            } while (!IsValid);

            return CorrectFam;
        }

        private static void WaitForKey()
        {
            CustomWrite("\nPress any key to continue the program . . .");
            Console.ReadKey();
            Console.Clear();
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
        #endregion

        #region tasks
        private static void Task1(string family)
        {
            StringBuilder sb = new StringBuilder();

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
                sb.Append($"{names[i]}");
                if (i == names.Count - 2) { sb.Append(" and "); }
                else if (i < names.Count - 1) { sb.Append(", "); }
            }
            sb.Append($" are all part of the {family} family\n\n");

            CustomWrite(sb.ToString());
            return;
        }

        private static void Task2()
        {
            StringBuilder sb = new StringBuilder();

            // adds just the name of each fruit object so they can be sorted
            List<string> names = new List<string>();
            for (int i = 0; i < DsFJ!.Length; i++)
            {
                names.Add(DsFJ![i].name!);
            }

            // sorts the names...
            names.Sort();

            // writes each name. now in alphabetical order
            for (int i = names.Count - 1; i >= 0; i--)
            {
                sb.Append($"{names[i]}");
                if (i < names.Count) { sb.Append(", "); }
            }
            sb.Append(" \nIn reverse order\n");

            CustomWrite(sb.ToString());
            return;
        }

        private static void Task3()
        {
            StringBuilder sb = new StringBuilder();

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
                sb.Append($"{names[i]}");
                if (i == names.Count - 2) { sb.Append(" and "); }
                else if (i < names.Count - 1) { sb.Append(", "); }
            }
            sb.Append(" all have more than 100 calories");

            CustomWrite(sb.ToString());

            return;
        }

        private static void Task4()
        {
            StringBuilder sb = new StringBuilder();

            // makes a padding value to align each line
            int maxlength = DsFJ!.Max(x => x.name!.Length);

            // indicates what each column means
            sb.AppendLine("NAME".PadLeft(maxlength) + ": SUGAR");

            // writes to the console with each fruit and its sugar value (formatted)
            for (int i = 0; i < DsFJ!.Length; i++)
            {
                sb.AppendLine($"{DsFJ[i].name!.PadLeft(maxlength)}: {DsFJ[i].nutritions!.sugar}");
            }
            CustomWrite(sb.ToString());

            Console.WriteLine();

            return;
        }

        private static void Task5()
        {
            StringBuilder sb = new StringBuilder();

            // gets users name
            CustomWrite("Your Name: ");
            name = Console.ReadLine();
            while (name == null || name == "")
            {
                Console.Clear();
                CustomWrite("Your Name: ");
                name = Console.ReadLine();
            }
            Console.Clear();

            // go through each fruit name and check if it starts with users name
            List<string> names = new List<string>();
            for (int i = 0; i < DsFJ!.Length; i++)
            {
                if (DsFJ[i].name!.StartsWith(name, StringComparison.OrdinalIgnoreCase))
                {
                    names.Add(DsFJ[i].name!);
                }
            }

            // writes the fruit names in a "," seperated list. last "," is replaced with "and"

            if (names.Count != 0)
            {
                for (int i = 0; i < names.Count; i++)
                {
                    
                    sb.Append($"{names[i]}");
                    if (i == names.Count - 2) { sb.Append(" and "); }
                    else if (i < names.Count - 1) { sb.Append(", "); }
                }
                if (names.Count != 1)
                {
                    sb.Append($" start with {name}");
                }
                else sb.Append($" starts with {name}");
            }
            else { sb.AppendLine($"There are no fruits that start with {name} in the given data"); }
            
            CustomWrite(sb.ToString());

            return;
        }

        private static void Task6()
        {
            
            // outputs each fruit and their index
            for (int i = 0; i < DsFJ!.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {DsFJ[i].name}");
            }

            // get input
            int index = CustomReadKey(DsFJ.Length, "\nSelect fruit by typing number: ") - 1;
            // makes sure anything after is after the read key
            Console.WriteLine();

            string finalOut = $@"

Name: {DsFJ[index].name} 
Id: {DsFJ[index].id}
Family: {DsFJ[index].family}
Order: {DsFJ[index].order}
Genus: {DsFJ[index].genus}
Nutrition
    Calories: {DsFJ[index].nutritions.calories}
    Fat: {DsFJ[index].nutritions.fat}
    Sugar: {DsFJ[index].nutritions.sugar}
    Carbohydrates: {DsFJ[index].nutritions.carbohydrates}
    Protein: {DsFJ[index].nutritions.protein}
";

            CustomWrite(finalOut);
            
        }
        #endregion


    }
}