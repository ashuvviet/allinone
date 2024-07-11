using Core.Utilities;
using NetConcepts.Model.Models;
using NetConcepts.Model.Utilities;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace NetConcepts.MultiThread
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Stopwatch stopwatch = new Stopwatch();
            stopwatch.Start();

            //var result = await ServiceHelper.ClientApi<bool>("https://google.com", HttpMethod.Get, null);
            //if (result)
            //{
            //    await Display1();
            //}

            // 1. Call Company Helper. CreateCompany in async way.
            var result  = await CompanyHelper.CreateCompany("Test", 10);

            // 2. Call Display1() in sync way inside this main.


            // yield keyword ?
            //var list2 = GetEmployeeNames(result);
            //foreach (var name in list2)
            //{
            //    Console.WriteLine(name);
            //}

            // this keyword :  get me employee whose name is Emp 1
            //var emp = result.Employees.FirstOrDefault(s => s.Name == "Emp 1");
            //var emp = result["Emp 1", "11"];


            stopwatch.Stop();
            Console.WriteLine($"total time taken in Ms{stopwatch.ElapsedMilliseconds}");
            Console.ReadKey();
        }

        private static IEnumerable<string> GetEmployeeNames(Company c)
        {
            foreach (var item in c.Employees)
            {
                yield return item.Name;
            }
        }      

        //private static IEnumerable<string> GetEmployeeNames(Company c)
        //{
        //    var list = new List<string>();
        //    foreach (var item in c.Employees)
        //    {
        //        list.Add(item.Name);
        //    }

        //    return list;
        //}

        private static async Task Display1()
        {
            await Display2();
            for (int i = 0; i < 100; i++)
            {               
                Console.WriteLine("First Method");
            }
        }

        private static async Task Display2()
        {
            for (int i = 0; i < 100; i++)
            {
                await Task.Delay(1);
                Console.WriteLine("Second Method");
            }
        }
    }
}
