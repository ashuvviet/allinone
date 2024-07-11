using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examples.OCP
{
    public class Rectangle
    {
        public double Height { get; set; }
        public double Width { get; set; }
    }

    public class Circle
    {
        public double Radius { get; set; }
    }

    public class AreaCalculator
    {
        public static double TotalArea(params object[] arrObjects)
        {
            double area = 0;
            Circle objCircle;
            foreach (var obj in arrObjects)
            {
                if (obj is Rectangle objRectangle)
                {
                    area += objRectangle.Height * objRectangle.Width;
                }
                else
                {
                    objCircle = (Circle)obj;
                    area += objCircle.Radius * objCircle.Radius * Math.PI;
                }
            }

            return area;
        }
    }
}
