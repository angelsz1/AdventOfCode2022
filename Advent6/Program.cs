namespace Advent6
{
    class Advent6
    {
        public static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Advent2.Advent2.Path("real_input_6.in"));
            Console.WriteLine(Solution(sr));
        }

        public static int Solution(StreamReader streamReader)
        {
            string line = streamReader.ReadLine();
            List<char> charSoFar = new List<char>();

            for (int i = 0; i < line.Length - 14; i++)
            {
                bool repe = false;
                for (int j = i; j < i + 14; j++)
                {
                    //Console.WriteLine(line[j]);
                    if (!charSoFar.Contains(line[j]))
                        charSoFar.Add(line[j]);
                    else
                        repe = true;
                }

                if (!repe)
                    return i + 14;
                charSoFar.Clear();

            }

            return 0;
        }
    }
}

