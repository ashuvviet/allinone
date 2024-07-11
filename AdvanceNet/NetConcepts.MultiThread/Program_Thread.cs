//using System;
//using System.Diagnostics;
//using System.Threading;

//namespace NetConcepts.MultiThread
//{
//    public class A
//    {
//        private object lockObj = new object();

//        public void Display1()
//        {
//            // Step1
//            Console.WriteLine("Display1 -> Step1");

//            // Step 2
//            Console.WriteLine("Display1 -> Step2");

//            //// Step3 -> Lock
//            //lock (lockObj)
//            //{
//            //    for (int i = 0; i < 10; i++)
//            //    {
//            //        Thread.Sleep(1);
//            //        Console.WriteLine("First Method");
//            //    }
//            //}
//            //


//            // Montior
//            Monitor.Enter(lockObj);
//            for (int i = 0; i < 10; i++)
//            {
//                Thread.Sleep(1);
//                Console.WriteLine("First Method");
//            }
//            Monitor.Exit(lockObj);
//        }

//        public void Display2()
//        {
//            // Step1
//            Console.WriteLine("Display2 -> Step1");

//            // Step 2
//            Console.WriteLine("Display2 -> Step2");

//            //// Step3 -> Lock
//            //lock (lockObj)
//            //{
//            //    for (int i = 0; i < 10; i++)
//            //    {
//            //        Thread.Sleep(1);
//            //        Console.WriteLine("Second Method");
//            //    }
//            //}

//            // Montior
//            try
//            {
//                Monitor.Enter(lockObj);
//                for (int i = 0; i < 10; i++)
//                {
//                    Thread.Sleep(1);
//                    Console.WriteLine("Second Method");
//                }
//            }
//            finally
//            {
//                Monitor.Exit(lockObj);
//            }

//        }
//    }

//    class Program
//    {
//        static void Main_NormalCase(string[] args)
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();
//            var aObj = new A();

//            aObj.Display1();
//            aObj.Display2();

//            stopwatch.Stop();

//            Console.WriteLine($"total time taken in Ms{stopwatch.ElapsedMilliseconds}");

//            // Total time taken running as Sequentail manner 35452 ms
//            Console.ReadKey();
//        }

//        static void Main_Thread_Concepts(string[] args)
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();
//            // Thread 

//            var aObj = new A();

//            Thread t1 = new Thread(aObj.Display1);
//            Thread t2 = new Thread(aObj.Display2);

//            t1.Start();
//            t2.Start();

//            // Total time taken running as Sequentail manner 17 ms
//            Console.ReadKey();

//            stopwatch.Stop();
//            Console.WriteLine($"total time taken in Ms{stopwatch.ElapsedMilliseconds}");
//            Console.ReadKey();
//        }

//        static void Main(string[] args)
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();
//            Thread

//           var aObj = new A();

//            Thread t1 = new Thread(aObj.Display1);
//            t1.Name = "First";
//            Thread t2 = new Thread(aObj.Display2);
//            t1.Name = "Second";

//            t1.Start();
//            t2.Start();

//            Total time taken running as Sequentail manner ms
//            Console.ReadKey();

//            stopwatch.Stop();
//            Console.WriteLine($"total time taken in Ms{stopwatch.ElapsedMilliseconds}");
//            Console.ReadKey();
//        }
//    }
//}
