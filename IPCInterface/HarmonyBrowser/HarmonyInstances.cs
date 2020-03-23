using System;
using System.Collections.Generic;
using System.ComponentModel;

namespace IPCInterface.HarmonyBrowser
{
    [Serializable]
    public class HarmonyInstances
    {
        public bool SkipMethodsInGenericClass { get; set; } = false;
        public List<string> List { get; set; } = new List<string>();
    }

    [Serializable]
    public class HarmonyUnpatch
    {
        public string instance { get; set; }
        public List<string> patches { get; set; }
    }

    [Serializable]
    public class Pair
    {
        public override string ToString() => $"{Key} = {Value}";

        public string Key { get; set; }
        public string Value { get; set; }
    }

    [Serializable]
    public class ReportRecord
    {
        [DisplayName("Assembly")]
        public string asm { get; set; }
        [DisplayName("Patch Name")]
        public string name { get; set; }
        [DisplayName("Avg Time")]
        public long avgTime { get; set; }
        [DisplayName("Ticks")]
        public long num { get; set; }
        [DisplayName("Min Time")]
        public long min { get; set; }
        [DisplayName("Max Time")]
        public long max { get; set; }
        [DisplayName("(Avg Time * Ticks) in ms")]
        public long avgTimeTick { get; set; }
    }
}