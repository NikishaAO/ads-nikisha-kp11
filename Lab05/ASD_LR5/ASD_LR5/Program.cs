using System;
using static System.Console;
using System.Collections.Generic;

namespace ASD_LR5
{
    class Program
    {
        static void Main(string[] args)
        {
            int choise, m = 4, n = 4, k = 2 ;
            int[,] Matrix = new int[4, 4] { { 1, 12, 15,11 }, { 4, 2, 9, 16}, { 10, 3, 8,6 },{ 5, 7, 14,13} };


            WriteLine("1 - згенерувати масив, iнше - контрольний приклад");
            choise = Convert.ToInt32(ReadLine());
            if(choise == 1)
            {
                Write("m = ");
                 m = Convert.ToInt32(ReadLine());
                Write("n = ");
                 n = Convert.ToInt32(ReadLine());

                Matrix = GenArr(m, n);
                Write("k = ");
                 k = Convert.ToInt32(ReadLine());
            }
         



            WriteArr(Matrix,k);

            int[] List = To1D(Matrix);            

         

            List<int> Al = new List<int>();
            List<int> Else = new List<int>();
            List<int> Index = new List<int>();

            for(int i = 0; i < List.Length; i++)
            {
                if (List[i] % k == 0)
                {
                    Al.Add(List[i]);
                    Index.Add(i);
                }
                else
                    Else.Add(List[i]);                  
            }

            if (Al.Count < m)
            {
                Al = InsertionDec(Al);
                Else = SortInc(Else, 0, Else.Count - 1);

            }
            else if (Else.Count < m)
            {
                Al = SortDec(Al, 0, Al.Count - 1);
                Else = InsertionInc(Else);
            }
            else
            {
                Al = SortDec(Al, 0, Al.Count - 1);
                Else = SortInc(Else, 0, Else.Count - 1);
            }

            int a = 0;
            int b = 0;


            for (int x = 0; x < List.Length; x++)
            {
                if (Index.Contains(x))
                {
                    List[x] = Al[a];
                    a++;
                }
                else
                {
                    List[x] = Else[b];
                    b++;
                }
            }

            a = 0;

            for (int j = 0; j < n; j++)
            for(int i = 0; i < m; i++)
            {
                    Matrix[i,j]= List[a];
                    a++;
            }

            WriteLine();
            WriteArr(Matrix, k);
        }

        static int[,] GenArr(int m, int n)
        {
            int[,] Arr = new int[m, n];
            List<int> Con = new List<int>();
            int num;
            Random rnd = new Random();

            for (int j = 0; j < n; j++)
                for (int i = 0; i < m; i++)
                {
                    do
                    {
                        num = rnd.Next(100);
                    } while (Con.Contains(num));
                    Arr[i, j] = num;
                    Con.Add(num);      
                }

            return Arr;
        }

        static void WriteArr(int[,] Arr, int k)
        {
            for (int j = 0; j < Arr.GetLength(1); j++)
            {
                WriteLine();
                for (int i = 0; i < Arr.GetLength(0); i++)
                {
                    if (Arr[i, j] % k == 0)
                    {
                        ForegroundColor = ConsoleColor.Yellow;
                        Write(Arr[i, j] + " ");
                        ResetColor();
                    }
                    else
                    {
                        ForegroundColor = ConsoleColor.Blue;
                        Write(Arr[i, j] + " ");
                        ResetColor();
                    }
                }
            }
        }


        static int[] To1D(int[,] Arr)
        {
            int[] NewArr = new int[Arr.GetLength(0) * Arr.GetLength(1)];
            int p = 0;

            for (int j = 0; j < Arr.GetLength(1); j++)
                for (int i = 0; i < Arr.GetLength(0); i++)
                {
                    NewArr[p] = Arr[i, j];
                    p++;
                }

            return NewArr;
        }

        static List<int> SortInc(List<int> Arr, int low, int high)
        {

            int p;
            if (low < high)
            {
                p = LomutoPartInc(Arr, low, high);
                Arr = SortInc(Arr, low, p-1);
                Arr = SortInc(Arr, p + 1, high);
            }


            return Arr;
            
        }

        static List<int> SortDec(List<int> Arr, int low, int high)
        {

            int p;
            if (low < high)
            {
                p = LomutoPartDec(Arr, low, high);
                Arr = SortDec(Arr, low, p - 1);
                Arr = SortDec(Arr, p + 1, high);
            }


            return Arr;

        }
        static int LomutoPartInc(List<int> Arr, int low, int high)
        {
           int  pivot = Arr[high];
            int i = low-1;
            int buf;
            for( int j = low; j < high; j++)
            {
                if(Arr[j] < pivot)
                {
                    i++;

                    buf = Arr[i];
                    Arr[i] = Arr[j];
                    Arr[j] = buf;

                }
            }
            buf = Arr[high];
            Arr[high] = Arr[i+1];
            Arr[i+1] = buf;

            return i+1;
        }

        static int LomutoPartDec(List<int> Arr, int low, int high)
        {
            int pivot = Arr[high];
            int i = low - 1;
            int buf;
            for (int j = low; j < high; j++)
            {
                if (Arr[j] > pivot)
                {
                    i++;

                    buf = Arr[i];
                    Arr[i] = Arr[j];
                    Arr[j] = buf;

                }
            }
            buf = Arr[high];
            Arr[high] = Arr[i + 1];
            Arr[i + 1] = buf;

            return i + 1;
        }

        static List<int> InsertionInc(List<int> List)
        {
            for(int i = 0; i < List.Count; i++)
            {
                int j = i - 1;
                int buf = List[i];
                while(j >= 0 && List[j] > buf )
                {
                    List[j + 1] = List[j];
                    j--;
                }
                List[j + 1] = buf;
            }

            return List;
        }

        static List<int> InsertionDec(List<int> List)
        {
            for (int i = 0; i < List.Count; i++)
            {
                int j = i - 1;
                int buf = List[i];
                while (j >= 0 && List[j] < buf )
                {
                    List[j + 1] = List[j];
                    j--;
                }
                List[j + 1] = buf;
            }

            return List;
        }
    }




}
