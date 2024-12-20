using System;
using System.IO;
using System.Reflection;
using Teigha.Runtime;

namespace LoadAssemblies.NCAD
{
    public class ExtensionInitialize : IExtensionApplication
    {
        public void Initialize()
        {
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomainOnAssemblyResolve;
        }

        public void Terminate() { }

        private Assembly CurrentDomainOnAssemblyResolve(object Sender, ResolveEventArgs Args)
        {
            string name = GetAssemblyName(Args);
            string path = Path.Combine(Path.GetDirectoryName((typeof(ExtensionInitialize).Assembly.Location)), name + ".dll");
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
