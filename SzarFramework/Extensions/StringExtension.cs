using System;
using System.Reflection;

namespace SzarFramework
{
    public static class StringExtension 
    {
        public static Type[] GetTypesFromAssembly(this string assemblyName)
        {
            return Assembly.Load(assemblyName).GetTypes();
        }

    }
}
