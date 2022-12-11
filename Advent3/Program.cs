using System.Collections;

namespace Advent3
{

    class Advent3
    {
        private static Hashtable myTable;
        
        public static string Path(string path)
        {
            return "../../../" + path;
        }

        public static void setUpTable()
        {
            myTable = new Hashtable();
            char[] letters =
            {
                'a', 'b', 'c', 'd', 'e', 'f', 'g', 'h', 'i', 'j', 'k', 'l', 'm', 'n', 'o', 'p', 'q', 'r', 's', 't',
                'u', 'v', 'w', 'x', 'y', 'z', 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O',
                'P', 'Q', 'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };

            for (int i = 0; i < letters.Length; i++ )
            {
                myTable.Add(letters[i], i+1 );
            }
        }
        

        public static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Path("real_input_3.in"));
            setUpTable();
            int value = Solution2(sr);
            Console.WriteLine("The sum of the priorities is " + value);
        }


        public static int Solution(StreamReader streamReader)
        {
            int returnValue = 0;
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                char repeatedChar = oddOneOut(line);
                returnValue += (int)myTable[repeatedChar];
            }

            return returnValue;
        }

        public static char oddOneOut(string line)
        {
            string firstHalf = line.Substring(0, line.Length / 2);
            string secondHalf = line.Substring(line.Length / 2);
            List<char> fhChars = new List<char>();
            //List<Character> fhChars = new List<>();
            foreach (char c in firstHalf)
                fhChars.Add(c);

            foreach (char c in secondHalf)
                if (fhChars.Contains(c))
                    return c;
            
            return ' ';
        }


        public static int Solution2(StreamReader streamReader)
        {
            int result = 0;
            while (!streamReader.EndOfStream)
            {
                string line1 = streamReader.ReadLine();
                string line2 = streamReader.ReadLine();
                string line3 = streamReader.ReadLine();

                result += (int)myTable[repeatedOne(line1, line2, line3)];
                
            }

            return result;
        }

        public static char repeatedOne(string l1, string l2, string l3)
        {
            List<char> elements = new List<char>();
            List<char> repeated = new List<char>();
            foreach (char c in l1)
                elements.Add(c);

            foreach (char c in l2)
            {
                if(elements.Contains(c))
                    repeated.Add(c);
            }

            foreach (char c in l3)
            {
                if (repeated.Contains(c))
                    return c;
            }

            return ' ';
        }
        
    }
    
    
}
