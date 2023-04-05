/*
 * 4.	Write a program which recursively creates 10 threads.
 * Each thread should be with the same body and receive a state with integer number, decrement it,
 * print and pass as a state into the newly created thread.
 * Use Thread class for this task and Join for waiting threads.
 * 
 * Implement all of the following options:
 * - a) Use Thread class for this task and Join for waiting threads.
 * - b) ThreadPool class for this task and Semaphore for waiting threads.
 */

using System;
using System.Globalization;
using System.Threading;

namespace MultiThreading.Task4.Threads.Join
{
    class Program
    {
        private static Semaphore semaphore;
        static void Main(string[] args)
        {
            Console.WriteLine("4.	Write a program which recursively creates 10 threads.");
            Console.WriteLine("Each thread should be with the same body and receive a state with integer number, decrement it, print and pass as a state into the newly created thread.");
            Console.WriteLine("Implement all of the following options:");
            Console.WriteLine();
            Console.WriteLine("- a) Use Thread class for this task and Join for waiting threads.");
            Console.WriteLine("- b) ThreadPool class for this task and Semaphore for waiting threads.");

            Console.WriteLine();
            semaphore = new Semaphore(1, 10);
            CreateThread(10);
            Console.WriteLine("Now with semaphore");
            CreateThreadWithSemaphore(10);
            Console.ReadLine();
        }

        static void CreateThread(int decrementer)
        {
            if (decrementer > 0)
            {
                decrementer--;

                Console.WriteLine("Decremented value is " + decrementer);

                Thread thread = new Thread(() => CreateThread(decrementer));
                thread.Start();
                thread.Join();
            }
        }

        static void CreateThreadWithSemaphore(int decrementer)
        {
            if (decrementer > 0)
            {
                semaphore.WaitOne();
                decrementer--;
                Console.WriteLine("Decremented value is " + decrementer);
                semaphore.Release();

                ThreadPool.QueueUserWorkItem((object state) => CreateThreadWithSemaphore(decrementer));
            }
        }
    }
}
