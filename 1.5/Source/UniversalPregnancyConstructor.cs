using HarmonyLib;
using Verse;

namespace UniversalPregnancy
{
    [StaticConstructorOnStartup]
    public static class UniversalPregnancyConstructor
    {
        static UniversalPregnancyConstructor()
        {
            var harmony = new Harmony(UniversalPregnancyMod.PACKAGE_ID);
            harmony.PatchAll();
        }
    }
}
