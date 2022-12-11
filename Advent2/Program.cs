namespace Advent2
{

    enum Values
    {
        NONE, ROCK, PAPER, SCISSORS
    }

    internal class Constants
    {
        public const char E_ROCK = 'A';
        public const char E_PAPER = 'B';
        public const char E_SCISSORS = 'C';

        public const char ROCK = 'X';
        public const char PAPER = 'Y';
        public const char SCISSORS = 'Z';

        public const char NEED_TO_LOSE = 'X';
        public const char NEED_TO_DRAW = 'Y';
        public const char NEED_TO_WIN  = 'Z';
        
        public const int LOSE = 0;
        public const int DRAW = 3;
        public const int WIN = 6;
    }

    public class Advent2
    {
        public static string Path(string path)
        {
            return "../../../" + path;
        }

        public static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Path("real_input_2.in"));
            int totalPoints = 0;
            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                char oppPlay = line[0];
                char myPlay = line[2];
                totalPoints += PointsCalculator2(oppPlay, myPlay);
            }

            Console.WriteLine(totalPoints);
            sr.Close();
        }

        public static int PointsCalculator(char op, char mp)
        {
            int ret = 0;

            switch (mp)
            {
                case Constants.ROCK:
                    ret += (int)Values.ROCK;
                    switch (op)
                    {
                        case Constants.E_ROCK:
                            ret += Constants.DRAW;
                            break;
                        case Constants.E_SCISSORS:
                            ret += Constants.WIN;
                            break;
                        case Constants.E_PAPER:
                            ret += Constants.LOSE;
                            break;
                    }
                    break;
                case Constants.PAPER:
                    ret += (int)Values.PAPER;
                    switch (op)
                    {
                        case Constants.E_ROCK:
                            ret += Constants.WIN;
                            break;
                        case Constants.E_SCISSORS:
                            ret += Constants.LOSE;
                            break;
                        case Constants.E_PAPER:
                            ret += Constants.DRAW;
                            break;
                    }
                    break;
                case Constants.SCISSORS:
                    ret += (int)Values.SCISSORS;
                    switch (op)
                    {
                        case Constants.E_ROCK:
                            ret += Constants.LOSE;
                            break;
                        case Constants.E_SCISSORS:
                            ret += Constants.DRAW;
                            break;
                        case Constants.E_PAPER:
                            ret += Constants.WIN;
                            break;
                    }
                    break;

            }

            return ret;
        }

        public static int PointsCalculator2(char oppPlay, char needTo)
        {
            int ret = 0;
            switch (needTo)
            {
                case Constants.NEED_TO_WIN:
                    ret += Constants.WIN;
                    switch (oppPlay)
                    {
                        case Constants.E_ROCK:
                            ret += (int)Values.PAPER;
                            break;
                        case Constants.E_SCISSORS:
                            ret += (int)Values.ROCK;
                            break;
                        case Constants.E_PAPER:
                            ret += (int)Values.SCISSORS;
                            break;
                    }
                    break;
                case Constants.NEED_TO_DRAW:
                    ret += Constants.DRAW;
                    switch (oppPlay)
                    {
                        case Constants.E_ROCK:
                            ret += (int)Values.ROCK;
                            break;
                        case Constants.E_SCISSORS:
                            ret += (int)Values.SCISSORS;
                            break;
                        case Constants.E_PAPER:
                            ret += (int)Values.PAPER;
                            break;
                    }
                    break;
                case Constants.NEED_TO_LOSE:
                    ret += Constants.LOSE;
                    switch (oppPlay)
                    {
                        case Constants.E_ROCK:
                            ret += (int)Values.SCISSORS;
                            break;
                        case Constants.E_SCISSORS:
                            ret += (int)Values.PAPER;
                            break;
                        case Constants.E_PAPER:
                            ret += (int)Values.ROCK;
                            break;
                    }
                    break;
            }

            return ret;
        }

    }

}

