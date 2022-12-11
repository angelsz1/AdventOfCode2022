using System.Collections;

namespace Advent5
{
    class Advent5
    {
        static List<Stack<char>> stacks = new List<Stack<char>>();
        private static List<List<char>> lists = new List<List<char>>();

        public static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Advent2.Advent2.Path("real_input_5.in"));
            setUpStacks(sr);
            //makeChanges(sr);     solution1
            makeNewChanges(sr); // solution2
            
            Console.WriteLine(Solution());
        }


        public static void makeNewChanges(StreamReader streamReader)
        {  
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                string[] things = line.Split(' '); // importantes son 1, 3 y 5
                int amount = int.Parse(things[1]);
                int from = int.Parse(things[3]);
                int to = int.Parse(things[5]);
                Stack<char> auxStack = new Stack<char>();
                while (amount > 0)
                {
                    char item = stacks[from - 1].Pop();
                    //Console.WriteLine("Saco a " + item + " del stack " + from + " y lo mando al stack " + to);
                    auxStack.Push(item);
                    amount--;
                }

                while (auxStack.Count != 0)
                {
                    char item = auxStack.Pop();
                    stacks[to -1].Push(item);
                }
            }
        }

        public static string Solution()
        {
            string solution = "";

            for (int i = 0; i < stacks.Count; i++)
                if(stacks[i].Count != 0)
                    solution += stacks[i].Peek();

            return solution;
        }
        public static void makeChanges(StreamReader streamReader)
        {

            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                string[] things = line.Split(' '); // importantes son 1, 3 y 5
                int amount = int.Parse(things[1]);
                int from = int.Parse(things[3]);
                int to = int.Parse(things[5]);

                while (amount > 0)
                {
                    char item = stacks[from - 1].Pop();
                    //Console.WriteLine("Saco a " + item + " del stack " + from + " y lo mando al stack " + to);
                    stacks[to-1].Push(item);
                    amount--;
                }
            }
            
        }
        
        public static void setUpStacks(StreamReader streamReader)
        {
            string line = streamReader.ReadLine();
            for (int i = 0; i < line.Length/3; i++){
                lists.Add(new List<char>());
            }
            while (line[1] != '1') // aca me fijo de no tomar en cuenta la linea que los numera
            {
                int index = 0;
                for (int i = 1; i < line.Length; i += 4)
                {
                    if (line[i] != ' ')
                    {
                        lists[index].Add(line[i]);
                    }
                    //Console.WriteLine("Index: " + index + ", stacks size: " + stacks.Count);
                    index++;
                }

                line = streamReader.ReadLine();
            }
            
            
            for (int i = 0; i < line.Length/3; i++)
            {
                lists[i].Reverse();
                stacks.Add(new Stack<char>(lists[i]));
            }

            streamReader.ReadLine();
        }
    }
}

