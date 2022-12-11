
namespace ConsoleApp2
{

    class Solution
    {
        public static string Path(string path)
        {
            return "../../../" + path;
        }
        public static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Path("real_input.in"));
            string content = sr.ReadToEnd();
            string result = Sol(content, "TOP3");
            Console.WriteLine("El top 3 de elfos lleva " + result + " calorias.");
        }


        public static string Sol(string archivo, string cmd)
        {
            string[] allNumbers = archivo.Split("\n");

            List<int> elfsCalories = new List<int>();

            int curElf = 0;
            foreach (string str in allNumbers)
            {
                if (str.Length == 1)
                {
                    elfsCalories.Add(curElf);
                    curElf = 0;
                }
                else
                    curElf += int.Parse(str);
            }
            if(cmd == "MAX")
                return elfsCalories.Max().ToString();
            if (cmd == "TOP3")
            {
                int sum = elfsCalories.Max();
                elfsCalories.Remove(elfsCalories.Max());
                sum += elfsCalories.Max();
                elfsCalories.Remove(elfsCalories.Max());
                sum += elfsCalories.Max();

                return sum.ToString();
            }

            return "";
        }
    }

}