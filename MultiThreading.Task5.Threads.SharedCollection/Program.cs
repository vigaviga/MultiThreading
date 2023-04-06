/*
 * 5. Write a program which creates two threads and a shared collection:
 * the first one should add 10 elements into the collection and the second should print all elements
 * in the collection after each adding.
 * Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.
 */
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task5.Threads.SharedCollection
{
    class Program
    {
        private static readonly object SyncObject = new object();
        private static List<int> Collection = new List<int>();
        static void Main(string[] args)
        {
            Console.WriteLine("5. Write a program which creates two threads and a shared collection:");
            Console.WriteLine("the first one should add 10 elements into the collection and the second should print all elements in the collection after each adding.");
            Console.WriteLine("Use Thread, ThreadPool or Task classes for thread creation and any kind of synchronization constructions.");
            Console.WriteLine();

            Thread t1 = new Thread(AddItem);
            Thread t2 = new Thread(PrintItems);

            t1.Start();
            t2.Start();
            t1.Join();
            t2.Join();

            Console.ReadLine();
        }

        private static void AddItem()
        {
            lock(SyncObject)
            {
                for (int i = 0; i < 10; i++)
                {
                    Collection.Add(i);
                    Monitor.Pulse(SyncObject);
                    Monitor.Wait(SyncObject);
                }
            }
        }

        private static void PrintItems()
        {
            lock(SyncObject)
            {
                for (int i = 0; i < 10; i++)
                {
                    Collection.ForEach(num => Console.WriteLine(num));
                    Console.WriteLine("------------");
                    Monitor.Pulse(SyncObject);
                    Monitor.Wait(SyncObject);
                }
            }
        }
    }
}
