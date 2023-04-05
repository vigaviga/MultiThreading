/*
 * 2.	Write a program, which creates a chain of four Tasks.
 * First Task – creates an array of 10 random integer.
 * Second Task – multiplies this array with another random integer.
 * Third Task – sorts this array by ascending.
 * Fourth Task – calculates the average value. All this tasks should print the values to console.
 */
using System;
using System.Linq;
using System.Threading.Tasks;

namespace MultiThreading.Task2.Chaining
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(".Net Mentoring Program. MultiThreading V1 ");
            Console.WriteLine("2.	Write a program, which creates a chain of four Tasks.");
            Console.WriteLine("First Task – creates an array of 10 random integer.");
            Console.WriteLine("Second Task – multiplies this array with another random integer.");
            Console.WriteLine("Third Task – sorts this array by ascending.");
            Console.WriteLine("Fourth Task – calculates the average value. All this tasks should print the values to console");
            Console.WriteLine();

            Task<int[]> t1 = new Task<int[]>(() =>
            {
                int[] arr = new int[10];
                for(int i = 0; i < 10; i++)
                {
                    arr[i] = new Random().Next(1, 100);
                    Console.Write(arr[i] + " ");
                }
                
                return arr;
            });

            Task<int[]> t2 = t1.ContinueWith(t =>
            {
                int[] arr = t.Result;
                int multiplier = new Random().Next(1, 10);
                Console.WriteLine("");
                Console.WriteLine("Multiplier is " + multiplier);
                Console.WriteLine("Multiplied values are.");
                for (int i = 0; i < 10; i++)
                {
                    arr[i] *= multiplier;
                    Console.Write(arr[i] + " ");
                }
                
                return arr;

            });

            Task<int[]> t3 = t2.ContinueWith(t =>
            {
                var arr = t.Result;
                for(int i = 0; i < 9; i++)
                {
                    for(int j = i+1; j < 10;  j++)
                    {
                        if (arr[i] > arr[j])
                        {
                            int temp = arr[i];
                            arr[i] = arr[j];
                            arr[j] = temp;
                        }
                    }
                }

                Console.WriteLine("");
                Console.WriteLine("Sorted array is.");

                foreach (int num in arr)
                {
                    Console.Write(num + " ");
                }
                return arr;
            });

            Task t4 = t3.ContinueWith(t =>
            {
                double average = t.Result.Average();
                Console.WriteLine("");
                Console.WriteLine("The average is.");
                Console.WriteLine(average);
            });

            t1.Start();
            Task.WaitAll(t4);

            Console.ReadLine();
        }
    }
}
