

namespace Advent9
{
    class Advent9
    {
        public static readonly int STARTING_POINT = 10;
        public static void Main(string[] args)
        {
            Solution s = new Solution(Advent2.Advent2.Path("real_input_9.in"));
            s.RunSolution(2);
            Console.WriteLine(s.GetResult());

        }
    }

    class Solution
    {
        private HashSet<Position> TailHasBeen = null;
        private Position tailCurrentPosition = new Position(Advent9.STARTING_POINT, Advent9.STARTING_POINT);
        private Position headCurrentPosition = new Position(Advent9.STARTING_POINT, Advent9.STARTING_POINT);
        private StreamReader sr = null;
        public Solution(string path)
        {
            sr = new StreamReader(path);
            TailHasBeen = new HashSet<Position>();
            TailHasBeen.Add(tailCurrentPosition);
        }

        public int GetResult()
        {
            return TailHasBeen.Count;
        }
        
        public void RunSolution(int count)
        {
            if(count == 1)
                ProcessMovements();
            else
                ProcessConjunctMovement();
        }

        private void ProcessMovements()
        {
            string cmd = Read();

            while (cmd != "EOF")
            {
                char direction = cmd[0];
                int stepsRemaining = int.Parse(cmd.Substring(2));
                while (stepsRemaining > 0)
                {
                    headCurrentPosition.Move(direction);
                    if (!tailCurrentPosition.IsOneAwayOrCloser(headCurrentPosition))
                    {
                        tailCurrentPosition = WhereToMove(tailCurrentPosition, headCurrentPosition);
                        TailHasBeen.Add(tailCurrentPosition);
                    }
                    stepsRemaining--;
                }

                cmd = Read();
            }
        }

        private string Read()
        {
            if (!sr.EndOfStream)
                return sr.ReadLine();
            return "EOF";
        }


        public static Position WhereToMove(Position tail, Position head)
        {
            if (tail.GetX() == head.GetX()) // misma fila
                return new Position(tail.GetX(), tail.GetY() > head.GetY() ? tail.GetY() - 1 : tail.GetY() + 1);
            if (tail.GetY() == head.GetY()) // misma columna
                return new Position(tail.GetX() > head.GetX() ? tail.GetX() - 1 : tail.GetX() + 1, tail.GetY());

            if (head.GetX() > tail.GetX())
                return head.GetY() > tail.GetY()
                    ? new Position(tail.GetX() + 1, tail.GetY() + 1)
                    : new Position(tail.GetX() + 1, tail.GetY() - 1);
            else
                return new Position(tail.GetX() - 1, head.GetY() > tail.GetY() ? tail.GetY() + 1 : tail.GetY() - 1);

        }
        
        
        //part 2

        private List<Knot> knots = new List<Knot>(); // position 0 is head, position 9 is 9th knot

        private void ProcessConjunctMovement() //assuming you didnt run part 1 solution first
        {
            string cmd = Read();
            knots.Add(new Knot());

            //set follow ups

            for (int i = 1 ; i <=9; i++)
            {
                knots.Add(new Knot(knots[i-1]));
            }


            while (cmd != "EOF")
            {
                char direction = cmd[0];
                int stepsRemaining = int.Parse(cmd.Substring(2));
                while (stepsRemaining > 0)
                {
                    knots[0].GetPosition().Move(direction);
                    for (int i = 1; i <= 9; i++)
                    {
                        knots[i].Follow();
                    }

                    TailHasBeen.Add(knots[9].GetPosition());
                    stepsRemaining--;
                }

                cmd = Read();
            }
        }
        
        


    }


    class Knot
    {
        private Position position = new Position(Advent9.STARTING_POINT, Advent9.STARTING_POINT);
        private Knot knotToFollow;

        public Knot()
        {
            knotToFollow = null;
        }

        public Knot(Knot knotToFollow)
        {
            this.knotToFollow = knotToFollow;
        }

        public void Follow()
        {
            if (!position.IsOneAwayOrCloser(knotToFollow.position))
            {
                position = Solution.WhereToMove(position, knotToFollow.position);
            }
        }

        public void SetFollow(Knot follow)
        {
            knotToFollow = follow;
        }

        public Position GetPosition()
        {
            return position;
        }
    }

    class Position
    {
        private int x, y;

        public Position(int x, int y)
        {
            this.x = x;
            this.y = y;
        }

        public override int GetHashCode()
        {
            int ret = int.Parse(x.GetHashCode().ToString() + y.GetHashCode().ToString());
            return ret;
        }

        public override bool Equals(object? obj)
        {
            Position pos = (Position)obj;
            if(obj != null)
                return this.x == pos.x && this.y == pos.y;
            return false;
        }
        
        public void Move(char direction)
        {
            switch (direction)
            {
                case 'R':
                    this.x++;
                    break;
                case 'L' :
                    this.x--;
                    break;
                case 'U' :
                    this.y++;
                    break;
                case 'D':
                    this.y--;
                    break;
            }
        }


        public int GetX()
        {
            return x;
        }

        public int GetY()
        {
            return y;
        }

        public override string ToString()
        {
            return "[X = " + x + ", Y = " + y + "]";
        }

        public bool IsOneAwayOrCloser(Position otherPosition)
        {
            int xDif = Math.Abs(this.x - otherPosition.x);
            int yDif = Math.Abs(this.y - otherPosition.y);

            return xDif <= 1 && yDif <= 1;
        }
    }
}

