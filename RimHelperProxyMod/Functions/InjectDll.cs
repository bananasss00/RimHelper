using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using IPCInterface;
using RimWorld;
using Verse;
using IPCInterface.Extensions;
using RimHelperProxyMod.Extensions;

namespace RimHelperProxyMod.Functions
{
    public static class InjectDll
    {
        public static string GetResult(InjectParameters parameters)
        {
            // memory leaks
            var dll = Assembly.Load(File.ReadAllBytes(parameters.dllPath));
            var targetClass = dll.GetType(parameters.dllClass);
            var targetFunction = targetClass.GetMethod(parameters.dllFunction);
            return (string)targetFunction.Invoke(null, null);
        }
    }
}