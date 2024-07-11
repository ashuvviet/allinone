using System.Collections.Generic;
using System.Linq;

namespace Examples.OCP
{
    public class CourseCalculator
    {
        public static IEnumerable<ICourse> GetAll()
        {
            var list = new List<Course>();
            list.Add(new Python("Basics of Python", 1000, 20));
            list.Add(new AdvanceDotNet("Advance .Net", 2000, 40));
            return list;
        }

        public static double TotalCost(params ICourse[] arrObjects)
        {
            double cost = 0;
            foreach (var obj in arrObjects.OfType<ICourseCost>())
            {
                cost += obj.TotalCost();
            }

            return cost;
        }

        public static IEnumerable<string> GetCertifications(params ICourse[] arrObjects)
        {
            var list = new List<string>();
            foreach (var obj in arrObjects.OfType<ICourseCertification>())
            {
                list.AddRange(obj.Certifications);
            }

            return list;
        }

        public static IEnumerable<string> GetModules(ICourse obj)
        {
            return obj.Modules;
        }
    }
}
