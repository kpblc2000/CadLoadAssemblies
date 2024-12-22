using Autodesk.AutoCAD.Runtime;
using System.Reflection;
using System;
using System.IO;

namespace LoadAssemblies.ACAD
{
    public class InitializeExtension : IExtensionApplication
    {
        public void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
        }

        public void Terminate() { }

        private Assembly CurrentDomainOnAssemblyResolve(object Sender, ResolveEventArgs Args)
        {
            string name = GetAssemblyName(Args);
            string path = Path.Combine(Path.GetDirectoryName((typeof(InitializeExtension).Assembly.Location)),
                name + ".dll");
            if (File.Exists(path))
            {
                Assembly assembly = Assembly.LoadFrom(path);
                if (assembly.FullName == Args.Name)
                {
                    return assembly;
                }
            }
            return null;
        }
        private string GetAssemblyName(ResolveEventArgs args)
        {
            if (args.Name.IndexOf(",") > -1)
            {
                return args.Name.Substring(0, args.Name.IndexOf(","));
            }
            else
            {
                return args.Name;
            }
        }
    }
}
