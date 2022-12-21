using System.Numerics;

namespace Advent12
{
    class Advent12
    {
        public static void Main(string[] args)
        {
            Solution s = new Solution("real_input_12.in");
            s.Show();
        }
    }

    class Solution
    {
        private StreamReader sr;
        private Graph grafo;

        public Solution(string path)
        {
            sr = new StreamReader(Advent2.Advent2.Path(path));
            grafo = new Graph(sr.ReadToEnd());
        }

        public void Show()
        {
            grafo.runSolution2();
        }
    }

    class Graph
    {
        private int[,] matAdj;
        private Position zPos = null;
        private Position sPos = null;
        private List<Position> aPositions = new List<Position>();
        private char[,] matrizDeAlturas;
        private int cantNodos;
        private int V;
        private int horizontalAmount;

        public Graph(string allTheFile)
        {
            string[] lines = allTheFile.Split('\n');
            matrizDeAlturas = new char[lines.Length, lines[0].Length];
            cantNodos = matrizDeAlturas.Length;
            horizontalAmount = lines[0].Length;
            V = cantNodos;

            int row = 0;
            foreach (string line in lines)
            {
                for (int i = 0; i < line.Length; i++)
                {
                    matrizDeAlturas[row, i] = line[i];
                    if (line[i] == 'E')
                    {
                        zPos = new Position(row, i);
                        matrizDeAlturas[row, i] = 'z';
                    }

                    if (line[i] == 'S')
                    {
                        sPos = new Position(row, i);
                        matrizDeAlturas[row, i] = 'a';
                        aPositions.Add(sPos);
                    }
                    
                    if(line[i] == 'a')
                        aPositions.Add(new Position(row, i));
                }

                row++;
            }

            SetAdjMatrix();

        }

        public void runSolution2()
        {
            int min = Int32.MaxValue;
            
            foreach (Position p in aPositions)
            {
                int curValue = dijkstra(matAdj, p.GetX() * horizontalAmount + p.GetY());
                if (curValue < min)
                    min = curValue;
            }
            
            Console.WriteLine("RESPUESTA :" + min);
        }

        public int[,] GetMatAdj()
        {
            return matAdj;
        }

        public Position GetSPos()
        {
            return sPos;
        }

        private void SetAdjMatrix()
        {
            matAdj = new int[cantNodos,cantNodos];

            for (int i = 0; i < cantNodos; i++)
            {
                Set(i,"up");
                Set(i, "down");
                Set(i, "left");
                Set(i, "right");
            }
        }

        private void Set(int node, string direction)
        {
            int i = node / horizontalAmount;
            int j = node % horizontalAmount;
            char curNodeChar = matrizDeAlturas[i, j];
            switch (direction)
            {
                case "up":
                    i--;
                    if(i < 0)
                        return;
                    break;
                case "down":
                    i++;
                    if(i >= matrizDeAlturas.Length/horizontalAmount)
                        return;
                    break;
                case "left":
                    j--;
                    if(j < 0)
                        return;
                    break;
                case "right":
                    j++;
                    if(j >= horizontalAmount)
                        return;
                    break;
            }
            
            
            char otherPosChar = matrizDeAlturas[i, j];
            if (Math.Abs(otherPosChar - curNodeChar) <= 1 || otherPosChar < curNodeChar)
                matAdj[node, i * horizontalAmount + j] = 1;
                
        }

        public int minDistance(int[] dist,
                    bool[] sptSet)
        {
            // Initialize min value
            int min = int.MaxValue, min_index = -1;
      
            for (int v = 0; v < V; v++)
                if (sptSet[v] == false && dist[v] <= min) {
                    min = dist[v];
                    min_index = v;
                }
      
            return min_index;
        }



        // A utility function to print
    // the constructed distance array
    
        public int Solution(int[] dist, int n)
        {
            return dist[zPos.GetX()*horizontalAmount + zPos.GetY()];
        }
      
        // Function that implements Dijkstra's
        // single source shortest path algorithm
        // for a graph represented using adjacency
        // matrix representation
        public int dijkstra(int[, ] graph, int src)
        {
            int[] dist = new int[V]; // The output array. dist[i]
            // will hold the shortest
            // distance from src to i
      
            // sptSet[i] will true if vertex
            // i is included in shortest path
            // tree or shortest distance from
            // src to i is finalized
            bool[] sptSet = new bool[V];
      
            // Initialize all distances as
            // INFINITE and stpSet[] as false
            for (int i = 0; i < V; i++) {
                dist[i] = int.MaxValue;
                sptSet[i] = false;
            }
      
            // Distance of source vertex
            // from itself is always 0
            dist[src] = 0;
      
        // Find shortest path for all vertices
            for (int count = 0; count < V - 1; count++) {
                // Pick the minimum distance vertex
                // from the set of vertices not yet
                // processed. u is always equal to
                // src in first iteration.
                int u = minDistance(dist, sptSet);
      
                // Mark the picked vertex as processed
                sptSet[u] = true;
      
                // Update dist value of the adjacent
                // vertices of the picked vertex.
                for (int v = 0; v < V; v++)
      
                    // Update dist[v] only if is not in
                    // sptSet, there is an edge from u
                    // to v, and total weight of path
                    // from src to v through u is smaller
                    // than current value of dist[v]
                    if (!sptSet[v] && graph[u, v] != 0 && 
                         dist[u] != int.MaxValue && dist[u] + graph[u, v] < dist[v])
                        dist[v] = dist[u] + graph[u, v];
            }
            
            return Solution(dist, V);
        }
        
        

        public override string ToString()
        {
            string ret = "";

            for (int i = 0; i < cantNodos; i++)
            {
                for (int j = 0; j < cantNodos; j++)
                    ret += matAdj[i, j] + " ";
                ret += "\n";
            }

            return ret;
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


        public int GetX()
        {
            return x;
        }
        
        public int GetY()
        {
            return y;
        }
    }


}

