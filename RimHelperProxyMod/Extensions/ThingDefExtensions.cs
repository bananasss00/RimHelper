using System.Collections.Generic;
using System.Linq;
using Verse;

namespace RimHelperProxyMod.Extensions
{
    public static  class ThingDefExtensions
    {
        private static HashSet<ThingDef> _craftableProducts = null;
        private static List<Thing> _thingsOnMap = null;

        public static void Reset()
        {
            _craftableProducts?.Clear();
            _craftableProducts = null;

            _thingsOnMap?.Clear();
            _thingsOnMap = null;
        }

        public static bool CanCraft(this ThingDef d)
        {
            if (_craftableProducts == null)
            {
                _craftableProducts = new HashSet<ThingDef>(DefDatabase<RecipeDef>.AllDefs
                    .Where(x => x.products != null)
                    .SelectMany(x => x.products.Select(y => y.thingDef))
                );
            }

            return _craftableProducts.Contains(d);
        }

        public static float? CountOnMap(this ThingDef d)
        {
            if (Current.ProgramState != ProgramState.Playing)
                return null;

            if (_thingsOnMap == null && Find.CurrentMap != null)
            {
                _thingsOnMap = new List<Thing>(Find.CurrentMap.listerThings.AllThings);
            }

            if (_thingsOnMap == null)
                return null;

            float count = 0f;
            _thingsOnMap.ForEach(t =>
            {
                if (!t.Position.Fogged(t.Map) && d == t.def)
                    count += t.stackCount;
            });

            return count.Nullify();
        }
    }
}