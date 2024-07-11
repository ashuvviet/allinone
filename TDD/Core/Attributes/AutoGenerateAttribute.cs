using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Attributes
{
    /// <summary>
    /// Attribute for setting assebmly for generation.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = false)]
    public sealed class AutoGenerateAttribute : Attribute
    {
        /// <summary>
        /// Gets the name.
        /// </summary>
        public static string Name
        {
            get
            {
                return "AutoGenerate";
            }
        }
    }
}
