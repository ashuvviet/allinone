using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetConcepts.Model.Models
{
    internal class Training
    {
        //private static string MyPro;

        //public event EventHandler OnTrainingCreated;

        public Training(string n, string c, int cost)
        {
            Name = n;
            CouseType = c;
            Cost = cost;
        }

        public Training()
        {
        }

        public string Name { get; set; }

        public string CouseType { get; set; }

        public int Cost { get; set; }

        public string GetFullDetails()
        {
            return Name + CouseType + Cost;
        }
    }
}
