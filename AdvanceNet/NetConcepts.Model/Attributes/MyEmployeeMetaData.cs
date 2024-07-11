using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetConcepts.Model.Attributes
{
    public class MyEmployeeMetaDataAttibute : Attribute
    {
        public int Order { set; get; }

        public string Tag { get; set; }
    }
}
