/*
*  Create a Task and attach continuations to it according to the following criteria:
   a.    Continuation task should be executed regardless of the result of the parent task.
   b.    Continuation task should be executed when the parent task finished without success.
   c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation
   d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled
   Demonstrate the work of the each case with console utility.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Threading.Tasks;

namespace MultiThreading.Task6.Continuation
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Create a Task and attach continuations to it according to the following criteria:");
            Console.WriteLine("a.    Continuation task should be executed regardless of the result of the parent task.");
            Console.WriteLine("b.    Continuation task should be executed when the parent task finished without success.");
            Console.WriteLine("c.    Continuation task should be executed when the parent task would be finished with fail and parent task thread should be reused for continuation.");
            Console.WriteLine("d.    Continuation task should be executed outside of the thread pool when the parent task would be cancelled.");
            Console.WriteLine("Demonstrate the work of the each case with console utility.");
            Console.WriteLine();

            CancellationTokenSource cts = new CancellationTokenSource();

            Task ParentTask = Task.Run(() =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Inside parent task");
                
            }, cts.Token);

            Task ContinueInAnyCaseTask = ParentTask.ContinueWith((x,y) =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Inside the task that will continue in any case.");
            }, new Person() { Id = 1 }) ;
            
            Task ContinueInNoSuccessCaseTask = ParentTask.ContinueWith(t =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Inside the task that will continue in case of no success.");
            }, TaskContinuationOptions.NotOnRanToCompletion);

            Task ContinueInFailCaseTask = ParentTask.ContinueWith(t =>
            {
                Thread.Sleep(2000);
                Console.WriteLine("Parent task got failed");
            }, TaskContinuationOptions.ExecuteSynchronously | TaskContinuationOptions.OnlyOnFaulted);

            Task ContinueOutsideThreadPool = ParentTask.ContinueWith((t) =>
            {
                Console.WriteLine("I'm outside thread pool thread.");
            }, TaskContinuationOptions.LongRunning | TaskContinuationOptions.OnlyOnCanceled);

            Console.ReadLine();
        }
    }
    class Person
    {
        public int Id { get; set; }
    }
}
