﻿using System.Linq;
using System.Reflection;
using System.Text;
using RimHelperProxyMod.Harmony;
using RimWorld;
using Verse;
using Verse.AI;

namespace RimHelperProxyMod.Extensions
{
    public static class ReflectionExtensions
    {
        public static string GetParametersString(this MethodBase method) =>
            string.Join(",", method.GetParameters().Select(o => $"{o.ParameterType.Name} {o.Name}").ToArray());

        public static string GetMethodNameString(this MethodBase method) =>
            $"{method.ReflectedType./*Full*/Name}.{method.Name}";

        public static string GetMethodFullString(this MethodBase method) =>
            $"{method.GetMethodNameString()}({method.GetParametersString()})";
    }
}