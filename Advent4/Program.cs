
namespace Advent4
{
    class Advent4
    {
        public static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Advent2.Advent2.Path("real_input_4.in"));
            int value = Solution(sr);
            
            Console.WriteLine("There are " + value + " pairs that COMPLETELY overlap.");
        }

        public static int Solution(StreamReader streamReader)
        {
            int overlapCases = 0;
            while (!streamReader.EndOfStream)
            {
                string line = streamReader.ReadLine();
                string[] nums = line.Split('-',',');
                

                int firstElfLB = int.Parse(nums[0]); 
                
                int firstElfUB = int.Parse(nums[1]); 
                
                int secondElfLB = int.Parse(nums[2]); 
                
                int secondElfUB = int.Parse(nums[3]); 


                if (ElfContains(firstElfLB, firstElfUB, secondElfLB, secondElfUB) ||
                    ElfContains(secondElfLB, secondElfUB, firstElfLB, firstElfUB))
                    overlapCases++;

            }

            return overlapCases;
        }

        public static bool ElfContains(int lb_1, int ub_1, int lb_2, int ub_2)
        {
            Console.WriteLine(lb_1 + "-" + ub_1 + ", " + lb_2 + "-" + ub_2);
            return lb_1 >= lb_2 && lb_1 <= ub_2 || ub_1 >= lb_2 && ub_1 <= ub_2;
        }
    }
}