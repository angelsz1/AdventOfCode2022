namespace Advent11
{
    class Advent11
    {
        public static void Main(string[] args)
        {
            Solution s = new Solution("real_input_11.in");
            Console.WriteLine("Modulus = " + Solution.MODULUS);
            Console.WriteLine(s.Solve());
        }
    }

    class Solution
    {
        private StreamReader sr;
        private List<Monkey> monkeys;
        public static int MODULUS=1;
        public Solution(string path)
        {
            sr = new StreamReader(Advent2.Advent2.Path(path));
            monkeys = new List<Monkey>();
            SetMonkeys();
        }

        public long Solve()
        {
            
            for (int i = 0; i < 10000; i++) // 10k rondas
            {
                foreach (Monkey monkey in monkeys)
                {
                    int inspect = monkey.Inspect();
                    while (inspect != -1)
                    {
                        Throw(monkey.GetNumber(), inspect);
                        inspect = monkey.Inspect();
                    }
                }    
            }

            List<int> inspections = new List<int>();
            foreach (Monkey m in monkeys)
            {
                inspections.Add(m.GetInspections());
            }

            long max = inspections.Max();
            inspections.Remove(inspections.Max());


            return max * inspections.Max();
        }

        /**
         *          File structure
         * Monkey NUMBER:
         * Starting items: [NUMBER], [NUMBER]...
         * Operation : new = old +/*\/ [NUMBER][OLD]
         * Test : divisible by NUMBER
         * If true : throw to monkey NUMBER
         * If false : throw to monkey NUMBER
         * LINE JUMP
         */
        private void SetMonkeys()
        {
            string line = Read();

            while (line != "EOF")
            {
                Monkey curMonkey = new Monkey(int.Parse(line[7].ToString()));
                string[] items = Read().Substring(18).Split(',');
                foreach (string s in items)
                {
                    curMonkey.AddItem(new Item(int.Parse(s)));
                }

                string operation = Read().Split(':')[1].Split('=')[1];
                string[] operations = operation.Split(' ');
                curMonkey.SetOperation(operations);
                
                curMonkey.SetTest(int.Parse(Read().Split(':')[1].Split(' ')[3]));
                
                curMonkey.SetIfTrue(int.Parse(Read().Split(':')[1].Split(' ')[4]));
                curMonkey.SetIfFalse(int.Parse(Read().Split(':')[1].Split(' ')[4]));

                Console.WriteLine("This monkeys test " + curMonkey.GetTest());
                MODULUS *= curMonkey.GetTest();
                
                
                Read(); //line jump
                line = Read(); //Next monkey or EOF
                
                monkeys.Add(curMonkey);
            }
        }

        private string Read()
        {
            return sr.EndOfStream ? "EOF" : sr.ReadLine();
        }

        private void Throw(int from, int to)
        {
            monkeys[to].AddItem(monkeys[from].RemoveItem());
        }
    }


    class Monkey
    {
        private Queue<Item> items;
        private int number;
        private string[] operation;
        private int ifTrue;
        private int ifFalse;
        private int test;
        private int inspectionCounter = 0;

        public Monkey(int number)
        {
            this.number = number;
            items = new Queue<Item>();
        }

        public void AddItem(Item itemToAdd)
        {
            items.Enqueue(itemToAdd);
        }

        public Item RemoveItem()
        {
            return items.Dequeue();
        }

        public void SetOperation(string[] operation)
        {
            this.operation = operation;
        }

        public void SetIfTrue(int iftrue)
        {
            ifTrue = iftrue;
        }

        public void SetIfFalse(int ifFalse)
        {
            this.ifFalse = ifFalse;
        }

        public void SetTest(int test)
        {
            this.test = test;
        }

        public int GetNumber()
        {
            return number;
        }

        public int GetTest()
        {
            return test;
        }

        public int GetInspections()
        {
            return inspectionCounter;
        }

        public int Inspect()
        {
            if (items.Count == 0)
                return -1;
            
            Item item = items.Peek();
            inspectionCounter++;

            long operationNumber;

            if (operation[3] == "old")
                operationNumber = item.GetWorryLevel();
            else
                operationNumber = int.Parse(operation[3]);


            switch (operation[2])
            {
                case "+":
                    item.SetWorryLevel(item.GetWorryLevel() + operationNumber);
                    break;
                case "*":
                    item.SetWorryLevel(item.GetWorryLevel() * operationNumber);
                    break;
            }

            item.SetWorryLevel(item.GetWorryLevel() % Solution.MODULUS);

            return item.GetWorryLevel() % test == 0 ? ifTrue : ifFalse;
        }

        public override string ToString()
        {
            string monk = "Monkey " + number;

            monk += "\nStarting items: ";
            foreach (Item i in items)
            {
                monk += i.ToString() + " ";
            }

            monk += "\nOperation: new = " + "old"+ " " +operation[2] + " " + operation[3];

            monk += "\nTest: divisible by " + test;

            monk += "\n\tIf True: Throw to monkey " + ifTrue;
            monk += "\n\tIf False: Throw to monkey " + ifFalse;


            return monk;


        }
    }

    class Item
    {
        private long worryLevel;

        public Item(long w)
        {
            worryLevel = w;
        }

        public void SetWorryLevel(long worry)
        {
            worryLevel = worry;
        }

        public long GetWorryLevel()
        {
            return worryLevel;
        }

        public override string ToString()
        {
            return worryLevel.ToString();
        }
    }
}

