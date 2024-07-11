using Examples.OCP;
using System;

namespace Examples
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("*** OCP Examples ****");

            Console.WriteLine("Enter Circle Radius : ");
            var radius = Console.ReadLine();
            var circle = new Circle() { Radius = double.Parse(radius) };

            Console.WriteLine("Enter Rectangle Height : ");
            var height = Console.ReadLine();
            Console.WriteLine("Enter Rectangle Width : ");
            var width = Console.ReadLine();
            var rectangle = new Rectangle { Height = double.Parse(height), Width = double.Parse(width) };
            var totalArea = AreaCalculator.TotalArea(circle, rectangle);

            Console.WriteLine($"*** Total Area : {totalArea}****");

            //double totalCost = 0;
            //foreach (var training in CourseCalculator.GetAll())
            //{
            //    CourseCalculator.GetCertifications(training);
            //    totalCost += CourseCalculator.TotalCost(training);
            //    Console.WriteLine($"*** Modules : { string.Join("->", CourseCalculator.GetModules(training)) } ****");
            //}


            //Console.WriteLine($"*** Total Training Cost : {totalCost} ****");
            Console.ReadLine();
        }
    }
}
