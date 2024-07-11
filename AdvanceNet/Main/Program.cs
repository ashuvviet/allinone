using NetConcepts.Model.Models;
using NetConcepts.Model.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace Main
{
    /// <summary>
    /// C# 9.0 - Implementation at interface level.
    /// </summary>
    public interface IMath
    {
        int Add(int a, int b) => a + b;
        int Sub(int a, int b) => a - b;
    }

    public class MathImp : IMath
    {
        public int Add(int a, int b)
        {
            return a + b + 1;
        }

        public int Sub(int a, int b)
        {
            return a - b;
        }
    }

    //public class Training
    //{
    //    public string Name { get; set; }
    //    public Training(string name)
    //    {
    //        Name = name;
    //    }
    //}

    ///// <summary>
    ///// Immutiable
    ///// </summary>
    //public record Training
    //{
    //    public string Name { get; set; }
    //    public Training(string name)
    //    {
    //        Name = name;
    //    }
    //}

    public record Training(string Name);

    class Program
    {
        static async Task Main(string[] args)
        {
            //var result = await CompanyHelper.CreateCompany("Test", 10);

            //// Pattern Matching / switch cases.
            //foreach (var item in result.Employees)
            //{
            //    Print(item);
            //}

            //IMath mathObj = new MathImp();
            //var result =  mathObj.Add(2, 2);
            //Console.WriteLine(result);

            var t = new Training("Advance Net");
            Console.WriteLine(t.Name);

            // nullable enabling
            Console.ReadKey();
        }

        private static void Print(Employee e)
        {
            //if (e is FullTimeEmp fullTimeEmp)
            //{
            //    if(fullTimeEmp.EmpCode > 0 && fullTimeEmp.EmpCode < 6)
            //    {
            //        Console.WriteLine("Full Time Emp of emp from 1 to 6");
            //    }
            //    Console.WriteLine("Full Time Emp");
            //}

            //if (e is PartTimeEmp)
            //{
            //    Console.WriteLine("Part Time Emp");
            //}

            // Console.WriteLine("Part Time Emp of emp from 1 to 6");
            // Console.WriteLine("Full Time Emp of emp from 1 to 6");
            // Console.WriteLine("Part Time Emp of emp from 7 to 10");
            // Console.WriteLine("Full Time Emp of emp from 7 to 10");
            switch (e)
            {
                case PartTimeEmp partTimeEmp when (partTimeEmp.EmpCode > 0 && partTimeEmp.EmpCode < 6) :
                    Console.WriteLine("Part Time Emp of emp from 1 to 6");
                    break;

                case PartTimeEmp partTimeEmp when (partTimeEmp.EmpCode > 5 && partTimeEmp.EmpCode < 10):
                    Console.WriteLine("Part Time Emp of emp from 7 to 10");
                    break;

                case FullTimeEmp fullTimeEmp when (fullTimeEmp.EmpCode > 0 && fullTimeEmp.EmpCode < 6):
                    Console.WriteLine("Full Time Emp of emp from 1 to 6");
                    break;

                case FullTimeEmp fullTimeEmp when (fullTimeEmp.EmpCode > 6 && fullTimeEmp.EmpCode < 10):
                    Console.WriteLine("Full Time Emp of emp from 7 to 10");
                    break;
            }
        }

        ///// <summary>
        ///// Multiple properties
        ///// </summary>
        ///// <param name="e"></param>

        //private static void Print(Employee e)
        //{
        //    switch (e.EmpCode, e.Name)
        //    {
        //        case (0, "Emp 01"):
        //            Console.WriteLine("Emp code is b/w 1 and 5");
        //            break;
        //    }
        //}

        //private static void Print(Employee e)
        //{
        //    var code = int.Parse(e.EmpCode);
        //    switch (code)
        //    {
        //        case (> 0) and (< 6) :
        //            Console.WriteLine("Emp code is b/w 1 and 5");
        //            break;
        //        case 6:
        //            Console.WriteLine("Emp code is 6");
        //            break;
        //        case (> 6) and (< 11):
        //            Console.WriteLine("Emp code is b/w 6 and 10");
        //            break;
        //    }
        //}
    }
}

