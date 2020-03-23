using System;
using UnityEngine;
using Verse;

namespace RimHelperProxyMod.Extensions
{
    public static class FloatExtensions
    {
        public static bool IsNull(this float floatValue) => floatValue > -0.001f && floatValue < 0.001f;

        public static float? Nullify(this float floatValue)
        {
            if (floatValue > -0.001f && floatValue < 0.001f)
                return null;
            return floatValue;
        }

        public static float? Nullify(this float? floatValue)
        {
            if (floatValue == null || floatValue > -0.001f && floatValue < 0.001f)
                return null;
            return floatValue;
        }

        public static float? ToPercent(this float? floatValue)
        {
            return floatValue == null ? null : (float?) Math.Round((float) floatValue * 100f, 2);
        }

        public static float ToPercent(this float floatValue)
        {
            return (float) Math.Round(floatValue * 100f, 2);
        }

        public static float RoundTo2(this float floatValue)
        {
            return (float) Math.Round(floatValue, 2);
        }

        public static float? RoundTo2(this float? floatValue)
        {
            return floatValue == null ? null : (float?) Math.Round((float) floatValue, 2);
        }

        public static float? ByStyle(this float? f, ToStringStyle style)
        {
            return f?.ByStyle(style);
        }

        public static float ByStyle(this float f, ToStringStyle style)
        {
            float value;
            switch (style)
            {
                case ToStringStyle.Integer:
                    value = (float) Mathf.RoundToInt(f);
                    break;
                case ToStringStyle.PercentZero:
                case ToStringStyle.PercentOne:
                case ToStringStyle.PercentTwo:
                    value = f.ToPercent();
                    break;
                case ToStringStyle.WorkAmount:
                    value = Mathf.CeilToInt(f / 60f);
                    break;
                //case ToStringStyle.Temperature:
                //case ToStringStyle.TemperatureOffset:
                default:
                    value = f;
                    break;
            }

            return value;
        }
    }
}