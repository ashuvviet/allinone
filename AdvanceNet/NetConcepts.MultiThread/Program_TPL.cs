//using NetConcepts.Model.Utilities;
//using System;
//using System.Diagnostics;
//using System.Linq;
//using System.Threading;
//using System.Threading.Tasks;

//namespace NetConcepts.MultiThread
//{
//    class Program
//    {
//        static void Main(string[] args)
//        {
//            Stopwatch stopwatch = new Stopwatch();
//            stopwatch.Start();

//            //Display1();
//            //Display2();

//            // TPL - Task Parallel Library. 
//            Parallel.Invoke(() => Display1(), () => Display2());

//            // TPL - For each
//            //var numbers = Enumerable.Range(1, 10000);
//            //foreach (var number in numbers)
//            //{
//            //    if (Helper.IsPrime(number))
//            //    {
//            //        Console.WriteLine($"Number {number} is Prime :" + true);
//            //    }
//            //    else
//            //    {
//            //        Console.WriteLine($"Number {number} is Prime :" + false);
//            //    }
//            //}

//            //Parallel.ForEach(numbers, (number) =>
//            //{
//            //    Console.WriteLine($"Number {number} is Prime :" + Helper.IsPrime(number));
//            //});

//            // Total time taken running as Sequentail manner  ms
//            //Console.ReadKey();

//            stopwatch.Stop();
//            Console.WriteLine($"total time taken in Ms{stopwatch.ElapsedMilliseconds}");
//            Console.ReadKey();
//        }

//        private static void Display1()
//        {
//            for (int i = 0; i < 100; i++)
//            {
//                Thread.Sleep(1);
//                Console.WriteLine("First Method");
//            }
//        }

//        private static void Display2()
//        {
//            for (int i = 0; i < 100; i++)
//            {
//                Thread.Sleep(1);
//                Console.WriteLine("Second Method");
//            }
//        }
//    }
//}
