using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;

namespace Conways_Game_of_Life
{
    class Program
    {
        static Random rand = new Random();
        static int lifeSize;
        static int generationCount;
        static int displayType;
        static bool[,] currentGeneration;
        static bool[,] nextGeneration;
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r\nGame of life");
            Console.WriteLine("\r\nPlease select game options(recommended 20-50):");
            selectArrSize:
            Console.WriteLine("1)Input please field size:");           
            Int32.TryParse(Console.ReadLine(),out lifeSize);
            if (lifeSize == 0)
                goto selectArrSize;
            selectGenerationCount:
            Console.WriteLine("2)Input please count of generation(10,100,1000...):");
            Int32.TryParse(Console.ReadLine(), out generationCount);
            if (generationCount == 0)
                goto selectGenerationCount;
            setDisplayType:
            Console.WriteLine("\r\nSelect display type:");
            Console.WriteLine("1 - list");
            Console.WriteLine("2 - Updating data");
            Int32.TryParse(Console.ReadLine(), out displayType);
            if (displayType > 2 || displayType < 1)
                goto setDisplayType;
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r\nStart random data:");
            Init();
            Display(currentGeneration);
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\r\nPress Enter to start...\r\n");
            Console.ReadLine();
            for (int i=0;i< generationCount; i++)
            {
                Execute();
            }
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine("\n\n\r\nThat is your result");
            Console.ReadLine();
        }

       static void Execute()
        {
            ApplyLifeRules();
            Display(currentGeneration);
        }

        //проверка маски
        static int countNeighbors(int x,int y)
        {
                int count = 0;
                for (int dx = -1; dx < 2; dx++)
                {
                    for (int dy = -1; dy < 2; dy++)
                    {
                        int nX = x + dx;
                        int nY = y + dy;
                        nX = (nX < 0) ? lifeSize - 1 : nX;
                        nY = (nY < 0) ? lifeSize - 1 : nY;
                        nX = (nX > lifeSize - 1) ? 0 : nX;
                        nY = (nY > lifeSize - 1) ? 0 : nY;
                        count += (currentGeneration[nX,nY]) ? 1 : 0;
                    }
                }   
            if (currentGeneration[x, y])
                count--;
            return count;
        }

        // новое поколение
        static void ApplyLifeRules()
        {
            for(int i=0;i<lifeSize;i++)
                for(int j=0;j<lifeSize;j++)
                {
                    int count = countNeighbors(i, j);
                    nextGeneration[i, j] = currentGeneration[i, j];
                    // проверка на появление в данной клетки новой живой клетки, только если 3 живых соседа
                    nextGeneration[i, j] = (count == 3) ? true : nextGeneration[i, j];
                    // если количество соседей у клетки меньше двух или больше трёх, тогда клетка умирает
                    nextGeneration[i, j] = ((count < 2) || (count > 3)) ? false : nextGeneration[i, j];
                }
                Array.Copy(nextGeneration, 0, currentGeneration, 0, lifeSize* lifeSize);
        }

        static void Display(bool[,] data)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.OutputEncoding = System.Text.Encoding.Unicode;
            int x = data.GetUpperBound(0);
            int y = data.GetUpperBound(1);            
            if(displayType==2)
                Console.Clear();
            if(displayType==1)
                Console.WriteLine(new string('=', lifeSize+1));
            Console.Write(new string('-', lifeSize+1));
            Console.Write(Environment.NewLine);
            for (int i=0;i < x; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write('|');
                for (int j = 0; j < y; j++)
                {
                    if (data[i, j] == true)
                    {
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write((char)9632);
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Black;
                        Console.Write((char)9632);
                    }

                }
                Console.ForegroundColor = ConsoleColor.White;
                Console.Write('|');
                Console.WriteLine();
              
            }
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(new string('-',lifeSize+1));   
            Thread.Sleep(100);
        }

        static void Init()
        {
            currentGeneration = new bool[lifeSize, lifeSize];
            nextGeneration = new bool[lifeSize, lifeSize];
            for (int i=0;i< lifeSize;i++)
                for(int j=0;j<lifeSize;j++)
                {
                    currentGeneration[i, j] = (rand.Next(0,2)==0)?false:true;
                }
        }

    }
}
