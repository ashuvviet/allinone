using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Attributes
{
    /// <summary>
    /// Defines an attribute for service export(MEF)
    /// </summary>
    [MetadataAttribute]
    [AttributeUsage(AttributeTargets.Class)]
    public class ServiceAttribute : ExportAttribute
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ServiceAttribute"/> class.
        /// </summary>
        public ServiceAttribute()
            : base(typeof(IService))
        {
        }

        /// <inheritdoc/>
        public Type Contract { get; set; }

        /// <inheritdoc/>
        public int Order { get; set; }
    }
}
