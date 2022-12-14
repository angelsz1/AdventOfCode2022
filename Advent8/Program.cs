namespace Advent8
{
    class Advent8
    {
        public static void Main(string[] args)
        {
            StreamReader sr = new StreamReader(Advent2.Advent2.Path("real_input_8.in"));
            Solution s = new Solution(sr);
            //Console.WriteLine("La solucion es : " + s.Solucion());
            Console.WriteLine("Solucion de la segunda parte = " + s.Solucion2());
        }
    }

    internal class Solution
    {

        private List<List<int>> matriz;
        private bool[,] visibleFromUp;
        private bool[,] visibleFromDown;
        private bool[,] visibleFromLeft;
        private bool[,] visibleFromRight;
        private StreamReader sr;

        public Solution(StreamReader sr)
        {
            this.sr = sr;
            CargarMatriz();
            sr.Close();
            CrearMatrizDeVisibilidad();
        }

        private void CrearMatrizDeVisibilidad() // im doing this to try to optimize the search for visibility
        {
            visibleFromDown = new bool[matriz.Count, matriz[0].Count()];
            visibleFromUp = new bool[matriz.Count, matriz[0].Count()];
            visibleFromLeft = new bool[matriz.Count, matriz[0].Count()];
            visibleFromRight = new bool[matriz.Count, matriz[0].Count()];
            
            for (int i = 0; i < matriz.Count; i++)
            {
                for (int j = 0; j < matriz[0].Count; j++)
                {
                    if (i == 0 || i == matriz.Count - 1)
                    {
                        visibleFromDown[i, j] = true;
                        visibleFromUp[i, j] = true;
                        visibleFromLeft[i, j] = true;
                        visibleFromRight[i, j] = true;
                    }

                    if (j == 0 || j == matriz[0].Count - 1)
                    {
                        visibleFromDown[i, j] = true;
                        visibleFromUp[i, j] = true;
                        visibleFromLeft[i, j] = true;
                        visibleFromRight[i, j] = true;
                    }

                }
            }

        }
        
        
        private void CargarMatriz()
        {
            matriz = new List<List<int>>();

            while (!sr.EndOfStream)
            {
                string line = sr.ReadLine();
                List<int> aux = new List<int>();
                foreach (char c in line)
                    aux.Add(int.Parse(c.ToString()));
                matriz.Add(aux);
            }
        }

        public int Solucion()
        {
            int cantFilas = matriz.Count;
            int cantColumnas = matriz[0].Count;

            for (int i = 1; i < cantFilas - 1; i++)
            {
                for (int j = 1; j < cantColumnas - 1; j++)
                {
                    visibleFromLeft[i,j] = findVisibilityL(matriz[i][j],i, j);
                    visibleFromRight[i,j] = findVisibilityR(matriz[i][j],i, j);
                    visibleFromUp[i,j] = findVisibilityU(matriz[i][j],i, j);
                    visibleFromDown[i,j] = findVisibilityD(matriz[i][j],i, j);
                }
            }

            int amountOfTrue = 0;
            
            for (int i = 0; i < cantFilas ; i++)
            {
                for (int j = 0; j < cantColumnas; j++)
                {
                    if (visibleFromLeft[i, j] || visibleFromRight[i, j]
                                              || visibleFromUp[i, j] || visibleFromDown[i, j])
                        amountOfTrue++;

                }
            }
            

            return amountOfTrue;
        }

        public int Solucion2()
        {
            int maxScenicScore = 0;

            for (int i = 0; i < matriz.Count; i++)
            {
                for (int j = 0; j < matriz[0].Count; j++)
                {
                    int l = countLeft(matriz[i][j], i, j) ;
                    int r = countRight(matriz[i][j], i, j);
                    int u = countUp(matriz[i][j], i, j);
                    int d = countDown(matriz[i][j], i, j);
                    
                    //Console.WriteLine("Pos(" + i + ", " + j + "), altura = " + matriz[i][j] + " l-r-u-d = " + l +" "+r+" "+u+" "+d);
                    
                    

                    int scenicScore = l * r * u * d;
                    if (scenicScore > maxScenicScore)
                        maxScenicScore = scenicScore;
                }
            }

            return maxScenicScore;
        }


        private int countLeft(int altura, int i, int j)
        {
            int count = 0;
            j--;
            while (j >= 0 && matriz[i][j] < altura)
            {
                count++;
                j--;
            }

            if (j < 0)
                return count;
            else
                return count + 1;
        }
        private int countRight(int altura, int i, int j)
        {
            int count = 0;
            j++;
            while (j < matriz[0].Count && matriz[i][j] < altura)
            {
                count++;
                j++;
            }

            if (j == matriz[0].Count)
                return count;
            else
                return count + 1;

        }
        private int countUp(int altura, int i, int j)
        {
            int count = 0;
            i--;
            while (i >= 0 && matriz[i][j] < altura)
            {
                count++;
                i--;
            }

            if (i < 0)
                return count;
            else
                return count + 1;
        }
        private int countDown(int altura, int i, int j)
        {
            int count = 0;
            i++;
            while (i < matriz.Count && matriz[i][j] < altura)
            {
                count++;
                i++;
            }

            if (i == matriz.Count)
                return count;
            else
                return count + 1;
        }


        private bool findVisibilityL(int altura, int i, int j)
        {
            if (altura > matriz[i][j - 1])
                if (visibleFromLeft[i,j - 1])
                    return true;
                else
                    return findVisibilityL(altura, i, j - 1);
            
            return false;
        }
        
        private bool findVisibilityR(int altura, int i, int j)
        {
            if (altura > matriz[i][j + 1])
                if (visibleFromRight[i,j + 1])
                    return true;
                else
                    return findVisibilityR(altura, i, j + 1);
            return false;
        }
        
        private bool findVisibilityD(int altura, int i, int j)
        {
            if (altura > matriz[i+1][j])
                if (visibleFromDown[i+1,j])
                    return true;
                else
                    return findVisibilityD(altura, i+1, j);
            return false;
        }
        
        private bool findVisibilityU(int altura, int i, int j)
        {
            if (altura > matriz[i-1][j])
                if (visibleFromUp[i-1,j])
                    return true;
                else
                    return findVisibilityU(altura, i-1, j);
            return false;
        }
        
        public List<List<int>> GetList()
        {
            return matriz;
        }
        
    }
}