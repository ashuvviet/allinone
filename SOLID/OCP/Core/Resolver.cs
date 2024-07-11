using Core.Attributes;
using Core.Contracts;
using System;
using System.Collections.Generic;
using System.Composition;
using System.Composition.Hosting;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.Loader;

namespace Core
{
    public class Resolver
    {
        public IEnumerable<Assembly> Assemblies
        {
            get
            {
                var executableLocation = Assembly.GetEntryAssembly().Location;
                var path = Path.GetDirectoryName(executableLocation);
                var assemblies = Directory
                            .GetFiles(path, "*.dll", SearchOption.AllDirectories)
                            .Select(AssemblyLoadContext.Default.LoadFromAssemblyPath)
                            .ToList();

                return assemblies;
            }
        }

        /// <summary>
        /// The services imports.
        /// </summary>
        [ImportMany]
        public IEnumerable<Lazy<IService, ServiceAttribute>> serviceImports { get; set; }

        public void Resolve()
        {
            var myAssemblies = Assemblies.Where(asm => asm.IsDefined(typeof(AutoGenerateAttribute), true));

            var configuration = new ContainerConfiguration().WithAssemblies(myAssemblies);
            using (var container = configuration.CreateContainer())
            {
                serviceImports = container.GetExports<Lazy<IService, ServiceAttribute>>();
            }

            foreach (var item in serviceImports)
            {
                var contract = item.Metadata.Contract;
                var instance = item.Value;

                Container.AddSingelten(contract, instance);
            }
        }
    }
}
