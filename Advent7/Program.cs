
namespace Advent7
{
    class Advent7
    {
        class Directory
        {
            private List<Directory> SubDirectories = new List<Directory>();
            private readonly string _path;
            private int _size = 0;


            public Directory(string path)
            {
                this._path = path;
            }
            public Directory()
            {
                _path = "";
            }

            public string GetPath()
            {
                return this._path;
            }

            public int GetSize()
            {
                int realSize = this._size;

                foreach (Directory d in SubDirectories)
                {
                    realSize += d.GetSize();
                }

                return realSize;
            }

            public void AddToSize(int value)
            {
                this._size += value;
            }

            public void AddSubdirectory(Directory d)
            {
                SubDirectories.Add(d);
            }

            public string GetParentPath()
            {
                string result = "";
                string[] names = this._path.Split('/');
                for(int i = 0; i < names.Length -2 ; i++)
                {
                    result += names[i] + "/";
                }

                return result;
            }

        }

        static readonly int TOTAL_DISK_SPACE = 70_000_000;
        static readonly int REQUIRED_FREE_SPACE = 30_000_000;

        static List<Directory> _directories = new List<Directory>();

        public static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Advent2.Advent2.Path("real_input_7.in"));
            FindSizes(sr);
            int sum = SumOfAllLessThan100K();
            Console.WriteLine(sum);
            int sizeOfDeletedDir = FreeUpSpace();
            Console.WriteLine(sizeOfDeletedDir);
        }


        public static int FreeUpSpace()
        {
            int currentFreeSpace = TOTAL_DISK_SPACE - MaxSizeDir();

            int sizeOfSmallestDirThatSatisfies = int.MaxValue;

            foreach (Directory d in _directories)
            {
                if(currentFreeSpace + d.GetSize() >= REQUIRED_FREE_SPACE && d.GetSize() < sizeOfSmallestDirThatSatisfies )
                    sizeOfSmallestDirThatSatisfies = d.GetSize();
            }

            return sizeOfSmallestDirThatSatisfies;
        }

        public static int MaxSizeDir()
        {
            int maxSize = _directories[0].GetSize();

            for(int i = 1; i < _directories.Count; i++)
            {
                if(_directories[i].GetSize() > maxSize)
                    maxSize = _directories[i].GetSize();
            }

            return maxSize;

        }

        public static void FindSizes(StreamReader streamReader)
        {
            Directory pwd = null;
            string cmd = ReadCommand(streamReader);
            while(cmd != "E")
            {
                switch (cmd[0])
                {
                    case 'c':
                        if(cmd[3] != '.'){
                            string path = cmd.Substring(3);
                            Directory dirToAdd;
                            if(pwd != null){
                                dirToAdd = new Directory(pwd.GetPath() + path + "/");
                                pwd.AddSubdirectory(dirToAdd);
                                pwd = dirToAdd;
                            }
                            else
                                pwd = new Directory(path);
                            _directories.Add(pwd);
                        }
                        else{
                            int index = WhereIsIt(pwd.GetParentPath());
                            pwd = _directories.ElementAt(index);
                        }
                        break;
                    case 'l':
                        break;

                    case 'f':
                        string[] num = cmd.Split(':');
                        pwd.AddToSize(int.Parse(num[1]));
                        break;

                }

                cmd = ReadCommand(streamReader);
            }

        }

        public static int WhereIsIt(string path)
        {
            int ret = -1;
            int index = 0;
            foreach(Directory d in _directories)
            {
                if(d.GetPath() == path)
                    return index;
                index++;
            }

            return ret;
        }


        public static int SumOfAllLessThan100K()
        {
            int sum = 0;

            foreach(Directory d in _directories)
            {
                if(d.GetSize() <= 100_000)
                    sum += d.GetSize();
            }

            return sum;
        }

        /**
         *  Returns the command,
         *  The size of the file,
         *  E in case the file is over,
         *  Or it calls itself again in case of a dir
         */
        public static string ReadCommand(StreamReader streamReader)
        {
            string cmd = "";
            if(!streamReader.EndOfStream)
                cmd = streamReader.ReadLine();
            else
                return "E";
            if(cmd[0] == '$')
            {
                return cmd.Substring(2);
            }
            else if(cmd[0] == 'd')
            {
                return ReadCommand(streamReader);
            }
            else
            {
                string[] things = cmd.Split(' ');
                return "file:" + things[0];
            }
        }
    }
}

