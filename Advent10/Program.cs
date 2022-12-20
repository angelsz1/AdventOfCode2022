namespace Advent10
{
    class Advent10
    {
        public static void Main(string[] args)
        {
            Solution s = new Solution("real_input_10.in");
            
            Console.WriteLine(s.Solve());
        }
    }

    class Solution
    {
        private StreamReader sr = null;
        private CPU cpu;
        private int SumOfSignals = 0;

        public Solution(string path)
        {
            sr = new StreamReader(Advent2.Advent2.Path(path));
            cpu = new CPU();
        }

        private string Read()
        {
            return sr.EndOfStream ? "EOF" : sr.ReadLine();
        }

        public int Solve()
        {
            string line = Read();

            while (line != "EOF")
            {
                string[] cmd = line.Split(' ');
                string ret;
                if (cmd[0] == "addx")
                    ret = cpu.addx(int.Parse(cmd[1]));
                else
                    ret = cpu.noop();

                if (ret != "NO_RET")
                    SumOfSignals += int.Parse(ret);
                line = Read();
            }
            
            cpu.ShowCRT();

            return SumOfSignals;
        }
        
        
        
    }


    class CRT
    {
        private char[,] matrix = new char[6,40];
        private int internalClock = 0;
        private int rowSelector = 0;

        public void Draw(int reg_x)
        {
            matrix[rowSelector, internalClock] = CheckValues(reg_x) ? '#' : ' ';
            Console.WriteLine(internalClock);
            internalClock = (internalClock + 1) % 40;
            rowSelector = internalClock == 0 ? rowSelector + 1 : rowSelector;
        }

        private bool CheckValues(int x)
        {
            if (x == 0 || x == 39)
                if (x == 0)
                    return internalClock == x || internalClock + 1 == x || internalClock + 2 == x;
                else
                    return internalClock == x || internalClock - 1 == x || internalClock - 2 == x;
            return internalClock == x || internalClock - 1 == x || internalClock + 1 == x;
        }

        public void Show()
        {
            for (int i = 0; i < 6; i++)
            {
                for (int j = 0; j < 40; j++)
                {
                    Console.Write(matrix[i,j]);
                }
                Console.WriteLine();
            }
        }

    }

    class CPU
    {
        private int clock = 0;
        private CRT crt;

        private int reg_x = 1;

        public CPU()
        {
            crt = new CRT();
        }

        public void ShowCRT()
        {
            crt.Show();
        }

        private void IncClock()
        {
            clock++;
            crt.Draw(reg_x);
        }
        public string addx(int v)
        {
            string ret = "NO_RET";

            IncClock();

            if (CheckIfReport())
                ret = (reg_x * clock).ToString();

            IncClock();
            
            
            if (CheckIfReport())
                ret = (reg_x * clock).ToString();
            
            reg_x += v;

            return ret;

        }

        public string noop()
        {
            string ret = "NO_RET";

            IncClock();
            
            if (CheckIfReport())
                ret = (reg_x * clock).ToString();

            return ret;
        }

        private bool CheckIfReport()
        {
            return clock == 20 || clock == 60 || clock == 100 || clock == 140 || clock == 180 || clock == 220;
        }
        
    }
}

